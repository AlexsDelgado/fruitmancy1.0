using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSound : MonoBehaviour
{
    [SerializeField] private bool toggleMusic;
    [SerializeField] private bool toggleEffects;

    public void Toggle()
    {
        if (toggleEffects) SoundManager.Instance.ToggleSfx();
        if (toggleMusic) SoundManager.Instance.ToggleBgm();

    }


}
