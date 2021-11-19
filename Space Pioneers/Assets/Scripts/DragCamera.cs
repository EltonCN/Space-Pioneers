using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragCamera : MonoBehaviour
{
    [SerializeField]
    private float dragSpeed = 4f;
    private Transform _camera;
    private Vector3 origin;
    private Vector3 diff;
    InputAction drag;
    InputAction click;
    PlayerInput playerInput;
    InputAction.CallbackContext actualContext;    

    private void Start()
    {
        _camera = Camera.main.transform;
        
        playerInput = GetComponentInParent<PlayerInput>();
        InputActionMap map = playerInput.currentActionMap;

        click = map.FindAction("Click", true);
        click.started += OnClickStarted;
        click.canceled += OnClickCanceled;

        drag = map.FindAction("Drag", true);
    }

    public void OnClickStarted(InputAction.CallbackContext context)
    {
        Vector2 mouse = Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue());
        origin = new Vector3(mouse.x, 0, mouse.y);
        drag.performed += OnDragPerformed;
    }

    public void OnClickCanceled(InputAction.CallbackContext context)
    {
        drag.performed -= OnDragPerformed;
    }

    public void OnDragPerformed(InputAction.CallbackContext context)
    {
        Vector2 mouse = Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue());
        diff = Vector3.Normalize(origin - new Vector3(mouse.x, 0, mouse.y));
        _camera.position = Vector3.MoveTowards(_camera.position, 
                                                _camera.position + diff, 
                                                dragSpeed * Time.deltaTime);
        OnClickCanceled(actualContext);
        OnClickStarted(actualContext);
    }
    
    public void ActivateInput()
    {
        playerInput.ActivateInput();
    }

    public void DeactivateInput()
    {
        playerInput.DeactivateInput();
    }

    public void OnEnable() 
    {
        ActivateInput();
    }

    public void OnDisable() 
    {
        DeactivateInput();
    }
}
