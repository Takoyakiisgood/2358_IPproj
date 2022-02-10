/******************************************************************************
Author: Jordan Yeo Xiang Yu

Name of Class: OptionsMenu

Description of Class: This class will control the volume of the game, quality and Quit game button.

Date Created: 18/12/2021
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;       //Slider for Master Volume
    public Slider slider2;      //Slider for Music Volume
    public Slider slider3;      //Slider for SFX Volume


    void Start()
    {
        //Load user previous game volume settings or else set default 0.75f
        slider.value = PlayerPrefs.GetFloat("MasterVolume", 0.75f);
        slider2.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        slider3.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);

    }



    public void SetMasterLevel(float sliderValue)
    {
        //Set volume based on the slider
        mixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);

        //Save volume data to prefab
        PlayerPrefs.SetFloat("MasterVolume", sliderValue);
    }

    public void SetBGMLevel(float sliderValue)
    {
        //Set volume based on the slider
        mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);

        //Save volume data to prefab
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
    }

    public void SetSFXLevel(float sliderValue)
    {
        //Set volume based on the slider
        mixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20);

        //Save volume data to prefab
        PlayerPrefs.SetFloat("SFXVolume", sliderValue);
    }

}
