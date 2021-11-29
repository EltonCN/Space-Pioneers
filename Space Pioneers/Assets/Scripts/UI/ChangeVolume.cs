using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeVolume : MonoBehaviour
{
    public float MainVolume
    {
        get
        {
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
            return PlayerPrefs.GetFloat("music_volume");
        }
        set
        {
            float clamped = Mathf.Clamp(value, 0f, 1f);
            PlayerPrefs.SetFloat("music_volume", clamped);
        }
    }
}
