using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

[Serializable]
public class EffectContainer
{
    private PickupEffect _effect;
    private float _lifeTime;
    private float _currentTime;

    public PickupEffect Effect { get => _effect; }

    public EffectContainer(PickupEffect effect)
    {
        _effect = effect;
        _lifeTime = effect.Duration;
        _currentTime = 0;
    }

    public void SetCurrentTime(float time)
    {
        _currentTime = time;
    }

    public bool AddToCurrentTime(float add)
    {
        _currentTime += add;
        return _currentTime >= _lifeTime;
    }

    public override bool Equals(object obj)
    {
        if (obj == null) return false;

        var cont = obj as EffectContainer;
        if (cont == null) return false;

        if (cont.Effect == _effect)
        {
            return true;
        }

        return false;
    }
}
