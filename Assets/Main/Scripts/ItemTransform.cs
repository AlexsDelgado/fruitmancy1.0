using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemTransform : MonoBehaviour
{

    public float pickupRange = 2f;    // Adjust the range for picking up the item.
    public CombateCaCPlayer combatePlayer;
    public Player player;
    [SerializeField]private bool canPickup;
    public Animator m_animator;
    [SerializeField] int fruta;

    //nuevo inventario
    GameManager Inventory;
    public Sprite sprite;
    public ItemType type;
    public string nameItem;


    private void Start()
    {

        Inventory = GameManager.Instance;
    }

    private void Update()
    {
        if (canPickup && Input.GetButtonDown("ActionButton"))
        //Input.GetButtonDown("ActionButton")
        //Input.GetKeyDown(KeyCode.T)
        {
            PickupItem();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
      
            canPickup = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the colliding object is the player.
        if (other.CompareTag("Player"))
        {
            canPickup = false;
        }
    }

    private void PickupItem()
    {
   
        //combatePlayer.enabled = true;
        playerManager.player_Instance.health = playerManager.player_Instance.maxHealth;
        playerManager.player_Instance.transformacion(fruta);
        playerManager.player_Instance.status = fruta;
        gameObject.SetActive(false); 
    }
}
