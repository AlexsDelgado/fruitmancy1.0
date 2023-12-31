

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using Unity.VisualScripting;
using UnityEngine.Rendering;
using UnityEngine.U2D;


public enum ItemType
{
    appleScroll,
    bananaScroll,
    peachScroll,
    none
}

public class GameManager : MonoBehaviour
{

    

    public static GameManager Instance;
    
    public UnityEvent sumCoins = new UnityEvent();
    public GameObject PausePanel;
    public GameObject DefeatPanel;
    public GameObject VictoryPanel;
    public bool pause = false;

    //stats
    [SerializeField] public int currency;

    //contadorcontador da�o recibido
    //contador da�o infligido 
    //contador enemigos eliminados
    //contador de muertes
    //contador de frutas

    //inventario
    public int numSlot;
    ItemTransform inventory;
    playerManager fruta;
    public Item[] items;
    private Sprite sprite;
    private GameObject prefabInv;
    public ShopZone ui;
    public bool chatting = false;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
    public void Start()
    {
        items[0].Selected.GetComponent<Image>().enabled = true;
        items[1].Selected.GetComponent<Image>().enabled = false;
        items[2].Selected.GetComponent<Image>().enabled = false;
    }

    public void CargarMenu(GameObject pausa, GameObject defeat, GameObject victory)
    {
        PausePanel = pausa;
        DefeatPanel = defeat;
        VictoryPanel = victory;


    }


    public void AddCoin(Currency coin)
    {
        coin.OnCoinGrab.AddListener(GrabCoin);
    }
    public void GrabCoin()
    {
        sumCoins.Invoke();
        StatManager.Instance.collectedCurrencyRun++;
        currency++;
        ui.UpdateUi();
    }
    public void buyItem(int price)
    {
        currency -= price;
    }
    public void Pause()
    {
        PausePanel.SetActive(true);
        pause = true;
        Time.timeScale = 0;
    }
    public void Death()
    {
        DefeatPanel.SetActive(true);
        pause = true;
        Time.timeScale = 0;
        StatManager.Instance.endRun();
        StatManager.Instance.loses++;
    }
    public void Victory()
    {
        VictoryPanel.SetActive(true);
        pause = true;
        Time.timeScale = 0;
        StatManager.Instance.endRun();
        StatManager.Instance.wins++;
    }
    public void Continue()
    {
        PausePanel.SetActive(false);
        VictoryPanel.SetActive(false);
        DefeatPanel.SetActive(false);
        pause = false;
        Time.timeScale = 1;
    }


    public void cambiarSelected()
    {
        switch (numSlot)
        {
            case 0:
                items[0].Selected.GetComponent<Image>().enabled = true;
                items[1].Selected.GetComponent<Image>().enabled = false;
                items[2].Selected.GetComponent<Image>().enabled = false;
                break;
            case 1:
                items[0].Selected.GetComponent<Image>().enabled = false;
                items[1].Selected.GetComponent<Image>().enabled = true;
                items[2].Selected.GetComponent<Image>().enabled = false;
                break;
            case 2:
                items[0].Selected.GetComponent<Image>().enabled = false;
                items[1].Selected.GetComponent<Image>().enabled = false;
                items[2].Selected.GetComponent<Image>().enabled = true;
                break;
        }
    }
    private void Update()
    {
        if (chatting && Input.GetButtonDown("ActionButton"))
        {
            if (bottomBar.IsCompleted())
            {
                if (bottomBar.IsLastSentence())
                {
                    bottonBarGO.SetActive(false);
                    txtRose++;
                    //Time.timeScale = 1;
                    playerManager.player_Instance.chatBool = false;
                }
                bottomBar.PlayNextSentence();
            }
        }
        if (Input.GetButtonDown("InventoryLeft"))
        {
            numSlot -= 1;
            if (numSlot < 0)
                numSlot = 0;
            cambiarSelected();
        }
        else if (Input.GetButtonDown("InventoryRight"))
        {
            numSlot += 1;
            if (numSlot > 2)
                numSlot = 2;
            cambiarSelected();
        }

        if (Input.GetButtonDown("UseInventory"))
        {
            UseItem(numSlot);
            //inventory.UseItem();
        }

        //text stuff
        //if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1"))
        //{
        //    if (bottomBar.IsCompleted())
        //    {
        //        bottomBar.PlayNextSentence();
        //    }
        //}

    }
    public void UseItem(int numSlot)
    {
        Debug.Log("se usa item");
        if (items[numSlot].isFull == true)
        {
            switch (items[numSlot].name)
            {
                case "Apple Scroll":
                   prefabInv = items[numSlot].prefab;
                        //fruta.transformacion(1);
                    break;
                case "Banana Scroll":
                    prefabInv = items[numSlot].prefab;
                    //fruta.transformacion(2);
                    break;
                case "Peach Scroll":
                    prefabInv = items[numSlot].prefab;
                    //fruta.transformacion(3);
                    break;
            }
            playerManager.player_Instance.FruitScroll(prefabInv);
            EmptySlot(numSlot, GetComponent<Image>());
        }

    }
    public void EmptySlot(int numSlot, Image img)
    {
        items[numSlot].isFull = false;
        items[numSlot].type = ItemType.none;
        //img.sprite = null;
        //img.enabled = false;
        items[numSlot].slotSprite.GetComponent<Image>().sprite = null;
        items[numSlot].slotSprite.GetComponent<Image>().enabled = false;
    }



    //text stuff
    public StoryScene[] Rose;
    public StoryScene[] Crow;
    public int txtRose=0; 
    public int txtCrow=0;

    public GameObject bottonBarGO;
    public BottomBarController bottomBar;

    public void leerChat()
    {
        bottonBarGO.SetActive(true);
        chatting = true;
        bottomBar.PlayScene(Rose[0]);

    }
    public void chatRose()
    {
        if (txtRose<= Rose.Length)
        {
            //Time.timeScale = 0;
            chatting = true;
            bottonBarGO.SetActive(true);
            bottomBar.PlayScene(Rose[txtRose]);
          
        }
        Debug.Log("no hay mas dialogos");

    }
    //public void chatRose(int scene)
    //{
    //    bottonBarGO.SetActive(true);
    //    bottomBar.PlayScene(Rose[scene]);
    //}
    //public void chatCrow()
    //{
    //    if (txtCrow <= Crow.Length)
    //    {
    //        Time.timeScale = 0;
    //        bottonBarGO.SetActive(true);
    //        bottomBar.PlayScene(Crow[txtCrow]);
    //        txtCrow++;
    //    }
    //    Debug.Log("no hay mas dialogos");
    //}
}

[System.Serializable]
public class Item
{
    public bool isFull;

    //public int amount;
    public ItemType type;
    public string name;
    public GameObject Selected;
    public GameObject slotSprite;
    public GameObject prefab;

    //public Item(string itemName, int itemAmount)
    //{
    //    name = itemName;
    //    amount = itemAmount;
    //}
}

