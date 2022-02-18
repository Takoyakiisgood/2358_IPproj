using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
Author: Ng Hui Ling

Name of Class: UiLibrary

Description of Class: This class gets and control all the UI page sharing the same parent.

Date Created: 12/02/2022
******************************************************************************/

public class UiLibrary : MonoBehaviour
{
    [Header("Configuration")]
    [Tooltip("The reference to UI gameobject")]
    [SerializeField]
    private GameObject[] UIPages;
    [SerializeField]
    private string PageToAppearFirst;
    [SerializeField]
    private List<Vector3> UIPagesPos;
    [SerializeField]
    private List<Quaternion> UIPagesRotate;
    // Start is called before the first frame update
    void Start()
    {
        if (UIPages != null)
        {
            //hide the asset that should be hidden at first
            for (int i = 0; i < UIPages.Length; i++)
            {
                //set all the pages to be inactive at first
                UIPages[i].SetActive(false);
                UIPagesPos.Add(UIPages[i].transform.position);
                UIPagesRotate.Add(UIPages[i].transform.rotation);
                //the first page the should appear first is the signIn Page 
                if (UIPages[i].name == PageToAppearFirst)
                {
                    UIPages[i].SetActive(true);
                }
            }
        }
        else
        {
            Debug.Log("UI pages is empty, please assign it");
        }
    }

    public void HideAllPages()
    {
        if (UIPages != null)
        {
            //hide the asset that should be hidden at first
            for (int i = 0; i < UIPages.Length; i++)
            {
                //set all the pages to be inactive at first
                UIPages[i].SetActive(false);
            }
        }
        else
        {
            Debug.Log("UI pages is empty, please assign it");
        }
    }

    public void Reset()
    {
        for (int i = 0; i < UIPages.Length; i++)
        {
            UIPages[i].transform.position = UIPagesPos[i];
            UIPages[i].transform.rotation = UIPagesRotate[i];
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
