using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class InputHandler : MonoBehaviour
{
    private Camera camaraPrincipal;
    public Vector2 position;
    public bool direccion;



    private void Awake()
    {
        camaraPrincipal = Camera.main;
    }
    public void OnClick(InputAction.CallbackContext context)
    {
        direccion = playerManager.player_Instance.direccion;
        //position = camaraPrincipal.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        position = camaraPrincipal.ScreenToViewportPoint(Mouse.current.position.ReadValue());
        //Debug.Log("x"+position.x);
        //Debug.Log("y"+position.y);
        if (playerManager.player_Instance.status != 0)
        {
            if (context.performed && playerManager.player_Instance.frutas[playerManager.player_Instance.status].GetComponent<CombateCaCPlayer>().puedeAtacar)
            {
                if (position.x < 0.5f)//izquierda
                {
                    //Debug.Log("izquierda q1 y q4");
                    if (position.y < 0.5f)
                    {
                        //Debug.Log("quarter 4 (izq abajo");
                        if (direccion)
                        {
                            playerManager.player_Instance.frutas[playerManager.player_Instance.status].GetComponent<CombateCaCPlayer>().ataqueNuevo(3);
                        }
                        else
                        {
                            playerManager.player_Instance.frutas[playerManager.player_Instance.status].GetComponent<CombateCaCPlayer>().ataqueNuevo(2);
                        }
                    }
                    else
                    {
                        // Debug.Log("quarter 1 (izq arriba");
                        //playerManager.player_Instance.combatePlayer.StartCoroutine(ataqueNuevo(1));
                        //playerManager.player_Instance.combatePlayer.ataqueNuevo(1);

                        if (direccion)
                        {
                            playerManager.player_Instance.frutas[playerManager.player_Instance.status].GetComponent<CombateCaCPlayer>().ataqueNuevo(0);
                        }
                        else
                        {
                            playerManager.player_Instance.frutas[playerManager.player_Instance.status].GetComponent<CombateCaCPlayer>().ataqueNuevo(1);
                        }
                    }
                }
                else
                {
                    //Debug.Log("derecha q2 y q3");
                    if (position.y < 0.5f)
                    {
                        //Debug.Log("quarter 3 (der abajo");
                        // playerManager.player_Instance.combatePlayer.ataqueNuevo(3);

                        if (direccion)
                        {
                            playerManager.player_Instance.frutas[playerManager.player_Instance.status].GetComponent<CombateCaCPlayer>().ataqueNuevo(2);
                        }
                        else
                        {
                            playerManager.player_Instance.frutas[playerManager.player_Instance.status].GetComponent<CombateCaCPlayer>().ataqueNuevo(3);
                        }


                    }
                    else
                    {
                        //Debug.Log("quarter 2 (der arriba");

                        if (direccion)
                        {
                            playerManager.player_Instance.frutas[playerManager.player_Instance.status].GetComponent<CombateCaCPlayer>().ataqueNuevo(1);
                        }
                        else
                        {
                            playerManager.player_Instance.frutas[playerManager.player_Instance.status].GetComponent<CombateCaCPlayer>().ataqueNuevo(0);
                        }
                    }
                }
                //if (!context.started) return;


            }

        }
        if (context.performed && playerManager.player_Instance.frutas[playerManager.player_Instance.status].GetComponent<CombateCaCPlayer>().puedeAtacar)
        {
            if (position.x < 0.5f)//izquierda
            {
                //Debug.Log("izquierda q1 y q4");
                if (position.y < 0.5f)
                {
                    //Debug.Log("quarter 4 (izq abajo");
                    if (direccion)
                    {
                        playerManager.player_Instance.frutas[playerManager.player_Instance.status].GetComponent<CombateCaCPlayer>().ataqueNuevo(3);
                    }
                    else
                    {
                        playerManager.player_Instance.frutas[playerManager.player_Instance.status].GetComponent<CombateCaCPlayer>().ataqueNuevo(2);
                    }  
                }
                else
                {
                    // Debug.Log("quarter 1 (izq arriba");
                    //playerManager.player_Instance.combatePlayer.StartCoroutine(ataqueNuevo(1));
                    //playerManager.player_Instance.combatePlayer.ataqueNuevo(1);
                    
                    if (direccion)
                    {
                        playerManager.player_Instance.frutas[playerManager.player_Instance.status].GetComponent<CombateCaCPlayer>().ataqueNuevo(0);
                    }
                    else
                    {
                        playerManager.player_Instance.frutas[playerManager.player_Instance.status].GetComponent<CombateCaCPlayer>().ataqueNuevo(1);
                    }
                }
            }
            else
            {
                //Debug.Log("derecha q2 y q3");
                if (position.y < 0.5f)
                {
                    //Debug.Log("quarter 3 (der abajo");
                    // playerManager.player_Instance.combatePlayer.ataqueNuevo(3);
                    
                    if (direccion)
                    {
                        playerManager.player_Instance.frutas[playerManager.player_Instance.status].GetComponent<CombateCaCPlayer>().ataqueNuevo(2);
                    }
                    else
                    {
                        playerManager.player_Instance.frutas[playerManager.player_Instance.status].GetComponent<CombateCaCPlayer>().ataqueNuevo(3);
                    }


                }
                else
                {
                    //Debug.Log("quarter 2 (der arriba");
                   
                    if (direccion)
                    {
                        playerManager.player_Instance.frutas[playerManager.player_Instance.status].GetComponent<CombateCaCPlayer>().ataqueNuevo(1);
                    }
                    else
                    {
                        playerManager.player_Instance.frutas[playerManager.player_Instance.status].GetComponent<CombateCaCPlayer>().ataqueNuevo(0);
                    }
                }
            }
            //if (!context.started) return;
            

        }
        //if (context.canceled)
        //{
        //    //Debug.Log("se suelta clickc");
        //   //playerManager.player_Instance.sfxHIT.SetActive(false);
        //}
    }
}
