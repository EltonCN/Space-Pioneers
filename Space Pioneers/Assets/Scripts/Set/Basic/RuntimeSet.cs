
// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------


using System.Collections.Generic;
using UnityEngine;

public abstract class RuntimeSet<T> : ScriptableObject
{
    public List<T> Items = new List<T>();
    private int itemCount = 0;
    [SerializeField] bool notResetOnEnable = false;

    public void Add(T thing)
    {
        if (!Items.Contains(thing))
            Items.Add(thing);
        
        itemCount = Items.Count;
    }

    public void Remove(T thing)
    {
        if (Items.Contains(thing))
            Items.Remove(thing);

        itemCount = Items.Count;
    }

    public void Reset()
    {
        Items = new List<T>();
        itemCount = 0;
    }

    public int ItemCount
    {
        get
        {
            itemCount = Items.Count;
            return itemCount;
        }
    }

    public void OnEnable()
    {
        if(!notResetOnEnable)
        {
            Reset();
        }
        
    }
}