using UnityEngine;

[AddComponentMenu("SpacePioneers/Game Mode/Game Mode Listener")]
public class GameModeListener : GameEventListener
{
    [SerializeField] private GameModeState globalState;
    [SerializeField] private GameEvent onGameModeChange;
    [SerializeField] private ActionSnapshotRS actionSet;
    [SerializeField] private FloatVariable costToSecond;
    [SerializeField] private float cooldown;

    private float lastChange = 0;

    void Start()
    {
        Response.AddListener(this.ToggleGameMode);
    }

    public void ToggleGameMode()
    {
        if(Time.time - lastChange < cooldown)
        {
            return;
        }

        if(globalState.actualGameMode == GameMode.ACTION)
        {
            cooldown = 0;
            globalState.actualGameMode = GameMode.PLANNING;
        }
        else if(globalState.actualGameMode == GameMode.PLANNING)
        {
            float totalCost = 0;
            foreach(ActionSnapshot snap in actionSet.Items)
            {
                totalCost += snap.getActionCost();
            }
            cooldown = costToSecond.value*totalCost;

            actionSet.Reset();

            globalState.actualGameMode = GameMode.ACTION;
        }

        lastChange = Time.time;

        onGameModeChange.Raise();
    }
}