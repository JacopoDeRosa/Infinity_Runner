using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InteractiveTile : MonoBehaviour
{
    public event Action onPickup;

    public virtual void Activate(Larry character)
    {
        onPickup.Invoke();
        gameObject.SetActive(false);
    }
}
