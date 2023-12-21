using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioSource sfx;
    [SerializeField] private AudioSource bgm;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        AudioListener.volume = 0.2f;
    }

    public void ToggleSfx()
    {
        sfx.mute = !sfx.mute;
    }
    public void ToggleBgm()
    {
        bgm.mute = !bgm.mute;
    }
    public void PlaySound(AudioClip clip)
    {
        sfx.PlayOneShot(clip);
    }
    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }

}
