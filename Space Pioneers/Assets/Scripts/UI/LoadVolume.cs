using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadVolume : MonoBehaviour
{
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider mainSlider;

    void OnEnable()
    {
        updateBoth();
    }

    void Start()
    {
        updateBoth();
    }

    void updateBoth()
    {
        if(musicSlider != null)
        {
            updateSlider(musicSlider, "music_volume");
        }
        if(mainSlider != null)
        {
            updateSlider(mainSlider, "main_volume");
        }      
    }

    void updateSlider(Slider slider, string prefName)
    {
        if(!PlayerPrefs.HasKey(prefName))
        {
            PlayerPrefs.SetFloat(prefName, 1f);
        }

        slider.value = PlayerPrefs.GetFloat(prefName);
    }
}
