using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupTile : InteractiveTile
{
    [SerializeField] private PickupEffect _effect;

    public override void Activate(Larry character)
    {
        print("Hit " + character.name);
        character.AddEffect(_effect);
        gameObject.SetActive(false);
    }
}
