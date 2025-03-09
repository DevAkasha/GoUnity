using UnityEngine;
public abstract class Interactable : MonoBehaviour 
{
    public abstract void OnInteraction(Collider collider);
    public abstract string GetPromptString();
}

[RequireComponent(typeof(Collider))]
public abstract class Interactable<T> : Interactable where T : InteractableData
{
    public T data;

    public Collider col;
    private bool isDestroyed = false;

    [SerializeField] protected bool isCollisonInteract;

    public bool isRayInteractable;
    public bool primer;

    [SerializeField] protected bool isOccupidable;
    [SerializeField] protected float occupidDestroyDelay = 0.1f;

    [SerializeField] protected bool isDisposable;
    [SerializeField] protected float applyDestroyDelay = 0.1f;

    protected virtual void OccupidPlayer(PlayerSprit player) { }
    protected virtual void ApplyPlayer(PlayerSprit player) { }
    protected virtual void OccupidNPC(NPCSprit npc) { }
    protected virtual void ApplyNPC(NPCSprit npc) { }
    protected virtual void ApplyEnviroment(GameObject Target) { }

    protected virtual void Awake()
    {
        col = GetComponent<Collider>();
    }

    public override void OnInteraction(Collider collider)
    {
        if (isRayInteractable) OnTriggerEnter(collider);
    }
    public override string GetPromptString()
    {
        return $"{data.itemName}\n{data.description}";
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isCollisonInteract) OnTriggerEnter(collision.collider);
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
                    if (isDisposable) DestroyAfterDelay(applyDestroyDelay);
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
                if (isDisposable) DestroyAfterDelay(applyDestroyDelay);
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

