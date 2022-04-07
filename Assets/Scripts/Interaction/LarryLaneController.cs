using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class LarryLaneController : MonoBehaviour, IController
{
    [SerializeField] private float _sideSpeed;
    [SerializeField] private float _inputSmoothing;

    [ShowInInspector]
    private float _smoothMoveInput;

    private float _xPosition;
    private bool _controlLock;

    public void Lock()
    {
        _controlLock = true;
        transform.LookAt(transform.position + new Vector3(0, 0, 1));
    }

    public void UnLock()
    {
        _controlLock = false;
    }

    private void Update()
    {
        float inputSmoothing = Mathf.Clamp01(_inputSmoothing * Time.deltaTime);

        float inputX = Input.GetAxis("Horizontal");
        _smoothMoveInput = Mathf.Lerp(_smoothMoveInput, inputX, inputSmoothing);

        _xPosition += _smoothMoveInput * _sideSpeed * Time.deltaTime;
        _xPosition = Mathf.Clamp(_xPosition, -1, 1);
        Vector3 transformVector = new Vector3(_xPosition, transform.position.y, transform.position.z);
        transform.position = transformVector;

        transform.LookAt(transform.position + new Vector3(_smoothMoveInput, 0, 1));
    }
}
