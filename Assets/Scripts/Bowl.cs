using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowl : MonoBehaviour
{
    public GameObject[] bowlContents;
    private int scoopCount;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Laddle")
        {
            if (Laddle.hasSoup == true)
            {
                GameManager.instance.scoopTangYuan();
                scoopCount++;
                GameManager.instance.scoopTangYuan();
                if (scoopCount == 1)
                {
                    bowlContents[scoopCount].SetActive(true);
                }
                else if (scoopCount < 4)
                {
                    bowlContents[scoopCount].SetActive(true);
                    bowlContents[scoopCount - 1].SetActive(false);
                }
                Laddle.hasSoup = false;

            }
            
        }
    }
    private void Update()
    {
        if (GameManager.instance.isReseted())
        {
            if (GameManager.instance.currentTask == "cookTangYuan")
            {
                Reset();
                GameManager.instance.ToggleReset();
            }

        }
    }
    private void Reset()
    {
        for(int i = 0; i<bowlContents.Length; i++)
        {
            bowlContents[i].SetActive(false);
        }
        scoopCount = 0;
    }
}
