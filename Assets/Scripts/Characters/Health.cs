using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Health : MonoBehaviour, IReloadable
{
    [SerializeField] private int _currentHp;
    [SerializeField] private int _maxHp;

    private bool _isDead;

    public event IntChangeHandler onHpChanged;
    public event Action onDeath;

    public int CurrentHp { get => _currentHp; }

    /// <summary>
    /// Use change to add or remove hp (SIGNS MATTER)
    /// </summary>
    /// <param name="change"></param>
    public void ChangeHp(int change)
    {
        if (_isDead) return;

        _currentHp += change;
        if (_currentHp <= 0)
        {
            _isDead = true;
            onDeath?.Invoke();
            _currentHp = 0;
        }
        else if(_currentHp > _maxHp)
        {
            _currentHp = _maxHp;
        }

        onHpChanged?.Invoke(_currentHp);
    }

    public void Reload()
    {
        _isDead = false;
        _currentHp = _maxHp;
        onHpChanged?.Invoke(_currentHp);
    }







}
