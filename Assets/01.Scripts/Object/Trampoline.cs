using UnityEngine;

public class Trampoline : InteractableStuff
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].normal.y < -0.5f)//위에서 충돌시에 조건 true
        {
            Debug.Log("위쪽에 출돌했음!");
            isRayInteractable = true;
            OnInteraction(collision.collider);
        }
        else Debug.Log("위가 아님!!");
    }
   
    protected override void ApplyPlayer(PlayerSprit player)
    {
        player.Entity.rb.velocity = new Vector3(player.Entity.rb.velocity.x,0f, player.Entity.rb.velocity.z);
        player.Entity.rb.AddForce(new Vector3(0f, 1000f, 0f), ForceMode.Impulse);
    }

}
