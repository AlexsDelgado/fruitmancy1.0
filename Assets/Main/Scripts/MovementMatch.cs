using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementMatch : MonoBehaviour
{
    void Update()
    {
        var p = transform.parent;
        this.transform.position = p.position;
        this.transform.rotation = p.rotation;
    }
}
