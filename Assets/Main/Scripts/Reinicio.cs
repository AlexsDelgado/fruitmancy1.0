using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reinicio : MonoBehaviour
{
    private SceneController scenemanager;
    // Start is called before the first frame update
    void Start()
    {
        scenemanager = SceneController.instance;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void reiniciar()
    {
        scenemanager.retry();
    }
}
