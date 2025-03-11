using UnityEngine;

// Sprit - (Persona) - Entity - Part 아키텍처 중 선언형 관계관리 클래스입니다.
// 기본적으로는 뷰모델+프레젠터의 역할을 합니다.
// 클래스 압축성을 위해 Sprit에 모든 기능이 이양될 수 있습니다.
public abstract class Persona : MonoBehaviour { }
public abstract class BasePersona<U> : Persona where U : BaseEntity
{
    protected U entity;

    protected virtual void Awake()
    {
        entity = GetComponent<U>();
    }
}
