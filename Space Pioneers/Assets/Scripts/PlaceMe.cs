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

        rb.MovePosition(worldPoint);   

        Debug.Log(worldPoint);
    }

    public void OnMouseUp(InputAction.CallbackContext context)
    {
        rb.isKinematic = false;
    }
}
