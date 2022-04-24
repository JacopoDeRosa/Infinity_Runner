using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerCharacter : MonoBehaviour, IReloadable
{
    [SerializeField]
    private Health _health;

    [SerializeField]
    private PlayerStanceController _slideController;

    [SerializeField]
    private float _baseSpeed = 3;

    [SerializeField]
    private float _speedChangeSmoothness;



    private List<EffectContainer> _activeEffects;
    private float _currentSpeed;
    private float _targetSpeed;

    public float Speed { get => _currentSpeed; }
    public Stance CurrentStance { get => _slideController.CurrentStance; }

    public event FloatChangeHandler onSpeedChange;

    public event IntChangeHandler onDamage;
    public event IntChangeHandler onHeal;
    public event Action onDeath;

    private void Awake()
    {
        _activeEffects = new List<EffectContainer>();
        _health.onDeath += OnDeath;
    }

    // The onSpeedChange event gets called at start to tell listeners about the character's starting speed
    private void Start()
    {
        _targetSpeed = _baseSpeed;
        _currentSpeed = _targetSpeed;
        onSpeedChange?.Invoke(Speed);
    }

    private void Update()
    {
        UpdateSpeed();
        UpdateEffects();
    }

    private void UpdateSpeed()
    {
        if (_currentSpeed <= _targetSpeed - 0.1f || _currentSpeed >= _targetSpeed + 0.1f)
        {
            float smooth = Mathf.Clamp01(_speedChangeSmoothness * Time.deltaTime);

            _currentSpeed = Mathf.Lerp(_currentSpeed, _targetSpeed, smooth);

            onSpeedChange?.Invoke(_currentSpeed);
        }
        else if (_currentSpeed != _targetSpeed)
        {
            _currentSpeed = _targetSpeed;
            onSpeedChange?.Invoke(_currentSpeed);
        }
    }

    private void UpdateEffects()
    {
        List<EffectContainer> toRemove = new List<EffectContainer>();
        foreach (EffectContainer cont in _activeEffects)
        {
            if (cont.AddToCurrentTime(Time.deltaTime))
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

    // Sets the character speed and invokes the onSpeedChange event to comunicate with eventual listeners, this is used for example
    // to comunicate with Larry's animator without referencing it in the character script.
    public void ChangeSpeed(float speed)
    {
        _targetSpeed += speed;
    }

    public void AddEffect(PickupEffect effect)
    {
        var container = new EffectContainer(effect);

        if (_activeEffects.Contains(container))
        {
            var existingContainer = _activeEffects.Find(x => x.Equals(container));
            existingContainer.SetCurrentTime(0);
            return;
        }

        effect.Begin(this);
        _activeEffects.Add(new EffectContainer(effect));
    }

    public void DealDamage(int damage)
    {
        onDamage?.Invoke(damage);
        _health.ChangeHp(-damage);
    }
    public void HealDamage(int damage)
    {
        onHeal?.Invoke(damage);
        _health.ChangeHp(damage);
    }

    private void OnDeath()
    {
        _targetSpeed = 0;
        onDeath?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        // When Larry touches a tile with an interaction compent he triggers the effect on himself
        // After that it's the tile's job to give the effect to larry
        var tile = other.gameObject.GetComponent<InteractiveTile>();
        if (tile != null)
        {
            tile.Activate(this);
        }
    }

    public void Reload()
    {
        _targetSpeed = _baseSpeed;
        _currentSpeed = _targetSpeed;
        onSpeedChange?.Invoke(_currentSpeed);
    }
}

