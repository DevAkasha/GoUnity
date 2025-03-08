using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class InteractableObject : MonoBehaviour
{
    public Collider col;

    public float destroyDelay = 0.1f;
    private bool isDestroyed = false;
    public bool primer = false;

    protected abstract void OccupidPlayer(PlayerSprit player);
    protected abstract void ApplyPlayer(PlayerSprit player);
    protected abstract void OccupidNPC(NPCSprit NPC);
    protected abstract void ApplyNPC(NPCSprit NPC);
    protected abstract void ApplyEnviroment();

    protected virtual void Awake()
    {
        col = GetComponent<Collider>();
    }

    public void OnInteraction(Collider collision) 
    {
        OnTriggerEnter(collision);
        Debug.Log("상호작용 시작!!");
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (isDestroyed) return;
        Debug.Log("상호작용 완료!!");
        switch (collision.tag)
        {
            case "Player":
                PlayerSprit player = collision.GetComponent<PlayerSprit>();
                if (!primer) ApplyPlayer(player);
                else 
                {
                    OccupidPlayer(player);
                    DestroyAfterDelay();
                }
                break;

            case "NPC":
                NPCSprit NPC = collision.GetComponent<NPCSprit>();
                if (!primer) ApplyNPC(NPC);
                else
                {
                    OccupidNPC(NPC);
                    DestroyAfterDelay();
                }
                break;

            case "Enviroment":
                ApplyEnviroment();
                DestroyAfterDelay();
                break;
            default: 
                Debug.LogWarning($"{name}이 예상못한 객체({collision.name})에 충돌했어!");
                break;
        }
    }

    private void DestroyAfterDelay()
    {
        isDestroyed = true;
        Destroy(gameObject, destroyDelay);
    }
}

