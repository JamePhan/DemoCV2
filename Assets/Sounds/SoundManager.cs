using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : Singleton<SoundManager>
{
    [Header("Sound Reference")]
    public  List<AudioClip>                             ListSounds;
    public  Dictionary<string, AudioSource>             DictSounds;    
    private float                                       totalVolume = 1f;
    public  Slider                                      slider;

    protected override void InAwake()
    {
        DictSounds = new Dictionary<string, AudioSource>();
        if (PlayerPrefs.HasKey("Volume")) totalVolume = PlayerPrefs.GetFloat("Volume");
        else totalVolume = 1f;
        slider.value = totalVolume;
        foreach (var sound in ListSounds)
        {
            AudioSource audioSrc = transform.AddComponent<AudioSource>();
            audioSrc.clip = sound;
            audioSrc.playOnAwake = false;
            audioSrc.volume = totalVolume;
            DictSounds.Add(sound.name, audioSrc);
        }
        slider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float newVolume)
    {
        totalVolume = Mathf.Clamp01(newVolume);
        foreach (var audio in DictSounds.Values)
        {
            audio.volume = totalVolume;
        }
        PlayerPrefs.SetFloat("Volume", totalVolume);
        PlayerPrefs.Save();
    }

    public void UI_Button()
    {
        DictSounds["Button"].Play();
    }

    public void UI_Close()
    {
        DictSounds["Close"].Play();
    }

    public void UI_Coins()
    {
        DictSounds["Coins"].Play();
    }

    public void UI_Magical()
    {
        DictSounds["Magical"].Play();
    }

    public void UI_Win()
    {
        DictSounds["Win"].Play();
    }

    public void Shoot()
    {
        DictSounds["Shoot"].Play();
    }

    public void RocketSpawn()
    {
        DictSounds["RocketSpawn"].Play();
    }

    public void BulletSpawn()
    {
        DictSounds["AK"].Play();
    }


    public void RocketExplosion()
    {
        DictSounds["RocketExplosion"].Play();
    }
}
