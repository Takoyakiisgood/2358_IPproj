using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laddle : MonoBehaviour
{
    public GameObject scoopOfSoup;
    public GameObject tangYuanSoup;
    private int scoopCount;
    private bool hasChecked;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "TangYuan")
        {
            tangYuanSoup.SetActive(true);

        }
        else if (other.gameObject.tag == "EmptyBowl")
        {
            scoopCount++;
            GameObject scoop = Instantiate(tangYuanSoup, other.gameObject.transform.position, Quaternion.identity);
            if (!hasChecked)
            {
                scoop.transform.parent = other.gameObject.transform;
                hasChecked = true;
            }
            
            if (scoopCount == 1)
            {
                scoop.gameObject.transform.Find(scoopCount + "/4 Scoop").gameObject.SetActive(true);
                tangYuanSoup.SetActive(false);
            }
            else if(scoopCount <5)
            {
                scoop.gameObject.transform.Find(scoopCount + "/4 Scoop").gameObject.SetActive(true);
                scoop.gameObject.transform.Find(scoopCount-1 + "/4 Scoop").gameObject.SetActive(false);
                tangYuanSoup.SetActive(false);
            }
            

            
        }
    }

}
