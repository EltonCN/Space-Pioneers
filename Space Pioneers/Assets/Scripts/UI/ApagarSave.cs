using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApagarSave : MonoBehaviour
{
    [SerializeField] SaveManager saveManager;
    private int save_index;

    public void deletar()
    {  
        saveManager.delete(save_index);
    }

    public int SaveIndex
    {
        set
        {
            save_index = value;
        }
        get
        {
            return save_index;
        }
    }
}
