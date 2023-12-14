using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BarraDeVida : MonoBehaviour
{
    private Slider slider;
    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void ChangeMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
    }
    public void ChangeActualHealth(int healthAmount)
    {
        slider.value = healthAmount;
    }

    public void initializeHealthBar(int healthAmount)
    {
        ChangeMaxHealth(healthAmount);
        ChangeActualHealth(healthAmount);
    }
}
