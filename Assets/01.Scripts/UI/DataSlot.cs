using TMPro;
using UnityEngine;

public class DataSlot : MonoBehaviour
{
    TextMeshProUGUI icon;

    private void Start()
    {
        CharacterManager.Instance.Player.persona.SetDataSlot(this);
    }

    public void SetIcon(string name)
    {
        icon.text = name;
    }
}
