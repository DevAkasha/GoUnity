using UnityEngine;

// 인터페이스 및 비제네릭 부모클래스의 이용 https://devakasha.tistory.com/72 참고

public abstract class Interactable : MonoBehaviour 
{
    public abstract void OnInteraction(Collider collider);
    public abstract string GetPromptString();
}


//상호작용 오브젝트의 가장 기본 클래스입니다.
//Collider의 충돌이나 트리거를 감지하여 상태에 따라 행동을 분기하고 메서드를 호출합니다.
[RequireComponent(typeof(Collider))]
public abstract class Interactable<T> : Interactable where T : InteractableData
{
    // 기본 구성
    public T data;
    public Collider col;
    private bool isDestroyed = false;

    //아이템세팅
    [SerializeField] protected bool isCollisonInteract;
    public bool isRayInteractable;
    public bool primer; //효과가 적용되게 활성된 상태인지 습득가능한 비활성 상태인지 확인하는 bool값
    [SerializeField] protected bool isOccupidable;
    [SerializeField] protected float occupidDestroyDelay = 0.05f;
    [SerializeField] protected bool isDisposable;
    [SerializeField] protected float applyDestroyDelay = 0.05f;

    protected virtual void OccupidPlayer(PlayerSprit player) { }
    protected virtual void ApplyPlayer(PlayerSprit player) { }
    protected virtual void OccupidNPC(NPCSprit npc) { }
    protected virtual void ApplyNPC(NPCSprit npc) { }
    protected virtual void ApplyEnviroment(GameObject Target) { }

    protected virtual void Awake()
    {
        col = GetComponent<Collider>();
        initiate();
    }

    public override void OnInteraction(Collider collider)
    {
        if (isRayInteractable) OnTriggerEnter(collider);
    }

    public override string GetPromptString()
    {
        return $"{data.itemName}\n{data.description}";
    }

    protected virtual void initiate() //스크립터블오브젝트의 data를 초기화
    {
        isCollisonInteract = data.interactionType == InteractionType.Collision || data.interactionType == InteractionType.Both;
        isRayInteractable = data.interactionType == InteractionType.Trigger || data.interactionType == InteractionType.Both;
        primer = data.interactionCategory == InteractalbeCategory.Stuff;
        isOccupidable = data.interactionCategory == InteractalbeCategory.Item;
        isDisposable = data.isDisposable;
    }

    // OnCollisionEnter를 OnTriggerEnter로 변환해서 연결
    private void OnCollisionEnter(Collision collision)
    {
        if (isCollisonInteract) OnTriggerEnter(collision.collider);
    }

    //상태에 따른 행동분기 및 메서드 호출
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

    //딜레이 파괴기능
    private void DestroyAfterDelay(float delay)
    {
        isDestroyed = true;
        Destroy(gameObject, delay);
    }
}

