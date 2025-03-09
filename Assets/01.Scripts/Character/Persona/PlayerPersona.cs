using System.Collections;
using System.Collections.Generic;
using UnityChan;
using UnityEngine;

public class PlayerPersona : BasePersona<DUnityChan>
{
    private StateIndicator hpIndicator;
    private StateIndicator stIndicator;
    private Prompt prompt;

    [SerializeField] private GameObject lookAtPos;
    [SerializeField] private Camera mainCamera; 
    [SerializeField] private ThirdPersonCamera mainCameraCS;

    private float interactionRate = 0.05f;
    private float lastCheckTime;
    public LayerMask layerMask;

    public Interactable curInteractObject;
    public Collider col;

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

    private void Update()
    {
        hpIndicator.Indicate(entity.CurHP / entity.MaxHP);
        stIndicator.Indicate(entity.CurST / entity.MaxST);

        if (Time.time - lastCheckTime > interactionRate)
        {
            lastCheckTime = Time.time;
            CheckInteractable();
        }
    }

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

    public void TryInteractaction(Interactable target)
    {
        if (target == null) return;
        target.OnInteraction(col);
    }   
}
