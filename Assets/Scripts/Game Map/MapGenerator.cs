using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class MapGenerator : MonoBehaviour
{
    
    
    [SerializeField]
    private Larry _larry;
    [SerializeField] 
    private MapChunk[] _allChunks;
    [SerializeField]
    private Transform _chunksSpawnPoint;
    [SerializeField]
    private float _despawnZ = -1;
    [SerializeField]
    private AnimationCurve _yPositionCurve;
    [SerializeField]
    private float _chunkLenght;
    [SerializeField]
    private float _tickRate;

    [ShowInInspector]
    private List<MapChunk> _activeChunks = new List<MapChunk>();

    [ShowInInspector]
    [ReadOnly]
    private Queue<MapChunk> _availableChunks;
    
    [ShowInInspector]
    [ReadOnly]
    private int _currentObstcleWait = 0;

    [SerializeField]
    [ReadOnly]
    private float _mapTimer = 0;


    private float NextChunkWait { get => (_chunkLenght / _larry.Speed) - (_tickRate * _larry.Speed); }

    private WaitForSeconds _tickWait;

    private void Awake()
    {
        // Populates the queue with all the chunks marked in the start array, this array is usless afterwards and can be ignored
        // This queue is used to recycle the chunks in a FIFO pattern.
        _availableChunks = new Queue<MapChunk>();
        foreach(MapChunk chunk in _allChunks)
        {
            _availableChunks.Enqueue(chunk);
        }
        _tickWait = new WaitForSeconds(_tickRate);
        StartCoroutine(UpdateMap());
    }

    private void RecycleChunks()
    {
        if (_mapTimer >= NextChunkWait)
        {
            // Gets a new Chunk from the ones that have gone trough the treadmill in a FIFO pattern
            var activeChunk = _availableChunks.Dequeue();
            // Sets the chunk active since it gets deactivated at cycle end
            activeChunk.gameObject.SetActive(true);
            // Sets the chunk spawn position and snaps it on the "Tsubasa" curve.
            activeChunk.transform.position = _chunksSpawnPoint.position;
            activeChunk.transform.position = new Vector3(activeChunk.transform.position.x, GetChunkY(activeChunk.transform), activeChunk.transform.position.z);
            // Checks if it's time to spawn a new obstacle, if so the chunk will be marked as an obstacle and a new wait time chosen
            if (_currentObstcleWait == 0)
            {
                activeChunk.Init(true);
                _currentObstcleWait = Random.Range(2, 4);
            }
            // Otherwise the chunk will be set as empty and wait will decrease
            else
            {
                activeChunk.Init(false);
                _currentObstcleWait--;
            }
            // Adds the chunk to the active chunks list allowing it to be moved in update.
            _activeChunks.Add(activeChunk);

            _mapTimer = 0;
        }
        else
        {
            _mapTimer += _tickRate;
        }
    }

    private void MoveChunks()
    {
        // If a chunk passes the despawn position it will be set for recycling, collections shouldn't be modified in loops
        // so it gets stored in a variable and recycled afterward.
        MapChunk chunkToRemove = null;
        foreach (var chunk in _activeChunks)
        {
            if (chunk.transform.position.z <= _despawnZ)
            {
                chunkToRemove = chunk;
            }
            else
            {
                // Moves the chunk on Z based on Larry's speed and on Y based on the provided curve
                float chunkY = GetChunkY(chunk.transform);
                float chunkZ = chunk.transform.position.z - (_larry.Speed * _tickRate);
                chunk.transform.position = new Vector3(0, chunkY, chunkZ);

                float nextChunkY = _yPositionCurve.Evaluate(chunk.transform.position.z - _chunkLenght);

                chunk.transform.LookAt(new Vector3(0, nextChunkY, chunk.transform.position.z - _chunkLenght));
            }

        }

        // If a chunk was marked it gets recycled
        if (chunkToRemove != null)
        {
            _activeChunks.Remove(chunkToRemove);
            chunkToRemove.ResetChunk();
            _availableChunks.Enqueue(chunkToRemove);
        }
    }

    private float GetChunkY(Transform chunk)
    {
        // Finds the Y position of the chunk based on his Z position by placing it on the serialized curve
        return _yPositionCurve.Evaluate(chunk.position.z);
    }

    private IEnumerator UpdateMap()
    {
        while (true)
        {
            RecycleChunks();
            MoveChunks();
            yield return _tickWait;
        }
    }
    
}

