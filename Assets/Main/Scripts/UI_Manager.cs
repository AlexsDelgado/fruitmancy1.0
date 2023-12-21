using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager UI;
    [SerializeField] private GameObject[] Hearts;
    [SerializeField] private int health = 3;

    [SerializeField] Image bossHealthFill;
    [SerializeField] GameObject bossHealth;

    private void Awake()
    {
        if (UI == null)
        {
            UI = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerManager.player_Instance.recibirDmg.AddListener(changeLife);
        playerManager.player_Instance.frutamancia.AddListener(loadLife);

        Hearts[1].gameObject.SetActive(false);
        Hearts[2].gameObject.SetActive(false);
    }

    public void DisplayBossHealth(bool toggle)
    {
        bossHealth.SetActive(toggle);
    }

    public void UpdateBossHealth(float currentHealth, float maxHealth)
    {
        bossHealthFill.fillAmount = Mathf.Clamp(currentHealth / maxHealth, 0, 1f);
    }

    public void changeLife()
    {
        health--;
        switch (health)
        {
            case 0:
                Hearts[0].gameObject.SetActive(false);
                break;

            case 1:
                Hearts[1].gameObject.SetActive(false);
                break;
            case 2:
                Hearts[2].gameObject.SetActive(false);
                break;

        }
    }
    public void loadLife()
    {
        health = 3;
        Hearts[0].gameObject.SetActive(true);
        Hearts[1].gameObject.SetActive(true);
        Hearts[2].gameObject.SetActive(true);
    }
    public void Retry()
    {

        SceneController.instance.retry();
    }

    public void MainMenu()
    {
        GameManager.Instance.Continue();
        SceneController.instance.MainMenu();
    }
}
