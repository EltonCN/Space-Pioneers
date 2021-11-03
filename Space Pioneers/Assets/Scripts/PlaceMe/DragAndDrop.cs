using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragAndDrop : MonoBehaviour, Draggable
{
    PlayerInput playerInput;
    InputAction drag;
    Draggable currentDragger;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        InputActionMap map = playerInput.currentActionMap;

        // Registro dos eventos do mouse nesse ciclo
        InputAction click = map.FindAction("Click", true);
        // O mapa definido em PlayerInput chama os métodos via InputSystem
        click.started += OnClickStarted;
        click.canceled += OnClickCanceled;

        // Parte de detecção do Drag mapeado na PlayerInput
        drag = map.FindAction("Drag", true);
    }

    public void OnClickStarted(InputAction.CallbackContext context)
    {
        RaycastHit hit;

        if (Mouse.current.clickCount.ReadValue() == 1)
        {
            currentDragger = null;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()), out hit))
            {
                // Aqui queremos saber se o gameObject encontrado implementa a interface Draggable,
                if (hit.collider != null)
                {
                    // No outro tutorial que vi, foi implementado essa verificação com Tags ao invés
                    // de Interfaces, assim é melhor e mais robusto, dividimos o comportamento por classes
                    Draggable dragger = hit.collider.gameObject.GetComponent<Draggable>();
                    if (dragger != null) { 
                        currentDragger = dragger; 
                    }
                }
            }

            if (currentDragger != null)
            {
                // Registro dos eventos de Drag nesse ciclo, uma lista dos objetos a serem movidos pelo mouse
                currentDragger.OnMouseDown(context);
                drag.performed += currentDragger.OnMouseDrag;
            }
        }
    }

    public void OnClickCanceled(InputAction.CallbackContext context)
    {
        // Quando o botão do mouse é solto, apagamos da lista de EventHandlers drag.performed 
        // o método OnMouseDrag e chamamos o método que realiza as ações ao soltar o botão do Mouse
        
        try {
            drag.performed -= currentDragger.OnMouseDrag;
            currentDragger.OnMouseUp(context);
        }
        catch (NullReferenceException)
        {
            // Debug.Log("Não selecionou nada");
        }
        // OBSERVAÇÃO: se nenhum OnMouseDrag tiver sido registrado anteriormente, essa operação
        // não faz anda pois desregistrar um método não registrado como C#Event é Operação Nula
    }

    public void OnMouseDown(InputAction.CallbackContext context) { }
    public void OnMouseDrag(InputAction.CallbackContext context)
    {
        // Esse método no tutorial que vi é usado para movimentar a camera, por isso esta classe
        // implementa IMouse. Caso contrário a implementação de draggers mudaria também
        // Pode servir para movimentar a câmera para melhor posicionar as peças, se necessario

    }
    public void OnMouseUp(InputAction.CallbackContext context) { }

    public void ActivateInput()
    {
        playerInput.ActivateInput();
    }

    public void DeactivateInput()
    {
        playerInput.DeactivateInput();
    }
}