using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkDecorations : MonoBehaviour
{
    [SerializeField] private GameObject _streetLamp;
    [SerializeField] private DecoBuilding[] _buildingsR;
    [SerializeField] private DecoBuilding[] _buildingsL;

    public GameObject StreetLamp { get => _streetLamp; }

    public DecoBuilding[] BuildingsRight { get => _buildingsR; }
    public DecoBuilding[] BuildingsLeft { get => _buildingsL; }

    public int GetRandomBuildingRight { get => Random.Range(0, _buildingsR.Length); }
    public int GetRandomBuildingLeft { get => Random.Range(0, _buildingsL.Length); }


    public void ResetDeco()
    {
        _streetLamp.SetActive(false);
        foreach (var building in _buildingsR)
        {
            building.ResetBuilding();
        }
        foreach (var building in _buildingsL)
        {
            building.ResetBuilding();
        }
    }
}
