using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameModeListener : GameEventListener
{
    [SerializeField] private List<Button> actionButtons = new List<Button>();
    [SerializeField] private List<Button> planningButtons = new List<Button>();
    [SerializeField] private GameModeState globalState;

    private void Start() {
        Response.AddListener(this.OnModeChange);

        OnModeChange();
    }

    public void OnModeChange()
    {
        if(globalState.actualGameMode == GameMode.ACTION)
        {
            enterActionMode();
        }
        else if(globalState.actualGameMode == GameMode.PLANNING)
        {
            enterPlanningMode();
        }
    }

    void enterPlanningMode()
    {
        foreach(Button b in actionButtons)
        {
            b.interactable = false;
        }

        foreach(Button b in planningButtons)
        {
            b.interactable = true;
        }
    }

    void enterActionMode()
    {
        foreach(Button b in planningButtons)
        {
            b.interactable = false;
        }
        foreach(Button b in actionButtons)
        {
            b.interactable = true;
        }
    }

    public bool IsActionMechanic<T>()
    {
        return ListHasClass<T>(actionButtons);
    }

    public bool IsPlanningMechanic<T>()
    {
        return ListHasClass<T>(planningButtons);
    }

    private bool ListHasClass<T>(List<Button> list)
    {
        foreach(var obj in list)
        {
            if(obj is T)
            {
                return true;
            }
        }

        return false;
    }

    
}
