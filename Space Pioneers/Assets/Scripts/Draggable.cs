using UnityEngine.InputSystem;

public interface Draggable
{
    void OnMouseDown(InputAction.CallbackContext context);
    void OnMouseDrag(InputAction.CallbackContext context);
    void OnMouseUp(InputAction.CallbackContext context);
}
