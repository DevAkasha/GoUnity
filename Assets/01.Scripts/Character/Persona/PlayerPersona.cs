using UnityChan;
using UnityEngine;

public class PlayerPersona : BasePersona<DUnityChan>
{
    //플레이어 종속 뷰
    private StateIndicator hpIndicator;
    private StateIndicator stIndicator;
    private Prompt prompt;
    private DataSlot slot;

    //카메라 전환 관련
    [SerializeField] private GameObject lookAtPos;
    [SerializeField] private Camera mainCamera; 
    [SerializeField] private ThirdPersonCamera mainCameraCS;

    //상호작용 관련
    private float interactionRate = 0.05f;
    private float lastCheckTime;
    public Interactable curInteractObject;
    public Collider col;

    private InteractableData curData;
    public InteractableData CurData
    {
        get { return curData; }
        private set 
        {
            curData = value;
            if (curData = null) slot.SetIcon("");
            else slot.SetIcon(CurData.itemName);
        }
    }

    //데이터 저장
    public void AddItem(InteractableData data)
    {
        CurData = data;
    }

    // 플레이어 종속 뷰 등록(추후 델리게이트 사용 리펙토링 고안)
    public void SetStateIndicator(StateIndicator indicator)
    {
        switch (indicator.type)
        {
            case StateType.HP:
                hpIndicator = indicator;
                break;
            case StateType.ST:
                stIndicator = indicator;
                break;
            default:
                Debug.LogWarning($"{indicator.name}이 set되지 않았어!");
                break;
        }
    }
    public void SetPrompt(Prompt prompt)
    {
        this.prompt = prompt;
        if (this.prompt==null) Debug.LogWarning($"{prompt.name}이 set되지 않았어!");
    }
    public void SetDataSlot(DataSlot dataSlot)
    {
        slot = dataSlot;
        if (slot == null) Debug.LogWarning($"{dataSlot.name}이 set되지 않았어!");
    }

    private void Update()
    {
        //종속 뷰 갱신
        hpIndicator.Indicate(entity.CurHP / entity.MaxHP);
        stIndicator.Indicate(entity.CurST / entity.MaxST);

        if (Time.time - lastCheckTime > interactionRate)
        {
            lastCheckTime = Time.time;
            CheckInteractable();
        }
    }

    //인터렉션 확인
    private void CheckInteractable()
    {
        if (lookAtPos == null) return;
        
        Ray ray;
        if (mainCameraCS.CameraPositionIndex == 2)
            ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        else 
            ray = new Ray(lookAtPos.transform.position, lookAtPos.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, 5f))
        {
            //제네릭은 GetComponent가 불가능 부모클래스로 실행
            curInteractObject = hit.collider.GetComponent<Interactable>();

            if (curInteractObject != null)
            {
                prompt.SetText(curInteractObject.GetPromptString());
                prompt.gameObject.SetActive(true);
            }
        }
        else
            curInteractObject = null;
        
        if (curInteractObject == null)
        {
            prompt.SetText("");
            prompt.gameObject.SetActive(false);
        }

        Debug.DrawRay(ray.origin, ray.direction * 5f, Color.green);

    }

    //인터렉션 시도
    public void TryInteractaction(Interactable target)
    {
        if (target == null) return;
        target.OnInteraction(col);
    }   
}
