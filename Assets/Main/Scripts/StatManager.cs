using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    public static StatManager Instance;

    //public GameObject[] Scroll;
    //public int actualScroll;
    //public bool openScroll = false;

    //[SerializeField] private GameObject menuScroll;
    //[SerializeField] private GameObject menuArboles;
    //[SerializeField] private GameObject menuOpciones;


    //contadorcontador daño recibido
    //contador daño infligido 
    //contador enemigos eliminados
    //contador de muertes
    //contador de frutas
    public bool primeraRun = false;
    public int runs = 0;
    [SerializeField] public int wins;
    [SerializeField] public int loses;
    
    [SerializeField] private int damageDeal = 0;
    [SerializeField] private int damageGet = 0;
    [SerializeField] private int enemiesDefeated = 0;
    [SerializeField] private int fruitDeaths = 0;
    [SerializeField] private int collectedCurrency = 0;

    public int damageDealRun;
    public int damageGetRun;
    public int enemiesDefeatedRun;
    public int fruitDeathsRun;
    public int collectedCurrencyRun;   


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
    //private void Update()
    //{
    //    if (Input.GetButtonDown("Fire1") && openScroll)
    //    {
    //        CloseScroll(actualScroll);
    //    }
    //}

    public void endRun() {
        {
            runs++;
            damageDeal += damageDealRun;
            damageGet += damageGetRun;
            enemiesDefeated += enemiesDefeatedRun;
            fruitDeaths += fruitDeathsRun;
            collectedCurrency += collectedCurrencyRun;

            damageDealRun = 0;
            damageGetRun = 0;
            enemiesDefeatedRun = 0;
            fruitDeathsRun = 0;
            collectedCurrencyRun = 0;
        } 
    }

    //public void ShowScroll(int scroll)
    //{
    //    Scroll[scroll].SetActive(true);
    //    actualScroll = scroll;
    //    openScroll = true;


    //}
    //public void firstRun()
    //{
    //    menu.ShowScroll(0);
    //    primeraRun = true;

    //}
    //public void CloseScroll(int scroll)
    //{
    //    Scroll[scroll].SetActive(false);
    //    openScroll = false;
    //    if (primeraRun==true && runs==0)
    //    {
    //        Debug.Log("cumple condicion");
    //        SceneController.instance.LoadScene("GameplayScene ok");
    //    }
    //}
    //public void ocultarMenu(GameObject menu)
    //{
    //    menu.SetActive(false);
    //}

    //public void mostrarMenu(GameObject menu)
    //{
    //    menu.SetActive(true);
    //}


}
