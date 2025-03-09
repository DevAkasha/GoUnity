using UnityEngine;
using System;

public abstract class BaseSprit<T,U> : MonoBehaviour where T : BasePersona<U> where U : BaseEntity
{
    // Sprit : 컨트롤러  persona : 프레젠터  entity : 모델+기능 
    public T persona;
    public U entity;

    protected virtual void Awake()
    {
        persona = GetComponent<T>();
        entity = GetComponent<U>();
    }
}
