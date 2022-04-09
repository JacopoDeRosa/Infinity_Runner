using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

public class Larry : MonoBehaviour
{
    [SerializeField]
    private Health _health;

    [SerializeField]
    private LarryJumpAndSlide _slideController;

    [SerializeField]
    private bool _isEnemy;

    [SerializeField]
    private float _speed;

    [ShowInInspector]
    private List<EffectContainer> _activeEffects;

    public float Speed { get => _speed; }
    public Health Health { get => _health; }
    public Stance CurrentStance { get => _slideController.CurrentStance; }

    public event FloatChangeHandler _onSpeedChange;

    // Sets the character speed and invokes the onSpeedChange event to comunicate with eventual listeners, this is used for example
    // to comunicate with Larry's animator without referencing it in the character script.
    public void ChangeSpeed(float speed)
    {
        _speed += speed;
        _onSpeedChange?.Invoke(_speed);
    }

    public void AddEffect(PickupEffect effect)
    {
        var container = new EffectContainer(effect);

        if (_activeEffects.Contains(container))
        {
            var existingContainer =  _activeEffects.Find(x => x.Equals(container));
            existingContainer.SetCurrentTime(0);
            return;
        }

        effect.Begin(this);
        _activeEffects.Add(new EffectContainer(effect));
    }

    private void Awake()
    {
        _activeEffects = new List<EffectContainer>();
    }

    // The onSpeedChange event gets called at start to tell listeners about the character's starting speed
    private void Start()
    {
        _onSpeedChange?.Invoke(_speed);
    }

    private void Update()
    {
        List<EffectContainer> toRemove = new List<EffectContainer>();
        foreach (EffectContainer cont in _activeEffects)
        {
            if(cont.AddToCurrentTime(Time.deltaTime))
            {
                toRemove.Add(cont);
            }
        }

        foreach (EffectContainer remove in toRemove)
        {
            remove.Effect.End(this);
            _activeEffects.Remove(remove);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var tile = other.gameObject.GetComponent<InteractiveTile>();
        if(tile != null)
        {
            tile.Activate(this);
        }
    }
}

