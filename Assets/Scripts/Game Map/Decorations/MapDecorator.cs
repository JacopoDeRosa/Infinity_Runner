using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDecorator : MonoBehaviour
{
    [SerializeField] private int _lightsInterval = 5;
    [SerializeField] private MapGenerator _generator;

    private int _chunksGenerated;

    private int _activeBuildingRight = 0;
    private int _currentBuildingIndexRight = 0;

    private int _activeBuildingLeft = 0;
    private int _currentBuildingIndexLeft = 0;

    private void Awake()
    {
        _generator.onChunkInit += OnChunkGenerated;
    }

    private void OnChunkGenerated(MapChunk chunk)
    {

        var deco = chunk.GetComponent<ChunkDecorations>();
        _chunksGenerated++;

        if(deco.BuildingsRight[_activeBuildingRight].SetPartActive(_currentBuildingIndexRight))
        {
            _currentBuildingIndexRight++;
        }
        else
        {
            _activeBuildingRight = deco.GetRandomBuildingRight;
            _currentBuildingIndexRight = 0;
        }

        if (deco.BuildingsLeft[_activeBuildingLeft].SetPartActive(_currentBuildingIndexLeft))
        {
            _currentBuildingIndexLeft++;
        }
        else
        {
            _activeBuildingLeft = deco.GetRandomBuildingLeft;
            _currentBuildingIndexLeft = 0;
        }

        if (_chunksGenerated % _lightsInterval == 0)
        {
            deco.StreetLamp.SetActive(true);
        }

        
    }
}
