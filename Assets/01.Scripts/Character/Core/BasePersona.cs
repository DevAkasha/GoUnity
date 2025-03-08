using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BasePersona<U> : MonoBehaviour where U : BaseEntity
{
    protected U entity;
    
    protected virtual void Awake()
    {
        entity = GetComponent<U>();
    }

}
