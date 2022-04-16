using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopableAudioClip : MonoBehaviour
{
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private float _introLenght;
    [SerializeField] private Vector2[] _loopStops;
}
