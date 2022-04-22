using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTile : InteractiveTile
{
    [SerializeField] private int _damage;
    [SerializeField] private Stance[] _immuneStances;

    public override void Activate(PlayerCharacter character)
    {
        foreach (Stance stance in _immuneStances)
        {
            if(character.CurrentStance == stance)
            {
                return;
            }          
        }
        character.DealDamage(_damage);
    }
}
