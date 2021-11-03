using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("SpacePioneers/Game Mode/Game Mode Mechanics")]
public class GameModeMechanics : GameEventListener
{
    [SerializeField] private List<MonoBehaviour> actionMechanics = new List<MonoBehaviour>();
    [SerializeField] private List<MonoBehaviour> planningMechanics = new List<MonoBehaviour>();

    [SerializeField] private GameModeState globalState;


    // Start is called before the first frame update
    void Start()
    {
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
         foreach(MonoBehaviour m in actionMechanics)
        {
            m.enabled = false;
        }

        foreach(MonoBehaviour m in planningMechanics)
        {
            m.enabled = true;
        }
    }

    void enterActionMode()
    {
        foreach(MonoBehaviour m in planningMechanics)
        {
            m.enabled = false;
        }

        foreach(MonoBehaviour m in actionMechanics)
        {
            m.enabled = true;
        }
    }
}
