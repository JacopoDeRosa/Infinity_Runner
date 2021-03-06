using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CellObstacle
{
    [SerializeField]
    private ObTypes _cellType;

    [SerializeField]
    private GameObject _cellObject;

    public ObTypes CellType { get => _cellType; }
    public GameObject CellObject { get => _cellObject; }

    public CellObstacle(ObTypes type)
    {
        _cellType = type;
    }
}