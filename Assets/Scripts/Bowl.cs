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

}
