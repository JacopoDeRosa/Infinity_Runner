using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupsInitializer : MonoBehaviour
{
    [SerializeField] private InteractiveTile[] _items;

    public void Initialize()
    {
        foreach (var item in _items)
        {
            item.gameObject.SetActive(false);
        }

        var activeItem = _items[Random.Range(0, _items.Length)];
        activeItem.gameObject.SetActive(true);
    }
}
