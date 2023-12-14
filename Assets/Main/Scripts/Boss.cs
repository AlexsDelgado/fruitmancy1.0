using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private Ant hormiga;
    private int vida;
    // Start is called before the first frame update
    void Start()
    {
        hormiga = this.GetComponent<Ant>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
