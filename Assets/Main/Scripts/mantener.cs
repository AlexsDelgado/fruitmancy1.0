using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mantener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.position.x = playerManager.player_Instance.posicion.x;

        var p = this.transform.position;
        p.x = playerManager.player_Instance.posicion.x;
        p.y = playerManager.player_Instance.posicion.y;
        //p.z = -10;
        this.transform.position = p;


    }
}
