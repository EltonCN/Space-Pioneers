
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
    public int ItemCount = 0;

    public void Add(T thing)
    {
        if (!Items.Contains(thing))
            Items.Add(thing);
        
        ItemCount = Items.Count;
    }

    public void Remove(T thing)
    {
        if (Items.Contains(thing))
            Items.Remove(thing);

        ItemCount = Items.Count;
    }

    public void Reset()
    {
        Items = new List<T>();
        ItemCount = 0;
    }
}