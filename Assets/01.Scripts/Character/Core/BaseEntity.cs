using System.Collections.Generic;
using UnityEngine;

// Sprit - (Persona) - Entity - Part 아키택처 중 명령형 객체 클래스입니다.
// 모델과 기능을 담당하는 순수 객체지향적 클래스의 토대입니다.
// 객체성을 계속 유지해야 하는 클래스입니다.

public interface IEntityComponent { }

public abstract class BaseEntity : MonoBehaviour
{
    public List<IEntityComponent> components = new List<IEntityComponent>();

    protected virtual void Awake()
    {
        components.AddRange(GetComponents<IEntityComponent>());
    }

    public void RegisterComponent(IEntityComponent component)
    {
        if (!components.Contains(component))
        {
            components.Add(component);
        }
    }
#if UNITY_EDITOR
    protected virtual void OnValidate()
    {
        components.Clear();
        components.AddRange(GetComponents<IEntityComponent>());
    }
#endif
}

