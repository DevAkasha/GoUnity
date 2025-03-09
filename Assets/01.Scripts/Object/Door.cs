using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableStuff
{
    protected override void ApplyEnviroment(GameObject target)
    {
        throw new System.NotImplementedException();
    }

    protected override void ApplyNPC(NPCSprit NPC)
    {
        throw new System.NotImplementedException();
    }

    protected override void ApplyPlayer(PlayerSprit player)
    {
        Debug.Log("플레이어와 상호작용했어");
    }

    protected override void OccupidNPC(NPCSprit NPC)
    {
        throw new System.NotImplementedException();
    }

    protected override void OccupidPlayer(PlayerSprit player)
    {
        Debug.Log("플레이어와 상호작용했어");
    }
}
