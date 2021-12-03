using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class Zoom : MonoBehaviour
{
    [SerializeField]
    private float zoomSpeed = 20f;
    [SerializeField]
    private float maxZoomOut = 160f;
    [SerializeField]
    private float maxZoomIn = 50f;
    public Transform _camera;
    private PlayerInput playerInput;
    InputAction zoom;
    float scrollY;
    
    private void Awake() 
    {
        playerInput = GetComponentInParent<PlayerInput>();
        //_camera = Camera.main;
        InputActionMap map = playerInput.currentActionMap;

        zoom = map.FindAction("Zoom", true);
        zoom.performed += ctx => OnZoom(ctx);
    }
    
    public void OnZoom(InputAction.CallbackContext ctx)
    {
        scrollY = ctx.ReadValue<float>();
        if (scrollY < 0 && _camera.position.y <= maxZoomOut) { // Zoom Out
            ZoomScreen(20); 
        } else if (scrollY > 0 && _camera.position.y >= maxZoomIn) { // Zoom In
            ZoomScreen(-20);
        }
    }

    private void ZoomScreen(float increment) 
    {
        Vector3 direction = new Vector3(0, increment, 0);
        _camera.position = Vector3.MoveTowards(_camera.position, 
                                                    _camera.position + direction, 
                                                    zoomSpeed * Time.deltaTime);
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
