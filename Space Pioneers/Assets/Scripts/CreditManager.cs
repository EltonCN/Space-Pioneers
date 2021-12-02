using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CreditManager : MonoBehaviour
{
    public VideoPlayer startCredits;
    public GameObject scrollingCredits;
    void Start() {
        startCredits.loopPointReached += EndReached;    
    }
    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        vp.playbackSpeed = vp.playbackSpeed / 10.0F;
        scrollingCredits.SetActive(true);
    }
}
