using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixerControl : MonoBehaviour
{
    public AudioMixer theMixer;

    void Start()
    { 
        if(!PlayerPrefs.HasKey("main_volume"))
            PlayerPrefs.SetFloat("main_volume", 1f);
        
        if(!PlayerPrefs.HasKey("music_volume"))
            PlayerPrefs.SetFloat("music_volume", 1f);

        UpdateVolume();
    }
    
    public void  UpdateVolume() 
    {
        setMainVol();
        setMusicVol();
    }

    public void setMainVol() 
    {
        theMixer.SetFloat("SfxVol", -80*(1-PlayerPrefs.GetFloat("main_volume")));
    }
    
    public void setMusicVol()
    {
        theMixer.SetFloat("MusicVol", -80*(1-PlayerPrefs.GetFloat("music_volume")));
    }
}
