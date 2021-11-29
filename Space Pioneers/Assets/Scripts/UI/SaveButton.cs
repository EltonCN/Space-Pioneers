using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SaveButton : MonoBehaviour
{
    [SerializeField] SaveManager saveManager;
    [SerializeField] int saveIndex;
    [SerializeField] Sprite selectedSaveSprite;
    [SerializeField] Sprite unselectedSaveSprite;

    Image image;

    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        if(saveManager.CurrentSave == saveIndex)
        {
            image.sprite = selectedSaveSprite;
        }
        else
        {
            image.sprite = unselectedSaveSprite;
        }
    }
}
