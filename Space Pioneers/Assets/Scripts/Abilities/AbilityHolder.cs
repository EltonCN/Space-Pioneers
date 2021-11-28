using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityHolder : MonoBehaviour
{
    public GameMode gameMode;
    public GameModeState globalState;
    public GameObject parentGameObject;
    public Ability ability;
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

    private void Start() {
        InputActionMap map = playerInput.currentActionMap;
        abilityAction = map.FindAction(inputActionName, true);
        abilityAction.performed += FireAbility;
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
                }
            break;
            case AbilityState.cooldown:
                if (cooldownTime > 0) {
                    cooldownTime -= Time.deltaTime;
                } else {
                    state = AbilityState.ready;
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
