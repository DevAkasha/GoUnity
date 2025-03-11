using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Sprit - (Persona) - Entity - Part 아키텍처 중 객체 기반 클래스입니다.
// BasePart는 Sprit, Persona, Entity가 포함된 GameObject의 자식 GameObject에서 사용됩니다.
// Entity와 함께 순수 객체지향적 클래스로 유지됩니다.
public abstract class BasePart : MonoBehaviour, IEntityComponent
{
    protected BaseEntity entity;
    
    protected virtual void Awake()
    {
        entity = GetComponentInParent<BaseEntity>();
        if (entity != null)
        {
            if (entity.components == null)
            {
                Debug.LogWarning($"{entity.gameObject.name}의 components 리스트가 초기화되지 않았습니다. 초기화 후 등록합니다.");
                entity.components = new List<IEntityComponent>();
            }
            entity.RegisterComponent(this);
        }
        else
        {
            Debug.LogWarning($"{gameObject.name}의 BasePart가 BaseEntity 없이 동작합니다. 독립적으로 사용할 수 있도록 설계되었습니다.");
        }
    }
}

