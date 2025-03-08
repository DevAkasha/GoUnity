using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum StateType { HP,ST}
public class StateIndicator : MonoBehaviour
{
    [SerializeField] Image valueImg;
    public StateType type;

    private void Start()
    {
        CharacterManager.Instance.Player.persona.SetStateIndicator(this);
    }
    
    public void Indicate(float value)
    {
        valueImg.fillAmount = value;
    }

}
