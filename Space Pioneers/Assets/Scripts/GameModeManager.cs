using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[AddComponentMenu("SpacePioneers/GameMode/Manager")]
public class GameModeManager : MonoBehaviour
{
    public enum GameMode {PLANNING, ACTION};

    private static GameModeManager globalManager;

    [SerializeField] private List<GameModeSensitive> subscribers;

    private GameModeManager()
    {
        subscribers = new List<GameModeSensitive>();
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
