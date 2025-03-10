using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealFactor: InteractableStuff
{
    protected override void ApplyPlayer(PlayerSprit player)
    {
        player.entity.Heal(20f);
    }
}
