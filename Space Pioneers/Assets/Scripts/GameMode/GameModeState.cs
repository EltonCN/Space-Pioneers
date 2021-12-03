using UnityEngine;

[CreateAssetMenu(menuName ="Space Pioneers/Game Mode State")]
public class GameModeState : ScriptableObject
{
    public GameMode actualGameMode = GameMode.ACTION;
}