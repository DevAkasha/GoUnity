using UnityEngine;
using System;

public abstract class BaseSprit<T,U> : MonoBehaviour where T : BasePersona<U> where U : BaseEntity
{
    public T persona;
    public U entity;

    protected virtual void Awake()
    {
        persona = GetComponent<T>();
        entity = GetComponent<U>();
    }
}
