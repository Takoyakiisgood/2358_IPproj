﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spoon : MonoBehaviour
{
    public Transform spawnPosition;
    public GameObject prefab;
    public GameObject[] plateContent;
    public int sesameCount;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Paste")
        {
            GameObject mySpoon =  Instantiate(prefab, spawnPosition.position, Quaternion.identity);
            GameManager.instance.scoopPaste();
            mySpoon.transform.parent = transform;
        }

        else if(other.gameObject.tag == "Plate")
        {
            Debug.Log("collide with plate");
            foreach (Transform obj in gameObject.transform)
            {
                Debug.Log(obj.name);
                if (obj.tag == "RoundDough")
                {
                    if(sesameCount < 4)
                    Destroy(obj.gameObject);
                    plateContent[sesameCount].SetActive(true);
                    sesameCount++;
                }
            }
        }
    }

}
