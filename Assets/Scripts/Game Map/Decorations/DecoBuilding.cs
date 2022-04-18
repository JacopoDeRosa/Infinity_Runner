using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoBuilding : MonoBehaviour
{
    [SerializeField] private GameObject[] _parts;

    private int _activePart;


    /// <summary>
    /// Returns false if the building is over
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public bool SetPartActive(int index)
    {
        if(index >= _parts.Length)
        {
            return false;
        }

        _parts[index].SetActive(true);
        _activePart = index;
        return true;
    }

    public void ResetBuilding()
    {
        _parts[_activePart].SetActive(false);
    }


}
