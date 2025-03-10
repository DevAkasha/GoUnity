using UnityEngine;

public enum InteractionType { Collision, Trigger, Both}
public enum InteractalbeCategory { Item, Stuff }



public abstract class InteractableData : ScriptableObject
{
    [Header("공통 프로퍼티")]
    public string itemName;
    public string description;
    public InteractionType interactionType;
    public InteractalbeCategory interactionCategory;
    public bool isDisposable; 
    public float destroyDelay;
}
