using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour, IReloadable
{
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private PlayerCharacter _mainCharacter;

    private void Awake()
    {
        _mainCharacter.onDeath += OnDeath;
    }

    private void Start()
    {
        _gameOverScreen.SetActive(false);
    }

    private void OnDeath()
    {
        _gameOverScreen.SetActive(true);
    }

    public void Reload()
    {
        Start();
    }
}
