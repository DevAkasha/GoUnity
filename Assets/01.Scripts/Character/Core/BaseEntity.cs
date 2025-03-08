using System.Collections.Generic;
using UnityEngine;

public interface IEntityComponent { }

public abstract class BaseEntity : MonoBehaviour
{
    protected List<IEntityComponent> components = new List<IEntityComponent>();

    protected virtual void Awake()
    {
        components.AddRange(GetComponents<IEntityComponent>());
    }

}
