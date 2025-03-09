using UnityEngine;

public enum InteractableStuffType { Consumable, Functional, Ontime, Persistent }

[CreateAssetMenu(fileName ="NewObject",menuName ="Interactable/Stuff")]
public class InteractableStuffData : InteractableData
{
    [Header("스터프 프로퍼티")]
    public InteractableStuffType StuffType;
}
