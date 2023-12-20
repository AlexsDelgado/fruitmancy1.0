using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volumen : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private void Start()
    {
        //SoundManager.Instance.ChangeMasterVolume(slider.value);
        slider.onValueChanged.AddListener(val => SoundManager.Instance.ChangeMasterVolume(val));
    }
}
