using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LarryStanceController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Larry _larry;
    [SerializeField] private Stance _currentStance = Stance.Running;

    public Stance CurrentStance { get => _currentStance; }
    private void Awake()
    {
        _larry._onSpeedChange += OnSpeedChange;
        _larry._onDamage += OnDamage;
    }

    private void Update()
    {
        // Stop Larry from jumping sliding or anything else if he is not running.
        if (_currentStance != Stance.Running) return;

        float rawVertical = Input.GetAxisRaw("Vertical");

        if(rawVertical == 1)
        {
            _currentStance = Stance.Jumping;
            _animator.SetTrigger("Jump");
        }
        else if(rawVertical == -1)
        {
            _currentStance = Stance.Sliding;
            _animator.SetTrigger("Slide");
        }
    }

    public void ResetStance()
    {
        _currentStance = Stance.Running;
        foreach (var parameter in _animator.parameters)
        {
            if(parameter.type == AnimatorControllerParameterType.Trigger)
            {
                _animator.ResetTrigger(parameter.name);
            }
        } 
    }

    private void OnSpeedChange(float speed)
    {
        _animator.SetFloat("Speed", speed);
    }

    private void OnDamage(int change)
    {
        _currentStance = Stance.Stumbling;
        _animator.SetTrigger("Stumble");
    }
}
