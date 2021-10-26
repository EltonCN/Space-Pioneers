using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("SpacePioneers/Game Mode/Game Mode Listener")]

public class GameModeListener : GameEventListener
{
    [SerializeField] private List<MonoBehaviour> actionMechanics = new List<MonoBehaviour>();
    [SerializeField] private List<MonoBehaviour> planningMechanics = new List<MonoBehaviour>();

    [SerializeField] private GameMode firstMode = GameMode.ACTION;


    private GameMode gameMode;

    // Start is called before the first frame update
    void Start()
    {
        Response.AddListener(this.toggle);

        if(firstMode == GameMode.ACTION)
        {
            enterActionMode();
        }
        else if(firstMode == GameMode.PLANNING)
        {
            enterPlanningMode();
        }
    }

    public void toggle()
    {
        if(gameMode == GameMode.ACTION)
        {
            enterPlanningMode();
            
        }
        else if(gameMode == GameMode.PLANNING)
        {
            enterActionMode();
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

        gameMode = GameMode.PLANNING;
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

        gameMode = GameMode.ACTION; 
    }
}
