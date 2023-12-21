using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDamage : MonoBehaviour
{
    public bool ExplosionFinished = false;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerManager.player_Instance.TakeDamage(1, Vector2.zero);
        }
    }

    public void finishExplosion()
    {
        ExplosionFinished = true;
    }
}

