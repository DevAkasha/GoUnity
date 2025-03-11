using UnityEngine;

//소재철 튜터님 프레임워크 스크립트
public abstract class UIBase : MonoBehaviour
{
    public eUIPosition uiPosition;

    //params :파라미터 여러개를 묶어서 처리가능, 오버로드보다 호출우선도는 떨어지지만 유연성이 강력함
    public abstract void Opened(params object[] param);
    public abstract void HideDirect();
}

