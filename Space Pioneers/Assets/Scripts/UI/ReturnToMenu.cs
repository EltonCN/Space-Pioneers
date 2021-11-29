using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour
{
    public void goToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
