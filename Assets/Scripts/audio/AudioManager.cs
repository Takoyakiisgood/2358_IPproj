/******************************************************************************
Author: Jordan Yeo Xiang Yu

Name of Class: AudioManager

Description of Class: This class control the game audio such as SFX.

Date Created: 13/07/2021
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class AudioManager : MonoBehaviour
{
    public AudioSource sfxSource;
    public static AudioManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    public void playSFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

}
