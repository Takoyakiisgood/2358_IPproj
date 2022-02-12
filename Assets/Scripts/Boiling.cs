using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boiling : MonoBehaviour
{
    public ParticleSystem[] boilContents;
    private float timer;
    private float duration = 10f;
    private bool startBoil;
    private void Update()
    {
        Debug.Log(timer);
        if (startBoil == true) 
        {
            Debug.Log("START");
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                BoilWater();
                startBoil = false;
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
    }
}
