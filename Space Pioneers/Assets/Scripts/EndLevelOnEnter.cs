using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System;

public class EndLevelOnEnter : MonoBehaviour
{
    [Tooltip("Object to wait enter in the collider.")] [SerializeField] GameObject targetObject;

    [Tooltip("Game save manager.")][SerializeField] SaveManager saveManager; 


    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject == targetObject)
        {
            if(saveManager.Save.last_played_level < SceneManager.GetActiveScene().buildIndex)
            {
                saveManager.Save.last_played_level = SceneManager.GetActiveScene().buildIndex;
            }

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
    }
        
}
