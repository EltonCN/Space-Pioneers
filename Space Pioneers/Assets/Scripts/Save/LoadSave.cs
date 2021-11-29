using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSave : MonoBehaviour
{
    [SerializeField] SaveManager saveManager;

    void Awake()
    {
        saveManager.load();
    }

    void OnDestroy()
    {
        saveManager.save();
    }
}
