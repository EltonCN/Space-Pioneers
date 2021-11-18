using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System;

public class EndLevelOnEnter : MonoBehaviour
{
    [Tooltip("Scene to go on object enter.")] [SerializeField] UnityEngine.Object nextScene;
    [Tooltip("Object to wait enter in the collider.")] [SerializeField] GameObject targetObject;


    private void OnCollisionEnter(Collision other) 
    {
        if(nextScene == null)
        {
            return;
        }

        if(other.gameObject == targetObject)
        {
            SceneManager.LoadScene(nextScene.name);
        }
    }

    void OnValidate()
    {
        if(nextScene == null)
        {
            Debug.LogWarning("Next scene set to null.");
            return;
        }

        try
        {
            SceneAsset s = (SceneAsset) nextScene;
        }
        catch(InvalidCastException)
        {
            Debug.LogError("The next scene must be an Unity Scene asset.");
            nextScene = null;
        }
        
    }
        
}
