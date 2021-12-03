using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeVolume : MonoBehaviour
{
    
    public float MainVolume
    {
        get
        {
            if(!PlayerPrefs.HasKey("main_volume"))
            {
                PlayerPrefs.SetFloat("main_volume", 1f);
            }
            
            return PlayerPrefs.GetFloat("main_volume");
        }
        set
        {
            float clamped = Mathf.Clamp(value, 0f, 1f);
            PlayerPrefs.SetFloat("main_volume", clamped);
        }
    }

    public float MusicVolume
    {
        get
        {
            if(!PlayerPrefs.HasKey("music_volume"))
            {
                PlayerPrefs.SetFloat("music_volume", 1f);
            }

            return PlayerPrefs.GetFloat("music_volume");
        }
        set
        {
            float clamped = Mathf.Clamp(value, 0f, 1f);
            PlayerPrefs.SetFloat("music_volume", clamped);
        }
    }
}
