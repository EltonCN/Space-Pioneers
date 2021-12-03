using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragCamera : MonoBehaviour
{
    [SerializeField]
    private float dragSpeed = 20f;
    [SerializeField]
    private Vector2 limitsX = new Vector2(-150, 150);
    [SerializeField]
    private Vector2 limitsZ = new Vector2(-200, 200);
    public Transform _camera;
    private Vector3 origin;
    private Vector3 diff;
    InputAction drag;
    InputAction click;
    PlayerInput playerInput;
    InputActionMap map;
    InputAction.CallbackContext actualContext;    
    Vector2 mouse;

    private void Awake()
    {
        //_camera = Camera.main.transform;
        
        playerInput = GetComponentInParent<PlayerInput>();
        map = playerInput.currentActionMap;

        click = map.FindAction("Click", true);
        drag = map.FindAction("Drag", true);
    }

    private void Start() {
        click.started += OnClickStarted;
        click.canceled += OnClickCanceled;
    }

    public void OnClickStarted(InputAction.CallbackContext context)
    {
        mouse = Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue());
        origin = new Vector3(mouse.x, 0, mouse.y);
        drag.performed += OnDragPerformed;
    }

    public void OnClickCanceled(InputAction.CallbackContext context)
    {
        drag.performed -= OnDragPerformed;
    }

    public void OnDragPerformed(InputAction.CallbackContext context)
    {
        mouse = Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue());
        diff = Vector3.Normalize(origin - new Vector3(mouse.x, 0, mouse.y));
        if (!(_camera.position.x <= limitsX.x || _camera.position.x >= limitsX.y ||
                _camera.position.z <= limitsZ.x || _camera.position.x >= limitsZ.y )) 
        {
                _camera.position = Vector3.MoveTowards(_camera.position, 
                                                _camera.position + diff, 
                                                dragSpeed * Time.deltaTime);
        }
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
