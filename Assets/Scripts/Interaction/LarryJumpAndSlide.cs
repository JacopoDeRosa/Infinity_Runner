using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LarryJumpAndSlide : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Stance _currentStance = Stance.Running;

    public Stance CurrentStance { get => _currentStance; }

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
    }

    public void Stumble()
    {
        _currentStance = Stance.Stumbling;
    }
}
