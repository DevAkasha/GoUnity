using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : InteractableObject
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].normal.y < -0.5f)
        {
            Debug.Log("위쪽에 출돌했음!");
            isRayInteractable = true;
            OnInteraction(collision.collider);
        }
        else Debug.Log("위가 아님!!");
    }
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
        player.entity.rb.velocity = new Vector3(player.entity.rb.velocity.x,0f, player.entity.rb.velocity.z);
        player.entity.rb.AddForce(new Vector3(0f, 1000f, 0f), ForceMode.Impulse);
    }

    protected override void OccupidNPC(NPCSprit NPC)
    {
        throw new System.NotImplementedException();
    }

    protected override void OccupidPlayer(PlayerSprit player)
    {
        throw new System.NotImplementedException();
    }

}
