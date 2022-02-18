using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
public class Boiling : MonoBehaviour
{
    public static Boiling instance;
    public ParticleSystem[] boilContents;
    public ParticleSystem[] fireArray;
    private float timer;
    private float duration = 10f;
    private bool startBoil;
    public bool hasOn; 
    public Animator animator;
    public AudioClip boilingSFX;
    public GameObject feedbackTip;
    private void Start()
    {
        instance = this;
    }
    private void Update()
    {
        //Debug.Log(timer);
        if (startBoil == true) 
        {
            Debug.Log("START");
            timer -= Time.deltaTime;
            feedbackTip.SetActive(true);
            feedbackTip.GetComponent<ToolTip>().ToolTipText = timer.ToString();
            if (timer < 0)
            {
                BoilWater();
                startBoil = false;
                feedbackTip.SetActive(false);
            }
        }
    }

    public void StartFire()
    {
        if (!hasOn)
        {
            animator.SetBool("KnobOn", true);
            hasOn = true;
            for (int i = 0; i < fireArray.Length; i++)
            {
                fireArray[i].Play();
            }
        }
        else
        {
            animator.SetBool("KnobOn", false);
            hasOn = false;
            StopBoiling();
            for (int i = 0; i < fireArray.Length; i++)
            {
                fireArray[i].Stop();
            }

        }
        
    }
    public void StartTimer()
    {
        timer = duration;
        startBoil = true;
    }
    private void BoilWater()
    {
        for (int i = 0; i < boilContents.Length; i++)
        {
            boilContents[i].Play();
        }
        GameManager.instance.BoilWater();
        GetComponent<AudioSource>().Play();
        
    }
    
    private void StopBoiling()
    {
        startBoil = false;
        for (int i = 0; i < boilContents.Length; i++)
        {
            boilContents[i].Stop();
        }
        GetComponent<AudioSource>().Stop();
    }
}
