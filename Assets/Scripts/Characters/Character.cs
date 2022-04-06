using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

public class Character : MonoBehaviour
{
    [SerializeField]
    private Health _health;

    [SerializeField]
    private bool _isEnemy;

    [SerializeField]
    private float _speed;

    public float Speed { get => _speed; }
    public Health Health { get => _health; }

    public event FloatChangeHandler _onSpeedChange;

    // Sets the character speed and invokes the onSpeedChange event to comunicate with eventual listeners, this is used for example
    // to comunicate with Larry's animator without referencing it in the character script.
    [Button]
    public void SetSpeed(float speed)
    {
        _speed = speed;
        _onSpeedChange?.Invoke(_speed);
    }

    // The onSpeedChange event gets called at start to tell listeners about the character's starting speed
    private void Start()
    {
        _onSpeedChange?.Invoke(_speed);
    }
}
