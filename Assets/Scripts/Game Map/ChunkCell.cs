using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ChunkCell : MonoBehaviour
{

    [SerializeField]
    [Tooltip("Never ever add or remove from this collection")]
    private CellObstacle[] _availableCells;

    private void OnValidate()
    {
        CheckCellsValidity();
    }

    private void CheckCellsValidity()
    {
        int lenght = Enum.GetNames(typeof(ObTypes)).Length;

        if (_availableCells == null || _availableCells.Length != lenght)
        {
            _availableCells = new CellObstacle[lenght];

            for (int i = 0; i < _availableCells.Length; i++)
            {
                ObTypes cellType = (ObTypes) i; 
                _availableCells[i] = new CellObstacle(cellType);
            }
        }
    }

    public void Init(ObTypes type)
    {
        foreach (var cell in _availableCells)
        {
            if(cell.CellType == type)
            {
                cell.CellObject.SetActive(true);
                continue;
            }
            cell.CellObject.SetActive(false);
        }

        BroadcastMessage("Initialize", null, SendMessageOptions.DontRequireReceiver);
    }
}
