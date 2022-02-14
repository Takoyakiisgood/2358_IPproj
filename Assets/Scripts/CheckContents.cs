using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckContents : MonoBehaviour
{
    private int contentCount;
    public GameObject[] contentArray;
    public GameObject[] finalContent;
    public string[] contentName;
    private bool contentChecked;
    public bool hasChecked;
    private int gingerCount;

    public Material pasteMaterial;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == contentName[0])
        {
            contentArray[0].SetActive(true);
            contentCount++;
            
        }
        else if (other.gameObject.name == contentName[1])
        {
            if(hasChecked == false)
            {
                Destroy(other.gameObject);
                contentArray[1].SetActive(true);
                contentCount++;
                hasChecked = true;
            }
            
        }
        else if (other.gameObject.name == contentName[2])
        {
            Destroy(other.gameObject);
            pasteMaterial.SetFloat("_Smoothness", 0.93f);
        }
        if (contentCount == 2)
        {
            if (!contentChecked)
            {
                for (int i = 0; i < contentArray.Length -1; i++)
                {
                    contentArray[i].SetActive(false);
                }
                contentArray[contentCount].SetActive(true);
                GameManager.instance.mixPasteWithButter();
                contentChecked = true;
            }

        }
        else if (other.gameObject.tag == "FinalIngredients")
        {
            if (other.gameObject.name == contentName[0])
            {
                finalContent[0].SetActive(true);
            }
            else if (other.gameObject.name == contentName[1])
            {
                Destroy(other.gameObject);
                finalContent[1].SetActive(true);
            }
            else if (other.gameObject.name == contentName[2])
            {
                Destroy(other.gameObject);
                gingerCount++;
                if (gingerCount == 1)
                {
                    finalContent[2].SetActive(true);
                }
                else if(gingerCount == 2)
                {
                    finalContent[3].SetActive(true);
                }
                
            }
        }
        else
        {
            
        }
        

        
    }

}
