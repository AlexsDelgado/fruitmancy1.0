using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mantener : MonoBehaviour
{

    private Animator m_animator;

    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.position.x = playerManager.player_Instance.posicion.x;
        if (playerManager.player_Instance.status!=0)
        {
            var l_horizontal = Input.GetAxisRaw("Horizontal");
            var l_vertical = Input.GetAxisRaw("Vertical");

            bool l_isRunning = l_horizontal != 0;

            m_animator.SetBool("walking", l_isRunning);
        }
        var p = this.transform.position;
        p.x = playerManager.player_Instance.posicion.x;
        p.y = playerManager.player_Instance.posicion.y;
        //p.z = -10;
        this.transform.position = p;
       


    }
}
