using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickupEffect : ScriptableObject
{
    [SerializeField] private float _duration;
    [SerializeField] private Sprite _effectSprite;

    public float Duration { get => _duration; }
    public abstract void Begin(PlayerCharacter character);
    public abstract void End(PlayerCharacter character);
}
