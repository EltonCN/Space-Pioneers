using UnityEngine;
using UnityEngine.InputSystem;

[AddComponentMenu("SpacePioneers/Mechanics/PlaceMe")]
[RequireComponent(typeof(Collider))]
public class PlaceMe : MonoBehaviour, Draggable
{
    Rigidbody rb;
    [SerializeField] float desiredY = 20;
    [SerializeField] ActionSnapshotRS actionSet;
    [SerializeField] FloatVariable cost;
    PlaceMeSnapshot snapshot;

    [SerializeField] bool ignoreCollision = false;
    [SerializeField] float collisionRadious = 0.75f;

    RigidbodyConstraints constraints;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    public void OnMouseDown(InputAction.CallbackContext context)
    {
        snapshot = new PlaceMeSnapshot(this, rb.position);
        actionSet.Add(snapshot); 

        constraints = rb.constraints;

        if(actionSet.OverMaximumCost())
        {
            actionSet.Remove(snapshot);
            snapshot = null;
        }
        else
        {
            
            rb.constraints = RigidbodyConstraints.None;
        }
    }

    public void OnMouseDrag(InputAction.CallbackContext context)
    {
        try
        {
            if(this.gameObject == null)
            {
                return;
            }
        }
        catch(MissingReferenceException)
        {
            return;
        } 

        if(snapshot == null)
        {
            return;
        }

        Vector3 mousePos = Mouse.current.position.ReadValue();
        mousePos.z = Camera.main.transform.position.y - desiredY;
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePos);
   

        float originalCost = snapshot.Cost;
        float newCost = (snapshot.OriginalPosition-worldPoint).magnitude*cost.value;

        snapshot.Cost = newCost;
        if(actionSet.OverMaximumCost())
        {
            snapshot.Cost = originalCost;
        }
        else
        {
            float distance = (worldPoint-rb.position).magnitude;

            RaycastHit hit;
            Ray ray = new Ray(rb.position, (worldPoint-rb.position));

            if(!Physics.SphereCast(ray, collisionRadious,  out hit, distance) || ignoreCollision)
            {
                MoveTo(worldPoint);
            }
            else
            {
                snapshot.Cost = originalCost;
            }
            
        }
    }

    public void OnMouseUp(InputAction.CallbackContext context)
    {
        snapshot = null;
        rb.constraints = constraints;
    }

    private void MoveTo(Vector3 position)
    {
        Debug.Log(position);

        
        rb.MovePosition(position);
    }

    public bool Cancel()
    {
        return false;
    }

    public class PlaceMeSnapshot : ActionSnapshot
    {
        PlaceMe originator;
        Vector3 originalPosition;
        float cost;

        public PlaceMeSnapshot(PlaceMe originator, Vector3 originalPosition, float cost = 0)
        {
            this.originator = originator;
            this.originalPosition = originalPosition;
            this.cost = cost; 
        }

        public float Cost
        {
            get
            {
                return this.cost;
            }
            
            set
            {
                this.cost = value;
            }
        }

        public Vector3 OriginalPosition
        {
            get
            {
                return this.originalPosition;
            }
        }

        public void undo()
        {
            originator.MoveTo(originalPosition);
        }

        public float getActionCost()
        {
            return this.cost;
        }

        public string getActionMessage()
        {
            return "Mover entidade";
        }
    }
}
