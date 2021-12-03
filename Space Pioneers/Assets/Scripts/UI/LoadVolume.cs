using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadVolume : MonoBehaviour
{
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider mainSlider;

    AudioSource musicAudio;
    AudioSource mainAudio;

    void OnEnable()
    {
        updateBoth();
    }

    void Start()
    {
        musicAudio = musicSlider.GetComponent<AudioSource>();
        mainAudio = mainSlider.GetComponent<AudioSource>();

        if(musicAudio != null)
        {
            musicAudio.enabled = false;
        }
        if(mainAudio != null)
        {
            mainAudio.enabled = false;
        }

        updateBoth();

        if(musicAudio != null)
        {
            musicAudio.enabled = true;
        }
        if(mainAudio != null)
        {
            mainAudio.enabled = true;
        }
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
