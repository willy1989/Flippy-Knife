using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource winSound;
    [SerializeField] private AudioSource loseSound;
    [SerializeField] private AudioSource cutSound;
    [SerializeField] private AudioSource stabSound;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(this);
        }
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
