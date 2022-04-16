using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Lives : MonoBehaviour
{
    [SerializeField] private Health _targetHp;
    [SerializeField] private UI_Heart[] _hearts;


    private void Awake()
    {
        _targetHp.onHpChanged += ChangeLives;
    }


    private void ChangeLives(int lives)
    {
        foreach (UI_Heart heart in _hearts)
        {
            heart.SetEmpty();
        }

        for (int i = 0; i < lives; i++)
        {
            _hearts[i].SetFull();
        }
    }
}
