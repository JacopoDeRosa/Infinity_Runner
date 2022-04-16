using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Heart : MonoBehaviour
{
    [SerializeField] private Sprite _fullSprite;
    [SerializeField] private Sprite _emptySprite;
    [SerializeField] private Image _image;


    public void SetFull()
    {
        _image.sprite = _fullSprite;
    }

    public void SetEmpty()
    {
        _image.sprite = _emptySprite;
    }

    private void OnValidate()
    {
        if(_fullSprite != null)
        {
            _image.sprite = _fullSprite;
        }
            
    }
}
