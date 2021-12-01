using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraGameModeListener : GameEventListener
{
    [SerializeField] private GameModeState globalState;
    //public CinemachineVirtualCamera virtualCamera;
    public CinemachineBrain cinemachineBrain;
    public void SwitchVirtualCamera() {
        if (globalState.actualGameMode == GameMode.ACTION) {
            cinemachineBrain.enabled = true;
            //virtualCamera.enabled = true;
        } else if(globalState.actualGameMode == GameMode.PLANNING) {
            cinemachineBrain.enabled = false;
            //virtualCamera.enabled = false;
        }
    }
}
