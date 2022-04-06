using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class LarryLaneController : MonoBehaviour
{
    [SerializeField] private float _sideSpeed;

    private float _xPosition;

    private void Update()
    {
        float inputX = Input.GetAxis("Horizontal");

        _xPosition += inputX * _sideSpeed * Time.deltaTime;
        _xPosition = Mathf.Clamp(_xPosition, -1, 1);
        Vector3 transformVector = new Vector3(_xPosition, transform.position.y, transform.position.z);
        transform.position = transformVector;

       transform.LookAt(transform.position + new Vector3(inputX, 0, 1));
    }
}
