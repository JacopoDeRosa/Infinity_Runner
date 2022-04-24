using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;


public class PlayerLaneController : MonoBehaviour, IReloadable
{
    [SerializeField] private float _inputSmoothing;
    [SerializeField] private PlayerInput _input;
    [SerializeField] private float _actionThreshold;
    [SerializeField] private Lanes _currentLane;

    private float _xPosition;

    private Vector2 _swipe;
    private Vector2 _pos;

    private void OnValidate()
    {
        if(_input == null)
        {
            _input = FindObjectOfType<PlayerInput>();
        }
    }

    private void Awake()
    {
        //   _input.actions["Swipe"].performed += OnSwipe;
        _input.actions["PointerPos"].performed += OnPointerPosition;
        _input.actions["Fire"].started += OnFire;
    }

    private void OnDestroy()
    {
        if(_input != null)
        {
            //  _input.actions["Swipe"].performed -= OnSwipe;
            _input.actions["PointerPos"].performed += OnPointerPosition;
            _input.actions["Fire"].started -= OnFire;
        }
    }

    private void OnSwipe(InputAction.CallbackContext context)
    {
        Vector2 swipe = context.ReadValue<Vector2>();

        if(swipe.x > _actionThreshold)
        {
            GoRight();
        }
        else if(swipe.x < -_actionThreshold)
        {
            GoLeft();
        }
    }

    private void OnPointerPosition(InputAction.CallbackContext context)
    {
        _pos = context.ReadValue<Vector2>();
    }

    private void OnFire(InputAction.CallbackContext context)
    {
       if(_pos.x < Screen.width / 2)
       {
            GoLeft();
       }
       else if(_pos.x > Screen.width / 2)
       {
            GoRight();
       }
    }

    private void GoLeft()
    {
        if(_currentLane != Lanes.Left)
        {
            transform.Translate(new Vector3(-1, 0, 0));
            _currentLane--;
        }
    }
    private void GoRight()
    {
        if (_currentLane != Lanes.Right)
        {
            transform.Translate(new Vector3(1, 0, 0));
            _currentLane++;
        }
    }

    public void Reload()
    {
        transform.position = new Vector3(0, transform.position.y, transform.position.z);
        _currentLane = Lanes.Middle;
    }
}
