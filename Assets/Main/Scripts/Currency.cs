using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Currency : MonoBehaviour
{
    public UnityEvent OnCoinGrab = new UnityEvent();
    private void Start()
    {
        GameManager.Instance.AddCoin(this);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)//layer del jugador
        {
            OnCoinGrab.Invoke();
            gameObject.SetActive(false);
        }
    }
}