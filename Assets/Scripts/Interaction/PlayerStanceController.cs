using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStanceController : MonoBehaviour, IReloadable
{
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerCharacter _playerCharacter;
    [SerializeField] private PlayerInput _input;
    [SerializeField] private Stance _currentStance = Stance.Running;
    [SerializeField] private float _actionThreshold;

    public Stance CurrentStance { get => _currentStance; }

    private Vector2 _swipe;

    private void OnValidate()
    {
        if(_input == null)
        {
            _input = FindObjectOfType<PlayerInput>();
        }
    }


    private void Awake()
    {
        _playerCharacter.onSpeedChange += OnSpeedChange;
        _playerCharacter.onDamage += OnDamage;
        _playerCharacter.onDeath += OnDeath;

        _input.actions["Swipe"].performed += OnSwipe;
    }

    private void OnDestroy()
    {
        if (_input != null)
        {
            _input.actions["Swipe"].performed -= OnSwipe;
        }
    }

    private void Slide()
    {
        if (_currentStance != Stance.Running) return;
        _currentStance = Stance.Sliding;
        _animator.SetTrigger("Slide");
    }

    private void Jump()
    {
        if (_currentStance != Stance.Running) return;
        _currentStance = Stance.Jumping;
        _animator.SetTrigger("Jump");
    }

    public void ResetStance()
    {
        _currentStance = Stance.Running;
        foreach (var parameter in _animator.parameters)
        {
            if (parameter.type == AnimatorControllerParameterType.Trigger)
            {
                _animator.ResetTrigger(parameter.name);
            }
        }
    }

    private void OnSwipe(InputAction.CallbackContext context)
    {
        _swipe = context.ReadValue<Vector2>();
        if (_swipe.y > _actionThreshold)
        {
            Jump();
        }
        else if (_swipe.y < -_actionThreshold)
        {
            Slide();
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

    private void  OnDeath()
    {
        _animator.SetTrigger("Die");
    }

    public void Reload()
    {
        _animator.SetTrigger("Reset");  
        _currentStance = Stance.Running;
    }
}
