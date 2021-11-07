using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialGameMode : MonoBehaviour
{
    [Tooltip("Global game mode state of the level.")] [SerializeField] private GameModeState globalState;
    [Tooltip("Event to raise on game mode change.")] [SerializeField] private GameEvent onGameModeChange;
    [Tooltip("First game mode.")] [SerializeField] private GameMode initialGameMode;

    void Awake()
    {
        globalState.actualGameMode = initialGameMode;
        onGameModeChange.Raise();

        Destroy(this.gameObject);
    }
}
