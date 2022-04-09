using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LarryAnimatorUpdater : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Larry _larry;

    private void Awake()
    {
        _larry._onSpeedChange += OnSpeedChange;
        _larry.Health.onHpChanged += OnDamage;
    }
    private void OnSpeedChange(float speed)
    {
        _animator.SetFloat("Speed", speed);
    }

    private void OnDamage(int change)
    {
        print(change);
        if (change < 0)
        {
         
            _animator.SetTrigger("Stumble");
        }
    }
}
