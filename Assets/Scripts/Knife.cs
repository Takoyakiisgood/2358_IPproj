using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    private int cutCount;
    public GameObject[] cutSequence;
    private float timer = 0;

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            timer = 0;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "RollDough")
        {
            
            if (timer == 0)
            {

                timer = 0.5f;
                cutCount++;
                Debug.Log(cutCount);
                if (cutCount < cutSequence.Length)
                {
                    cutSequence[cutCount].SetActive(true);
                    cutSequence[cutCount - 1].SetActive(false);
                }

            }

        }
        
    }

}
