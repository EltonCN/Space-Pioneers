using UnityEngine;
using UnityEngine.InputSystem;

public class RaiseOnClick : MonoBehaviour, Draggable
{
    [SerializeField] GameEvent Event;
    [SerializeField] FloatVariable dragRadius;

    bool raised;
    public void OnMouseDown(InputAction.CallbackContext context)
    {
        raised = false;
    }

    public void OnMouseUp(InputAction.CallbackContext context)
    {
    }

    public void OnMouseDrag(InputAction.CallbackContext context)
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        mousePos.z = Camera.main.transform.position.y - transform.position.y;
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePos);

        if(!raised && (worldPoint-transform.position).magnitude > dragRadius.value)
        {
            raised = true;
            Event.Raise();
        }

    }

      public bool Cancel()
    {
        return raised;
    }
}
