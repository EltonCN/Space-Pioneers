using UnityEngine;
using UnityEngine.InputSystem;

public class PlaceMe : MonoBehaviour, Draggable
{
    Rigidbody rb;
    [SerializeField] float desiredY = 20;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

    }

    public void OnMouseDown(InputAction.CallbackContext context)
    {
        rb.isKinematic = true;
    }

    public void OnMouseDrag(InputAction.CallbackContext context)
    {   
        Vector3 mousePos = Mouse.current.position.ReadValue();
        mousePos.z = Camera.main.transform.position.y - desiredY;
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePos);

        MoveTo(worldPoint);

        Debug.Log(worldPoint);
    }

    public void OnMouseUp(InputAction.CallbackContext context)
    {
        rb.isKinematic = false;
    }

    private void MoveTo(Vector3 position)
    {
        rb.MovePosition(position);
    }

    public class PlaceMeSnapshot : ActionSnapshot
    {
        PlaceMe originator;
        Vector3 originalPosition;
        int cost;

        public PlaceMeSnapshot(PlaceMe originator, Vector3 originalPosition, int cost = 0)
        {
            this.originator = originator;
            this.originalPosition = originalPosition;
            this.cost = cost; 
        }

        public int Cost
        {
            get
            {
                return this.cost;
            }
            
            set
            {
                this.cost = Cost;
            }
        }

        public void undo()
        {
            originator.MoveTo(originalPosition);
        }

        public int getActionCost()
        {
            return this.cost;
        }

        public string getActionMessage()
        {
            return "Mover entidade";
        }
    }
}
