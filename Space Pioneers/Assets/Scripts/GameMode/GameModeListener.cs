using UnityEngine;

[AddComponentMenu("SpacePioneers/Game Mode/Game Mode Listener")]
public class GameModeListener : GameEventListener
{
    [SerializeField] private GameModeState globalState;
    [SerializeField] private GameEvent onGameModeChange;

    void Start()
    {
        Response.AddListener(this.ToggleGameMode);
    }

    public void ToggleGameMode()
    {
        if(globalState.actualGameMode == GameMode.ACTION)
        {
            globalState.actualGameMode = GameMode.PLANNING;
        }
        else if(globalState.actualGameMode == GameMode.PLANNING)
        {
            globalState.actualGameMode = GameMode.ACTION;
        }

        onGameModeChange.Raise();
    }
}