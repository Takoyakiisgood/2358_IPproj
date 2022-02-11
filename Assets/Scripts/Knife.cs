using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    private int cutCount;
    private int sliceCount;
    public GameObject[] cutSequence;
    public GameObject[] sliceSequence;
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
        if (other.gameObject.tag == "RollDough")
        {

            if (timer == 0)
            {

                timer = 0.5f;
                cutCount++;
                //Debug.Log(cutCount);
                if (cutCount < cutSequence.Length)
                {
                    cutSequence[cutCount].SetActive(true);
                    cutSequence[cutCount - 1].SetActive(false);
                }

                if (cutCount == 3)
                {
                    GameManager.instance.cutDough();
                }
            }

        }
        else if (other.gameObject.tag == "Ginger")
        {
            if(timer == 0)
            {
                timer = 0.5f;
                sliceCount++;

                if (sliceCount < sliceSequence.Length)
                {
                    sliceSequence[sliceCount].SetActive(true);
                }

                if (sliceCount == 2)
                {
                    sliceSequence[0].SetActive(false);
                }
            }
            
        }
        
    }

}
