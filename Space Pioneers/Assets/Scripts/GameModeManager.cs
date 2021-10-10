using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[AddComponentMenu("SpacePioneers/GameMode/Manager")]
public class GameModeManager : MonoBehaviour
{
    public enum GameMode {PLANNING, ACTION};

    private static GameModeManager globalManager;

    public event Action OnEnterPlanningMode, OnEnterActionMode;

    private GameModeManager()
    {
    }

    public static GameModeManager Instance
    {
        get
        {
            if(globalManager == null)
            {
                globalManager = new GameModeManager();
            }

            return globalManager;
        }
    }

    public void changeGameMode(GameMode mode)
    {
        if(mode == GameMode.ACTION)
        {
            OnEnterActionMode();
        }
        else if (mode == GameMode.PLANNING)
        {
            OnEnterPlanningMode();
        }
    }
}
