using UnityEngine;
using UnityEngine.InputSystem;

public class PlaceMe : MonoBehaviour, Draggable
{
    Rigidbody rb;
    Camera cam;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
    }

    void Update()
    {

    }

    public void OnMouseDown(InputAction.CallbackContext context)
    {
        //rb.isKinematic = true;
    }

    public void OnMouseDrag(InputAction.CallbackContext context)
    {

        //Mouse.current. ;

        //Vector3 c = Camera.main.transform.position;


        Debug.Log("Hello There");
    }

    public void OnMouseUp(InputAction.CallbackContext context)
    {
        //rb.isKinematic = false;
    }
}
