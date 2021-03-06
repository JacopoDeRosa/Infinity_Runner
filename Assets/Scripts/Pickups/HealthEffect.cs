using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Heal Effect", menuName = "Pickups/New Heal Effect")]
public class HealthEffect : PickupEffect
{
    [SerializeField] private int _heal;

    public override void Begin(PlayerCharacter character)
    {
        character.HealDamage(_heal);
    }

    public override void End(PlayerCharacter character)
    {

    }
}
