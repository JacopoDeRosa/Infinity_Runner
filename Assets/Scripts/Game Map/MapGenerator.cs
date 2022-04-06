using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class MapGenerator : MonoBehaviour
{
    [SerializeField]
    private Character _larry;
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

    [ShowInInspector]
    private List<MapChunk> _activeChunks = new List<MapChunk>();

    [ShowInInspector]
    [ReadOnly]
    private Queue<MapChunk> _availableChunks;
    
    [ShowInInspector]
    [ReadOnly]
    private int _currentWait = 0;

    private float NextChunkWait { get => _chunkLenght / _larry.Speed; }

    private void Awake()
    {
        _availableChunks = new Queue<MapChunk>();
        foreach(MapChunk chunk in _allChunks)
        {
            _availableChunks.Enqueue(chunk);
        }
    }

    private void Start()
    {
        StartCoroutine(MapGeneration());
    }

    void Update()
    {
        MapChunk chunkToRemove = null;
        foreach (var chunk in _activeChunks)
        {
            if (chunk.transform.position.z <= _despawnZ)
            {
                chunkToRemove = chunk;
            }
            else
            {
                chunk.transform.Translate(new Vector3(0, 0, -(_larry.Speed * Time.deltaTime)));
                chunk.transform.position = new Vector3(0, GetChunkY(chunk.transform), chunk.transform.position.z);
            }

        }

        if(chunkToRemove != null)
        {
            _activeChunks.Remove(chunkToRemove);
            chunkToRemove.ResetChunk();
            _availableChunks.Enqueue(chunkToRemove);
        }
    }

    private float GetChunkY(Transform chunk)
    {
        return _yPositionCurve.Evaluate(chunk.position.z);
    }

    private IEnumerator MapGeneration()
    {
        while(true)
        {
            // Spawns a new Chunk
            var activeChunk = _availableChunks.Dequeue();
            activeChunk.gameObject.SetActive(true);
            activeChunk.transform.position = _chunksSpawnPoint.position;
            activeChunk.transform.position = new Vector3(activeChunk.transform.position.x, GetChunkY(activeChunk.transform), activeChunk.transform.position.z);
            if(_currentWait == 0)
            {
                activeChunk.Init(true);
                _currentWait = Random.Range(2, 4);
            }
            else
            {
                activeChunk.Init(false);
                _currentWait--;
            }
            _activeChunks.Add(activeChunk);

            yield return new WaitForSeconds(NextChunkWait);
        }   
    }
}
