using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public Button playButton;
    //public Button creditsButton;

    void Start()
    {
        SetManager();
    }

    public void SetManager()
    {
        playButton.onClick.RemoveAllListeners();
        playButton.onClick.AddListener(SceneController.instance.jugar);
        //creditsButton.onClick.RemoveAllListeners();
        //creditsButton.onClick.AddListener(SceneController.instance.jugar);
    }






}