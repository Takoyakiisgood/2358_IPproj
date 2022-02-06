using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckContents : MonoBehaviour
{
    private int contentCount;
    public GameObject[] contentArray;
    private bool contentChecked;
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "BrownSugar")
        {
            Destroy(other.gameObject);
            contentArray[0].SetActive(true);
            contentCount++;
        }
        else if (other.gameObject.tag == "BlackSesameSeeds")
        {
            Destroy(other.gameObject);
            contentArray[1].SetActive(true);
            contentCount++;
        }

        if (contentCount == 2)
        {
            if (!contentChecked)
            {
                for (int i = contentCount; i > -1; i--)
                {
                    contentArray[i].SetActive(false);
                }

                contentArray[contentCount].SetActive(true);
                contentChecked = true;
            }
            
        }
    }

}
