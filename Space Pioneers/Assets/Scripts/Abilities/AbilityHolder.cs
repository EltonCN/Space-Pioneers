using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AbilityHolder : MonoBehaviour
{
    public GameMode gameMode;
    public GameModeState globalState;
    public GameObject parentGameObject;
    public Ability ability;
    public Button buttonUI;
    float cooldownTime;
    float activeTime;
    enum AbilityState {
        ready,
        active,
        cooldown
    }
    AbilityState state = AbilityState.ready;

    // Input    
    public PlayerInput playerInput;
    public string inputActionName;
    private InputAction abilityAction;
    InputActionMap map;
    private void Awake() {
        InputActionMap map = playerInput.currentActionMap;
        abilityAction = map.FindAction(inputActionName, true);
    }

    private void Start() {
        abilityAction.performed += FireAbility;
        if (globalState.actualGameMode == gameMode) {
            buttonUI.interactable = true;
        } else {
            buttonUI.interactable = false;
        }
    }
    
    void Update()
    {
        switch(state) {
            case AbilityState.active:
                if (activeTime > 0) {
                    activeTime -= Time.deltaTime;
                } else {
                    ability.BeginCooldown(parentGameObject);
                    state = AbilityState.cooldown;
                    cooldownTime = ability.cooldownTime;
                    buttonUI.interactable = false;
                }
            break;
            case AbilityState.cooldown:
                if (cooldownTime > 0) {
                    cooldownTime -= Time.deltaTime;
                } else {
                    state = AbilityState.ready;
                    buttonUI.interactable = true;
                }
            break;
            default:
            break;
        }
    }

    public void FireAbility(InputAction.CallbackContext context) {
        if (state == AbilityState.ready && globalState.actualGameMode == gameMode) {
            ability.Activate(parentGameObject);
            state = AbilityState.active;
            activeTime = ability.activeTime;
        }
    }

    public void FireAbility() {
        if (state == AbilityState.ready && globalState.actualGameMode == gameMode) {
            ability.Activate(parentGameObject);
            state = AbilityState.active;
            activeTime = ability.activeTime;
        }
    }

}
