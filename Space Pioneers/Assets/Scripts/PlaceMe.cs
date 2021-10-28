using UnityEngine;
using UnityEngine.InputSystem;

public class PlaceMe : MonoBehaviour, Draggable
{
    Rigidbody rb;
    [SerializeField] float desiredZ = 20;

    // Start is called before the first frame update
    void Start()
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
        mousePos.z = desiredZ - Camera.main.transform.position.z;
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePos);

        transform.position = worldPoint;   
    }

    public void OnMouseUp(InputAction.CallbackContext context)
    {
        rb.isKinematic = false;
    }
}
