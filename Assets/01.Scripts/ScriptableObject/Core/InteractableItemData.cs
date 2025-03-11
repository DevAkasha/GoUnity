using UnityEngine;
public enum InteractableItemType { Resource, Equipment, Available }

[CreateAssetMenu(fileName ="newItem",menuName = "Interactable/Item")]
public class InteractableItemData :InteractableData
{
    [Header("아이템 프로퍼티")]
    public InteractableItemType itemType;
    public Sprite icon;
    public int maxStackCount;
    public GameObject prefab;

}

