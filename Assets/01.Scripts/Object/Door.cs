using UnityEngine;

public class Door : InteractableStuff
{
    protected override void ApplyPlayer(PlayerSprit player)
    {
        Debug.Log("플레이어와 상호작용했어");
    }

    protected override void OccupidPlayer(PlayerSprit player)
    {
        Debug.Log("플레이어와 상호작용했어");
    }
}
