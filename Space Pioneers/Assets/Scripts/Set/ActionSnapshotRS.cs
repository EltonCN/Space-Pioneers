using UnityEngine;

[CreateAssetMenu(menuName ="Space Pioneers/Set/Action Snapshot Set")]
public class ActionSnapshotRS : RuntimeSet<ActionSnapshot>
{
    float totalCost = 0f;
    float lastTotalCost;


    private void ComputeTotalCost()
    {
        totalCost = 0;
        foreach(ActionSnapshot snap in this.Items)
        {
            totalCost += snap.getActionCost();
        }
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
}