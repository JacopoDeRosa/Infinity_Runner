using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Audio Loop", menuName = "Audio/New Audio Loop")]
public class LoopableAudioClip : ScriptableObject
{
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private float _introLenght;
    [SerializeField] private Vector2[] _loopStops;

    public Vector2[] LoopStops { get => _loopStops; }
    public float IntroLenght { get => _introLenght; }
    public AudioClip AudioClip { get => _audioClip; }
}
