using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceLoop : MonoBehaviour
{
    [SerializeField] private LoopableAudioClip _activeClip;
    [SerializeField] private AudioSource _audioSource;

    private bool _introStarted;

    private int _currentLoopable;

    private Vector2 CurrentLoopable { get => _activeClip.LoopStops[_currentLoopable]; }

    private void Start()
    {
        if (_activeClip != null) SetNewClip(_activeClip);
    }

    private void Update()
    {
        if (_activeClip == null) return;

        if(_introStarted == false)
        {
            if(_audioSource.time >= _activeClip.IntroLenght)
            {
                _introStarted = true;
                _audioSource.time = CurrentLoopable.x;
            }

            return;
        }

        if(_audioSource.time >= CurrentLoopable.y)
        {
            _currentLoopable = Random.Range(0, _activeClip.LoopStops.Length);
            _audioSource.time = CurrentLoopable.x;
        }

    }
    public void SetNewClip(LoopableAudioClip clip)
    {
        _audioSource.Stop();
        _audioSource.time = 0;
        _audioSource.clip = clip.AudioClip;
        _introStarted = false;
        _currentLoopable = Random.Range(0, _activeClip.LoopStops.Length);
        _audioSource.Play();
    }
    
}
