using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapChunk : MonoBehaviour
{
    [SerializeField] private ChunkCell[] _cells;

    public System.Action onInit;

    private void OnValidate()
    {
        if(_cells.Length != 3)
        {
            _cells = new ChunkCell[3];
        }
    }

    public void Init(bool isObstacle)
    {
        if(isObstacle == false)
        {
            foreach (var cell in _cells)
            {
                cell.Init(ObTypes.Empty);
            }
            return;
        }

        int passage = Random.Range(0, _cells.Length);

        int enumLenght = System.Enum.GetNames(typeof(ObTypes)).Length;

        for (int i = 0; i < _cells.Length; i++)
        {
            if(i == passage)
            {
                _cells[passage].Init(ObTypes.Empty);
                continue;
            }

            ObTypes obType = (ObTypes)Random.Range(0, enumLenght);

            _cells[i].Init(obType);
        }

        onInit?.Invoke();
    }

    public void ResetChunk()
    {
        gameObject.SetActive(false);
    }
}
