using UnityEngine;
using UnityEngine.SceneManagement;

public class UpdateSaveEndLevel : MonoBehaviour
{
    [Tooltip("Game save manager.")][SerializeField] SaveManager saveManager; 

    void Start()
    {
        Debug.Log("Teste");
    }

    public void updateSave()
    {
        if(saveManager.Save.last_played_level < SceneManager.GetActiveScene().buildIndex)
        {
            saveManager.Save.last_played_level = SceneManager.GetActiveScene().buildIndex;
        }
    }
}