using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LarryAnimatorUpdater : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Character _larry;

    private void Awake()
    {
        _larry._onSpeedChange += OnSpeedChange;
    }
    private void OnSpeedChange(float speed)
    {
        _animator.SetFloat("Speed", speed);
    }
}
