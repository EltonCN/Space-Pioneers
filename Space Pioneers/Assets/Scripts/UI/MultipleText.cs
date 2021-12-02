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

    int actualText;
    LeanLocalizedTextMeshProUGUI textBox;

    void Start()
    {
        actualText = 0;
        textBox = GetComponent<LeanLocalizedTextMeshProUGUI>();

        if(translations.Count != 0)
        {
            textBox.TranslationName = translations[0];
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
    }
}
