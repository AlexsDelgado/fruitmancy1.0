using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public int Price;
    public int position;
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
    public void Update()
    {
        if (buyable)
        {
            if (istrue && Input.GetButtonDown("ActionButton"))
            {
                
                tryBuy();
            }
        }
        
    }
    public void tryBuy()
    {
        position = verificarInventario();
        if (GameManager.Instance.currency >= Price && position !=-1)
        {
            GameManager.Instance.buyItem(Price);
            grabFruit();
            
        }else
        {
            Debug.Log("no hay monedas suficientes");
            //grabFruit();
            //gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            istrue = true;
            if (buyable == false)
            {
                grabFruit();

            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the colliding object is the player.
        if (other.CompareTag("Player"))
        {
            istrue = false;
        }
    }

    public int verificarInventario()
    {
        for (int i = 0; i < Inventory.items.Length; i++)
        {
            if (Inventory.items[i].isFull == false)
            {
                return i;
            }
        }
        return -1;

    }

    public void grabFruit()
    {
        position = verificarInventario();
        if (position !=-1)
        {
            Inventory.items[position].isFull = true;
            Inventory.items[position].type = type;
            Inventory.items[position].name = nameItem;
            Inventory.items[position].slotSprite.GetComponent<Image>().sprite = sprite;
            Inventory.items[position].slotSprite.GetComponent<Image>().enabled = true;
            Inventory.items[position].prefab = fruta;
            gameObject.SetActive(false);
        }
        
        //for (int i = 0; i < Inventory.items.Length; i++)
        //{
        //    if (Inventory.items[i].isFull == false)
        //    {
        //        Debug.Log("Item Added");
        //        Inventory.items[i].isFull = true;
        //        //Inventory.items[i].amount = 1;
        //        Inventory.items[i].type = type;
        //        Inventory.items[i].name = nameItem;
        //        Inventory.items[i].slotSprite.GetComponent<Image>().sprite = sprite;
        //        Inventory.items[i].slotSprite.GetComponent<Image>().enabled = true;
        //        Inventory.items[i].prefab = fruta;
        //        //Destroy(gameObject);
        //        gameObject.SetActive(false);
        //        break;
        //    }
        //}
    }
}