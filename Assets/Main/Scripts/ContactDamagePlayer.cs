using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDamagePlayer : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerManager.player_Instance.TakeDamage(1, Vector2.zero);
        }
    }
}
