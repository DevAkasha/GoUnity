using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableItem : Interactable<InteractableItemData>
{
    protected override void OccupidPlayer(PlayerSprit player)
    {
        //player.AddItem(data);
    }
    protected override void OccupidNPC(NPCSprit npc)
    {
        //npc.AddItem(data);
    }
}
