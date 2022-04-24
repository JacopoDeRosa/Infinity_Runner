using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Timer : MonoBehaviour, IReloadable
{
    public static string ToClockFormat(float time)
    {

        int seconds = (int)time % 60;
        int minutes = (int)time / 60;

        string tMinutes = minutes.ToString().PadLeft(2, '0');
        string tSeconds = seconds.ToString().PadLeft(2, '0');

        return tMinutes + ":" + tSeconds;

    }

    [SerializeField] private TMP_Text _text;

    private float _currentTime;

    private void Update()
    {
        _currentTime += Time.deltaTime;
        _text.text = ToClockFormat(_currentTime);
    }

    public void Reload()
    {
        _currentTime = 0;
    }
}
