using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Lean.Localization;

[RequireComponent(typeof(LeanLocalizedTextMeshProUGUI))]
public class MultipleText : MonoBehaviour
{
    [Tooltip("Must be Lean Localization translation names")][SerializeField] List<string> translations =  new List<string>();
    [SerializeField]  UnityEvent raiseOnEnd;
    [SerializeField] List<UnityEvent> raiseWithText;

    int actualText;
    LeanLocalizedTextMeshProUGUI textBox;

    void Start()
    {
        actualText = 0;
        textBox = GetComponent<LeanLocalizedTextMeshProUGUI>();
        if(translations.Count != 0)
        {
            textBox.TranslationName = translations[0];
            if(raiseWithText.Count > 0)
            {
                raiseWithText[0].Invoke();
            }
            
        }
    }
    
    public void nextText()
    {
        actualText += 1;

        if(actualText >= translations.Count)
        {
            raiseOnEnd.Invoke();
            return;
        }
        
        textBox.TranslationName = translations[actualText];

        if(actualText < raiseWithText.Count)
        {
            raiseWithText[actualText].Invoke();
        }
    }
}
