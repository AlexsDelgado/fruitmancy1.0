using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject[] dungeons;
    public GameObject[] Scroll;
    public int actualScroll;
    public bool openScroll = false;

    [SerializeField] private GameObject menuScroll;
    [SerializeField] private GameObject menuArboles;
    [SerializeField] private GameObject menuOpciones;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && openScroll)
        {
            CloseScroll(actualScroll);
        }
    }


    public void ShowScroll(int scroll)
    {
        Scroll[scroll].SetActive(true);
        actualScroll = scroll;
        openScroll = true;


    }
    public void firstRun()
    {
        ShowScroll(0);
        //primeraRun = true;

    }
    public void CloseScroll(int scroll)
    {
        Scroll[scroll].SetActive(false);
        openScroll = false;
        if (StatManager.Instance.primeraRun == true && StatManager.Instance.runs == 0)
        {
            Debug.Log("cumple condicion");
            SceneController.instance.LoadScene("GameplayScene ok");
        }
    }
    public void ocultarMenu(GameObject menu)
    {
        menu.SetActive(false);
    }
    public void mostrarMenu(GameObject menu)
    {
        menu.SetActive(true);
    }
    void Start()
    {
        
    }
    public void Play()
    {
        dungeons[0].SetActive(true);
        dungeons[1].SetActive(true);
        dungeons[2].SetActive(true);
    }
}
