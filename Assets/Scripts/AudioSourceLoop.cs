using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceLoop : MonoBehaviour
{
    [SerializeField] private float _introLenght;
    [SerializeField] private Vector2[] _loopables;
    [SerializeField] private AudioSource _audioSource;

    private bool _introStarted;

    private int _currentLoopable;

    private Vector2 CurrentLoopable { get => _loopables[_currentLoopable]; }

    private void Start()
    {
        _currentLoopable = Random.Range(0, _loopables.Length);
    }

    private void Update()
    {
        if(_introStarted == false)
        {
            if(_audioSource.time >= _introLenght)
            {
                _introStarted = true;
                _audioSource.time = CurrentLoopable.x;
            }

            return;
        }

        if(_audioSource.time >= CurrentLoopable.y)
        {
            _currentLoopable = Random.Range(0, _loopables.Length);
            _audioSource.time = CurrentLoopable.x;
        }
    }
}
