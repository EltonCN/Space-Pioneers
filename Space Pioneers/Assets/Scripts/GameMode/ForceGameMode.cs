using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceGameMode : MonoBehaviour
{
    [SerializeField] GameMode forcedGameMode;
    [SerializeField] GameModeState gameModeState;
    [SerializeField] GameEvent changeGameMode;

    public void Force()
    {
        if(gameModeState.actualGameMode != forcedGameMode)
        {
            Debug.Log("Forcing");
            changeGameMode.Raise();
        }
    }
}
