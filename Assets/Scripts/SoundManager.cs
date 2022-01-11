using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource winSound;
    [SerializeField] private AudioSource loseSound;
    [SerializeField] private AudioSource cutSound;
    [SerializeField] private AudioSource stabSound;

    private void Awake()
    {
        SetInstance();
    }

    public void PlayWinSound()
    {
        winSound.Play();
    }

    public void PlayLoseSound()
    {
        loseSound.Play();
    }

    public void PlayJumpSound()
    {
        cutSound.Play();
    }
    public void PlayStabSound()
    {
        stabSound.Play();
    }
}
