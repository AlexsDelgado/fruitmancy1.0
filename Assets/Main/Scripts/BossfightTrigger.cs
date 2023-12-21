using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossfightTrigger : MonoBehaviour
{
    public bool bossFightStarted = false;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("PlayerManager")) && !bossFightStarted)
        {
            bossFightStarted=true;
        }
    }
}
