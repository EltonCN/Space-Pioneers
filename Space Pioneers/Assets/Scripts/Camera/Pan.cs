using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class Pan : MonoBehaviour
{
    [SerializeField]
    private float panSpeed = 5f;
    private PlayerInput playerInput;
    private Camera _camera;
    InputAction pan;
    Vector2 value;

    private void Awake() {
        playerInput = GetComponentInParent<PlayerInput>();
        InputActionMap map = playerInput.currentActionMap;
        _camera = Camera.main;

        pan = map.FindAction("Pan", true);
        pan.performed += x => value = x.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        float x = value.x;
        float z = value.y;
        if (x != 0 || z != 0) {
            PanScreen(x, z);
        }
    }

    public Vector3 PanDirection(float x, float z) 
    {
        Vector3 direction = Vector3.zero;
        if (z >= Screen.height * .90f) {
            direction.z += 1;
        }
        else if (z <= Screen.height * .05f) {
            direction.z -= 1;
        }
        if (x >= Screen.width * .95f) {
            direction.x += 1;
        }
        else if (x <= Screen.width * .05f) {
            direction.x -= 1;
        }
        return direction;
    }

    public void PanScreen(float x, float z) 
    {
        Vector3 direction = PanDirection(x, z);
        _camera.transform.position = Vector3.MoveTowards(_camera.transform.position, _camera.transform.position + direction, panSpeed * Time.deltaTime);
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
