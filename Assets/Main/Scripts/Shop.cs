using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public int Price;
    public GameObject fruta;
    [SerializeField] private bool buyable;
    private bool istrue;
    public Sprite sprite;
    public ItemType type;
    public string nameItem;
    private GameManager Inventory;

    private void Start()
    {
        Inventory = GameManager.Instance;
        

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //istrue = true;
        if (collision.gameObject.layer == 7)// && istrue && Input.GetKeyDown(KeyCode.R))//layer o tag del jugador && Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("entra al colision tru");
            if (buyable){
                if (GameManager.Instance.currency >= Price)
                {
                    //le da el objeto al inventario
                    //GameManager.Instance.m_coins -= Price;
                    //HUD.HUD_Instance.buyItem(Price);
                    GameManager.Instance.buyItem(Price);
                    //playerManager.player_Instance.prefruit = fruta; // ccodigo a ser reemplazado con Scroll
                    grabFruit();
                    gameObject.SetActive(false);
                }else{
                    Debug.Log("no hay monedas suficientes");
                }
            }else{
                grabFruit();
                gameObject.SetActive(false);
            }

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the colliding object is the player.
        if (other.CompareTag("Player"))
        {
            //istrue = false;
        }
    }

    public void grabFruit()
    {
        for (int i = 0; i < Inventory.items.Length; i++)
        {
            if (Inventory.items[i].isFull == false)
            {
                Debug.Log("Item Added");
                Inventory.items[i].isFull = true;
                //Inventory.items[i].amount = 1;
                Inventory.items[i].type = type;
                Inventory.items[i].name = nameItem;
                Inventory.items[i].slotSprite.GetComponent<Image>().sprite = sprite;
                Inventory.items[i].slotSprite.GetComponent<Image>().enabled = true;
                Inventory.items[i].prefab = fruta;
                
                //Destroy(gameObject);
                break;
            }
        }
    }
}
