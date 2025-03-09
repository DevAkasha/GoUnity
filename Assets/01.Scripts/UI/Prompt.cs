using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Prompt : MonoBehaviour
{
    TextMeshProUGUI text;
    public void SetText(string contents)
    {
        text.text = contents;
    }
}
