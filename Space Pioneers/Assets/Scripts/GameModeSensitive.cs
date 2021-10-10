interface GameModeSensitive
{
    void subscribeActionMode(GameModeManager manager=null)
    {
        if(manager == null)
        {
            manager = GameModeManager.Instance;
        }

        manager.OnEnterActionMode += this.OnEnterActionMode;
    }

    void subscribePlanningMode(GameModeManager manager=null)
    {
        if(manager == null)
        {
            manager = GameModeManager.Instance;
        }

        manager.OnEnterPlanningMode += this.OnEnterPlanningMode;
    }

    void OnEnterActionMode()
    {

    }

    void OnEnterActionMode()
    {

    }
}