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

    public InteractableObject curInteractObject;
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
        }
    }
    public void SetPrompt(Prompt prompt)
    {
        this.prompt = prompt;
    }

    private void Update()
    {
        if (hpIndicator != null) hpIndicator.Indicate(entity.CurHP / entity.MaxHP);
        if (stIndicator != null) stIndicator.Indicate(entity.CurST / entity.MaxST);

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

        if (Physics.Raycast(ray, out RaycastHit hit, 5f, layerMask))
        {
            InteractableObject interactable = hit.collider.GetComponent<InteractableObject>();
            curInteractObject = interactable ? interactable : null;

            //프롬프트에 텍스트 추가
        }
        else
            curInteractObject = null;

        Debug.DrawRay(ray.origin, ray.direction * 5f, Color.green);

    }

    public void TryInteractaction(InteractableObject target)
    {
        if (target == null) return;
        target.OnInteraction(col);
    }   
}
