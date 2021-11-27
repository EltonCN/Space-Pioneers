using UnityEngine;

[CreateAssetMenu(menuName ="Space Pioneers/Set/Action Snapshot Set")]
public class ActionSnapshotRS : RuntimeSet<ActionSnapshot>
{
    float totalCost = 0f;
    float lastTotalCost;

    [Tooltip("Maximum player actions cost.")] [SerializeField] FloatVariable maxActionCost; 

    private void ComputeTotalCost()
    {
        totalCost = 0;
        foreach(ActionSnapshot snap in this.Items)
        {
            totalCost += snap.getActionCost();
        }
    }

    public bool OverMaximumCost()
    {
        ComputeTotalCost();

        if(totalCost >= maxActionCost.value)
        {
            return true;
        }
        return false;
    }

    public new void Reset()
    {
        lastTotalCost = totalCost;
        base.Reset();
    }

    public float TotalCost
    {
        get
        {
            ComputeTotalCost();
            return totalCost;
        }
    }

    public float LastTotalCost
    {
        get
        {
            return lastTotalCost;
        }
    }

    public new void OnEnable()
    {
        base.OnEnable();
        this.lastTotalCost = 0; 
    }
}