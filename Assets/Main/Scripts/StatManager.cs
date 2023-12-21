using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    public static StatManager Instance;
  
    public GameObject[] Scroll;
    public int actualScroll;
    public bool openScroll = false;

    //contadorcontador daño recibido
    //contador daño infligido 
    //contador enemigos eliminados
    //contador de muertes
    //contador de frutas

    public int runs = 0;
    
    private int damageDeal = 0;
    public int damageDealRun;

    private int damageGet = 0;
    public int damageGetRun;

    private int enemiesDefeated = 0;
    public int enemiesDefeatedRun;

    private int fruitDeaths = 0;
    public int fruitDeathsRun;

    private int collectedCurrency = 0;
    public int collectedCurrencyRun;

    public int loses;
    public int wins;

    


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
    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && openScroll)
        {
            CloseScroll(actualScroll);
        }
    }

    private void endRun() {
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

    public void ShowScroll(int scroll)
    {
        Scroll[scroll].SetActive(true);
        actualScroll = scroll;
        openScroll = true;
        

    }
    public void CloseScroll(int scroll)
    {
        Scroll[scroll].SetActive(false);
    }



}
