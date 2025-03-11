using UnityEngine;


[CreateAssetMenu(fileName ="NewObject",menuName ="Interactable/Stuff")]
public class InteractableStuffData : InteractableData
{
    [Header("스터프 프로퍼티")]
    public InteractableStuffType StuffType;
}
