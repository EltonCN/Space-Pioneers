using UnityEngine;
using System.Linq;

[AddComponentMenu("SpacePioneers/Game Mode/Action undo listener")]
public class ActionUndoListener : GameEventListener
{
    [SerializeField] private ActionSnapshotRS actionSet;

    void Start()
    {
        Response.AddListener(this.undo);
    }

    public void undo()
    {
        if(actionSet.ItemCount == 0)
        {
            return;
        }
        
        ActionSnapshot snap = actionSet.Items.Last();

        snap.undo();

        actionSet.Remove(snap);
    }

}