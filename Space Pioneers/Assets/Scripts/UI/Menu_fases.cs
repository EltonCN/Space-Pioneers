using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using TMPro;

public class Menu_fases : MonoBehaviour
{
    GridLayoutGroup grid;

    [SerializeField] Button nextButton;
    [SerializeField] Button previousButton;
    [SerializeField] Sprite noAvailableSprite;
    [SerializeField] Sprite availableSprite;

    [SerializeField] Material noAvailableTextMaterial;
    [SerializeField] Material availableTextMaterial;
    [SerializeField] SaveManager saveManager;
    [SerializeField] int lastSceneLevelIndex;

    Image[] buttonImages;
    TextMeshProUGUI[] buttonTexts;

    Button[] buttons;

    GameObject[] buttonObjects;

    int offset;

    void Awake()
    {
        grid = GetComponent<GridLayoutGroup>();

        buttonImages = new Image[6];
        buttonTexts = new TextMeshProUGUI[6];
        buttons = new Button[6];
        buttonObjects = new GameObject[6];

        for(int i = 0; i< 6; i++)
        {
            Transform child = grid.transform.GetChild(i);

            buttonObjects[i] = child.gameObject;
            buttons[i] = child.GetComponent<Button>();
            buttonImages[i] = child.GetComponent<Image>();
            buttonTexts[i] = child.GetComponentInChildren<TextMeshProUGUI>();
        }
    }


    void OnEnable()
    {
        offset = 0;

        for(int i = 0; i<6; i++)
        {
            setButton(i, false);
        }

        updatePageButtons();
    }

    void updatePageButtons()
    {
        if(offset + 6 < lastSceneLevelIndex)
        {
            nextButton.gameObject.SetActive(true);
        }
        else
        {
            nextButton.gameObject.SetActive(false);
        }

        if(offset > 0)
        {
            previousButton.gameObject.SetActive(true);
        }
        else
        {
            previousButton.gameObject.SetActive(false);
        }
    }

    void setButton(int index, bool available)
    {
        if(index+offset+1 > lastSceneLevelIndex)
        {
            buttonObjects[index].SetActive(false);
            return;
        }
        else
        {
            buttonObjects[index].SetActive(true);
        }


        if(available)
        {
            buttonImages[index].sprite = availableSprite;
            buttonTexts[index].fontMaterial  = availableTextMaterial;
            buttons[index].interactable = true;
        }
        else
        {
            buttonImages[index].sprite = noAvailableSprite;
            buttonTexts[index].fontMaterial  = noAvailableTextMaterial;
            buttons[index].interactable = false;
        }

        buttonTexts[index].SetMaterialDirty();

        buttonTexts[index].text = (offset+index+1).ToString();
       
    }

    // Update is called once per frame
    void Update()
    {
        int last_played = saveManager.Save.last_played_level;

        for(int i = 0; i<6; i++)
        {
            int level = offset+i+1;

            if(level <= last_played+1)
            {
                setButton(i, true);
            }
            else
            {
                setButton(i, false);
            }
        }
    }

    public void buttonCall(int index)
    {
        SceneManager.LoadScene(index+offset+1);
    }

    public void nextPage()
    {
        offset += 6;
        updatePageButtons();
    }

    public void previousPage()
    {
        offset -= 6;
        updatePageButtons();
    }
}
