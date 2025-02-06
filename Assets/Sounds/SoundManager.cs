using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [Header("Sound Reference")]
    public List<AudioClip>                          ListSounds;

    public Dictionary<string, AudioSource>          DictSounds;

    protected override void InAwake()
    {
        DictSounds = new Dictionary<string, AudioSource>();

        foreach (var sound in ListSounds)
        {
            AudioSource audioSrc = transform.AddComponent<AudioSource>();
            audioSrc.clip = sound;
            audioSrc.playOnAwake = false;
            DictSounds.Add(sound.name, audioSrc);
        }
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

    public void RocketExplosion()
    {
        DictSounds["RocketExplosion"].Play();
    }
}
