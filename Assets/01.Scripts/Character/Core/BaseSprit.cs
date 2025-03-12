using UnityEngine;

// Sprit - (Persona) - Entity - Part 아키택처 중 명령형 컨트롤플로우 클래스입니다.
// 기본적으로는 이벤트핸들러 및 컨트롤러를 담당합니다.
// 클래스 압축성을 위해 BasePersona를 이용해 Persona없이 사용될 수 있습니다. 이 경우 뷰모델 프레젠터 역할도 같이할 수 있습니다.
public abstract class BaseSprit<T, U> : MonoBehaviour where T : Persona where U : BaseEntity
{
    public T Persona { get; private set; }
    public U Entity { get; private set; }

    protected virtual void Awake()
    {
        Persona = GetComponent<T>();
        if (Persona == null)
        {
            Debug.LogError($"{typeof(U).Name}이 존재하지 않습니다. {gameObject.name}의 BaseSprit이 올바르게 설정되지 않았습니다.");
        }
        Entity = GetComponent<U>();
        if (Entity == null)
        {
            Debug.LogWarning($"{typeof(T).Name}이 존재하지 않습니다. Sprit이 독립적으로 동작합니다.");
        }
    }

}