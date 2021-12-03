using UnityEngine;
using UnityEngine.SceneManagement;

public class SetLevelAsPlayed : MonoBehaviour
{
    [Tooltip("Game save manager.")][SerializeField] SaveManager saveManager; 

    public void setAsPlayed()
    {
        if(saveManager.Save.last_played_level < SceneManager.GetActiveScene().buildIndex)
        {
            saveManager.Save.last_played_level = SceneManager.GetActiveScene().buildIndex;
        }
    }
}