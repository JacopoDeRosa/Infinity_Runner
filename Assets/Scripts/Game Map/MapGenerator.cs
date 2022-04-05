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

    [ShowInInspector]
    private List<MapChunk> _activeChunks = new List<MapChunk>();


    private Queue<MapChunk> _availableChunks;

    private int _currentWait = 0;

    private float NextChunkWait { get => 1 / _larry.Speed; }

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
            if (chunk.transform.position.z <= -1)
            {
                chunkToRemove = chunk;
            }
            else
            {
                chunk.transform.Translate(new Vector3(0, 0, -(_larry.Speed * Time.deltaTime)));
            }

        }

        if(chunkToRemove != null)
        {
            _activeChunks.Remove(chunkToRemove);
            chunkToRemove.ResetChunk();
            _availableChunks.Enqueue(chunkToRemove);
        }
    }

    private IEnumerator MapGeneration()
    {
        while(true)
        {
            // Spawns a new Chunk
            var activeChunk = _availableChunks.Dequeue();
            activeChunk.gameObject.SetActive(true);
            activeChunk.transform.position = _chunksSpawnPoint.position;
            if(_currentWait == 0)
            {
                activeChunk.Init(true);
                _currentWait = Random.Range(2, 5);
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
