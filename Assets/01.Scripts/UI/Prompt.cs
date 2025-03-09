using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Prompt : MonoBehaviour
{
    TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void Start()
    {
        CharacterManager.Instance.Player.persona.SetPrompt(this);
    }
    public void SetText(string contents)
    {
        text.text = contents;
    }
}
