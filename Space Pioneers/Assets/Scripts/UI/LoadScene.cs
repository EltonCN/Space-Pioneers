using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{

    [Tooltip("Scene to load build index. Can't be used with the reload or load next options. Negative values means no use.")]
    [SerializeField] int targetSceneBuildIndex = 0;
    
    [SerializeField] bool reloadScene = false;
    [SerializeField] bool loadNext = false;

    public void load()
    {
        int actualSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if(reloadScene)
        {
            SceneManager.LoadScene(actualSceneIndex);
        }
        else if(loadNext)
        {
            SceneManager.LoadScene(actualSceneIndex+1);
        }
        else if(targetSceneBuildIndex > -1)
        {
            SceneManager.LoadScene(targetSceneBuildIndex);
        }
    }

    void OnValidate()
    {
        if(reloadScene || loadNext)
        {
            targetSceneBuildIndex = -1;
        }
    }
}
