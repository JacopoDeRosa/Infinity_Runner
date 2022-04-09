using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Health : MonoBehaviour
{
    [SerializeField] private int _currentHp;
    [SerializeField] private bool _hasShotgun;

    public event IntChangeHandler onHpChanged;
    public event Action onDeath;
    public event Action onShotgunUsed;

    /// <summary>
    /// Use change to add or remove hp (SIGNS MATTER)
    /// </summary>
    /// <param name="change"></param>
    public void ChangeHp(int change)
    {
        if(_hasShotgun)
        {
            _hasShotgun = false;
            onShotgunUsed?.Invoke();
            return;
        }
        _currentHp += change;
        if(_currentHp <= 0)
        {
            onDeath?.Invoke();
            _currentHp = 0;
        }
        onHpChanged.Invoke(change);
    }


    



   
}
