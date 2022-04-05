using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Item : MonoBehaviour
{
    public event Action onPickup;

    public virtual void PickUp(Character character)
    {
        onPickup.Invoke();
        gameObject.SetActive(false);
    }
}
