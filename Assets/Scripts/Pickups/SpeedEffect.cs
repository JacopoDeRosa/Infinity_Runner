using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Speed Effect", menuName = "Pickups/New Speed Effect")]
public class SpeedEffect : PickupEffect
{
    [SerializeField] private float _speedChange;

    public override void Begin(Larry character)
    {
        character.ChangeSpeed(_speedChange);
    }

    public override void End(Larry character)
    {
        character.ChangeSpeed(-_speedChange);
    }
}
