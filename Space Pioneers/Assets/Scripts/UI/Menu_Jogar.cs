using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Menu_Jogar : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject next_canvas;
    
    [SerializeField] VideoPlayer start_player;
    [SerializeField] VideoPlayer end_player;


    public void play_and_next()
    {
        StartCoroutine("PlaySequence");
    }

    void OnEnable()
    {
        end_player.Prepare();
        start_player.playbackSpeed = 1;
        end_player.playbackSpeed = 5;
    }

    IEnumerator PlaySequence()
    {
        start_player.playbackSpeed *= 10;
        while(start_player.isPlaying)
        {
            yield return null;
        }

        while(!end_player.isPrepared)
        {
            yield return null;
        }

        end_player.Play();
        
        while(end_player.isPlaying)
        {
            yield return null;
        }

        canvas.SetActive(false);
        next_canvas.SetActive(true);

    }
}
