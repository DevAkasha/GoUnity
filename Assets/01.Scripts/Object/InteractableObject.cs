using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class InteractableObject : MonoBehaviour
{
    public Collider col;
    private bool isDestroyed = false;

    public bool isRayInteractable = false;
    public bool primer = false;

    public bool isOccupidable = false;
    public float occupidDestroyDelay = 0.1f;

    public bool isDisposable = false;
    public float applyDestroyDelay = 0.1f;

 

    protected abstract void OccupidPlayer(PlayerSprit player);
    protected abstract void ApplyPlayer(PlayerSprit player);
    protected abstract void OccupidNPC(NPCSprit npc);
    protected abstract void ApplyNPC(NPCSprit npc);
    protected abstract void ApplyEnviroment(GameObject Target);

    protected virtual void Awake()
    {
        col = GetComponent<Collider>();
    }

    public void OnInteraction(Collider collider) 
    {
        if (isRayInteractable) OnTriggerEnter(collider);
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnTriggerEnter(collision.collider);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (isDestroyed) return;
        Debug.Log("상호작용 완료!!");
        switch (collider.tag)
        {
            case "Player":
                PlayerSprit player = collider.GetComponent<PlayerSprit>();
                if (primer)
                {
                    ApplyPlayer(player);
                    if(isDisposable) DestroyAfterDelay(applyDestroyDelay);
                }

                else
                {
                    if (isOccupidable)
                    {
                        OccupidPlayer(player);
                        DestroyAfterDelay(occupidDestroyDelay);
                    }
                }
                break;

            case "NPC":
                NPCSprit NPC = collider.GetComponent<NPCSprit>();
                if (primer)
                {
                    ApplyNPC(NPC);
                    if (isDisposable) DestroyAfterDelay(applyDestroyDelay);
                }
                else
                {
                    if (isOccupidable) 
                    {
                        OccupidNPC(NPC);
                        DestroyAfterDelay(occupidDestroyDelay);
                    }

                }
                break;

            case "Enviroment":
                    ApplyEnviroment(collider.gameObject);
                    if(isDisposable) DestroyAfterDelay(applyDestroyDelay);
                break;
            default: 
                Debug.LogWarning($"{name}이 예상못한 객체({collider.name})와 상호작용 했어");
                break;
        }
    }

    private void DestroyAfterDelay(float delay)
    {
        isDestroyed = true;
        Destroy(gameObject, delay);
    }
}

