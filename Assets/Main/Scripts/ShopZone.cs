using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopZone : MonoBehaviour
{
    public static ShopZone Instance;
    GameManager currency;
    public Text coinsText;
    public GameObject currencyHUD;

    public void UpdateUi()
    {
        coinsText.text = "Monedas: " + currency.currency;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            currencyHUD.SetActive(true); // activar el HUD de currency
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            currencyHUD.SetActive(false); // desactivar el HUD de currency
        }
    }
}