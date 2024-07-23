using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mission : ScriptableObject
{
    [HideInInspector] public Customer customer;
    public abstract void Start();
    public Action<bool> OnAdvanceStatusChanged;
    protected abstract void OnStart();
    protected abstract void OnEnd();
    public abstract void Advance();
}
