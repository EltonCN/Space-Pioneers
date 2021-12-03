using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragAndDrop : MonoBehaviour, Draggable
{
    PlayerInput playerInput;
    InputAction drag;
    Draggable currentDragger;

    InputAction.CallbackContext actualContext;    

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        InputActionMap map = playerInput.currentActionMap;

        // Registro dos eventos do mouse nesse ciclo
        InputAction click = map.FindAction("Click", true);
        // O mapa definido em PlayerInput chama os m�todos via InputSystem
        click.started += OnClickStarted;
        click.canceled += OnClickCanceled;

        // Parte de detec��o do Drag mapeado na PlayerInput
        drag = map.FindAction("Drag", true);

        drag.performed += this.OnMouseDrag;
    }

    public void OnClickStarted(InputAction.CallbackContext context)
    {
        RaycastHit hit;
        actualContext = context;
        if (Mouse.current.clickCount.ReadValue() == 1)
        {
            currentDragger = null;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()), out hit))
            {
                // Aqui queremos saber se o gameObject encontrado implementa a interface Draggable,
                if (hit.collider != null)
                {
                    // No outro tutorial que vi, foi implementado essa verifica��o com Tags ao inv�s
                    // de Interfaces, assim � melhor e mais robusto, dividimos o comportamento por classes
                    Draggable dragger = hit.collider.gameObject.GetComponent<Draggable>();
                    if (dragger != null) {
                        if(dragger is MonoBehaviour)
                        {
                            MonoBehaviour mb = (MonoBehaviour) dragger;

                            if(mb.enabled == true)
                            {
                                currentDragger = dragger; 
                            }
                        }
                        else
                        {
                            currentDragger = dragger; 
                        }
                        
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
        // Quando o bot�o do mouse � solto, apagamos da lista de EventHandlers drag.performed 
        // o m�todo OnMouseDrag e chamamos o m�todo que realiza as a��es ao soltar o bot�o do Mouse
        
        try {
            drag.performed -= currentDragger.OnMouseDrag;
            currentDragger.OnMouseUp(context);
        }
        catch (NullReferenceException)
        {
            // Debug.Log("N�o selecionou nada");
        }
        // OBSERVA��O: se nenhum OnMouseDrag tiver sido registrado anteriormente, essa opera��o
        // n�o faz anda pois desregistrar um m�todo n�o registrado como C#Event � Opera��o Nula
    }

    public void OnMouseDown(InputAction.CallbackContext context) { }
    public void OnMouseDrag(InputAction.CallbackContext context)
    {
        // Esse m�todo no tutorial que vi � usado para movimentar a camera, por isso esta classe
        // implementa IMouse. Caso contr�rio a implementa��o de draggers mudaria tamb�m
        // Pode servir para movimentar a c�mera para melhor posicionar as pe�as, se necessario

        if(currentDragger != null)
        {
            if(currentDragger.Cancel())
            {
                
                OnClickCanceled(actualContext);
                OnClickStarted(actualContext);
            }
        }
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

    public bool Cancel()
    {
        return false;
    }
}