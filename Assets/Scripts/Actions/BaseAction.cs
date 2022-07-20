using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAction : MonoBehaviour
{
    protected bool isActive;

    protected Unit unit;

    protected Action onActionComplete;

    protected virtual void Awake()
    {
        unit = this.GetComponent<Unit>();
    }
}