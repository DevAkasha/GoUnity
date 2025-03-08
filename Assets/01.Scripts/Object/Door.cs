using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableObject
{
    protected override void ApplyEnviroment()
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
