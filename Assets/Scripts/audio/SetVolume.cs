
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Microsoft.MixedReality.Toolkit.UI;
/******************************************************************************
Author: Huiling, Celest, Esther
Name of Class: SetVolume
Description of Class: This controls the volume master of the sound to be controlled by the slider
Date Created: 11/8/21
******************************************************************************/
public class SetVolume : MonoBehaviour
{
    /*
     * Editor Exposed Variables
     */
    [Header("Configuration")]

    /// <summary>
    /// reference to the audio mixer
    /// </summary>
    [SerializeField]
    private AudioMixer mixer;
    /// <summary>
    /// reference to the slider UI
    /// </summary>
    [SerializeField]
    private PinchSlider Slider;
    /// <summary>
    /// reference the the vol variable the controls all the volume
    /// </summary>
    [SerializeField]
    private string volVariable;
    // Start is called before the first frame update
    void Start()
    {
        //Set the default volume to 0.75
        if (volVariable != null)
        {
            Debug.Log(Slider.SliderValue);           
            Slider.SliderValue = 0.75f;
            PlayerPrefs.GetFloat(volVariable, 0.75F);
        }

    }

    public void SetLevel(float sliderValue)
    {
        if (volVariable != null)
        {
            sliderValue = Slider.SliderValue;
            mixer.SetFloat(volVariable, Mathf.Log10(sliderValue) * 20);
            PlayerPrefs.SetFloat(volVariable, sliderValue);
        }       
    }
}
