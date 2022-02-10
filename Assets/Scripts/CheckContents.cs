using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckContents : MonoBehaviour
{
    private int contentCount;
    public GameObject[] contentArray;
    public string[] contentName;
    private bool contentChecked;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name == contentName[0])
        {
            Destroy(other.gameObject);
            contentArray[0].SetActive(true);
            contentCount++;
        }
        else if (other.gameObject.name == contentName[1])
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
