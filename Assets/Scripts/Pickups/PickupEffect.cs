using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickupEffect : ScriptableObject
{
    [SerializeField] private float _duration;

    public float Duration { get => _duration; }
    public abstract void Begin(Larry character);
    public abstract void End(Larry character);
}
