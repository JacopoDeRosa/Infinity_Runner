using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Lives : MonoBehaviour
{
    [SerializeField] private Health _targetHp;
    [SerializeField] private TextMeshProUGUI _tmPro;


    private void Awake()
    {
        _targetHp.onHpChanged += ChangeLives;
        _tmPro.text = _targetHp.CurrentHp.ToString();
    }


    private void ChangeLives(int lives)
    {
        _tmPro.text = lives.ToString();
    }
}
