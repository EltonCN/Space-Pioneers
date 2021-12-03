using UnityEngine;
using UnityEngine.SceneManagement;

public class SetLevelAsPlayed : MonoBehaviour
{
    [Tooltip("Game save manager.")][SerializeField] SaveManager saveManager; 

    void Start()
    {

    }

    public void updateSave()
    {
        Debug.Log("Salvando");
        Debug.Log(saveManager.Save.last_played_level);
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        if(saveManager.Save.last_played_level < SceneManager.GetActiveScene().buildIndex)
        {
            Debug.Log("Salvando");
            saveManager.Save.last_played_level = SceneManager.GetActiveScene().buildIndex;
        }
    }
}