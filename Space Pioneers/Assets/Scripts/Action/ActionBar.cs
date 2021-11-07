using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("SpacePioneers/Game Mode/Action Bar")]
[RequireComponent(typeof(Image))]
public class ActionBar : GameEventListener
{
    [Tooltip("Action set with player actions.")] [SerializeField] ActionSnapshotRS actionSet;
    [Tooltip("Maximum player actions cost.")] [SerializeField] FloatVariable maxActionCost;
    [Tooltip("Synchronized game mode.")] [SerializeField] private GameModeState gameMode;
    [Tooltip("Action cost to second conversion rate.")] [SerializeField] FloatVariable costToSecond;

    Image actionBar;

    private float lastChange = 0;


    void Awake()
    {
        actionBar = GetComponent<Image>();
    }

    void Start()
    {
        Response.AddListener(this.ToggleGameMode);
    }

    void Update()
    {
        float fillAmount = 0f;
        if(gameMode.actualGameMode == GameMode.ACTION)
        {
            fillAmount = actionSet.LastTotalCost;

            float deltaT = Time.time-lastChange;
            float cooldown = actionSet.LastTotalCost*costToSecond.value;
            float passedTime = Mathf.Clamp(deltaT/cooldown, 0f, 1f);

            fillAmount = fillAmount * (1-passedTime);

        }
        else if(gameMode.actualGameMode == GameMode.PLANNING)
        {
            fillAmount = actionSet.TotalCost;
        }

        fillAmount /= maxActionCost.value;

        fillAmount = Mathf.Clamp(fillAmount, 0f, 1f);
        actionBar.fillAmount = fillAmount;
    }

    void ToggleGameMode()
    {
        lastChange = Time.time;
    }
}
