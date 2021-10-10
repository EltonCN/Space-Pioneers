using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[AddComponentMenu("SpacePioneers/GameMode/Manager")]
public class GameModeManager : MonoBehaviour
{
    public enum GameMode {PLANNING, ACTION};

    [SerializeField] private GameModeManager globalInstace;
    private static GameModeManager globalManager;

    private List<GameModeSensitive> subscribers = new List<GameModeSensitive>();

    private GameMode currentMode = GameMode.ACTION;

    void OnAfterDeserialize()
    {
        if(globalInstace != null)
        {
            globalManager = globalInstace;
        }
    }

    void Start()
    {
        if(globalManager == null)
        {
            globalManager = this;
        }
    }

    public static GameModeManager Instance
    {
        get
        {
            return globalManager;
        }
    }

    public void enterActionMode()
    {
        changeGameMode(GameMode.ACTION);
    }
    public void enterPlanningMode()
    {
        changeGameMode(GameMode.PLANNING);
    }

    public void switchMode()
    {
        if(this.currentMode == GameMode.ACTION)
        {
            this.currentMode = GameMode.PLANNING;

            enterPlanningMode();

        }
        else if (this.currentMode == GameMode.PLANNING)
        {
            this.currentMode = GameMode.ACTION;

            enterActionMode();
        }
    }

    public void changeGameMode(GameMode mode)
    {
        if(mode == GameMode.ACTION)
        {
            foreach(GameModeSensitive gms in this.subscribers)
            {
                gms.OnEnterActionMode();
            }
        }
        else if (mode == GameMode.PLANNING)
        {
            foreach(GameModeSensitive gms in this.subscribers)
            {
                gms.OnEnterPlanningMode();
            }   
        }
    }

    public void subscribe(GameModeSensitive gms)
    {
        this.subscribers.Add(gms);
    }

    public void unsubscribe(GameModeSensitive gms)
    {
        this.subscribers.Remove(gms);
    }
}
