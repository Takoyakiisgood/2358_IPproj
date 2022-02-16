using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public static Knife instance;
    private int cutCount;
    private int sliceCount;
    public GameObject[] cutSequence;
    public GameObject[] sliceSequence;
    private float timer = 0;

    private void Start()
    {
        instance = this;
    }
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

                if (sliceCount < sliceSequence.Length-1)
                {
                    sliceSequence[sliceCount].SetActive(true);
                }

                if (sliceCount == 1)
                {
                    sliceSequence[0].SetActive(false);
                    sliceSequence[sliceCount].SetActive(true);
                }
                else if(sliceCount == 2)
                {
                    sliceSequence[sliceCount].SetActive(true);
                    sliceSequence[sliceCount+1].SetActive(true);
                    sliceSequence[4].SetActive(false);

                    //Update sub task
                    GameManager.instance.ChopGinger();
                }
            }
            
        }
        
    }

    public void Reset()
    {
        cutCount = 0;
    }
    public void ResetStep4()
    {
        sliceCount = 0;
        sliceSequence[1].SetActive(true);
        sliceSequence[2].SetActive(false);
        sliceSequence[3].SetActive(false);
        sliceSequence[4].SetActive(true);
        sliceSequence[0].SetActive(true);
    }

}
