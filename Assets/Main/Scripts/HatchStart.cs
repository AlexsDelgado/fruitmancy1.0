using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatchStart : MonoBehaviour
{
    public GameObject Ladder;
    private bool touching = false;
    Collider2D otherCollider;
    private void Update()
    {
        if (touching && Input.GetKeyDown(KeyCode.T))
        {
            otherCollider.transform.position = Ladder.transform.position;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerManager"))
        {
            otherCollider = other;
            touching = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PlayerManager"))
        {
            touching = false;
        }
    }
}
