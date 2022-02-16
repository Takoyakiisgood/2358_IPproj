
using UnityEngine;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;

/******************************************************************************
Author: Ng Hui Ling, Jordan Yeo Xiang Yu

Name of Class: ModelLibrary

Description of Class: This class gets and set all the interactable asset transform values.

Date Created: 12/02/2022
******************************************************************************/

public class ModelLibrary : MonoBehaviour
{
    /*
     * Editor Exposed Variables
     */
    [Header("Configuration")]
    [Tooltip("The reference to Assets of the interactables")]

    [SerializeField]
    private GameObject[] SetupAsset;

    [SerializeField]
    private GameObject[] Step1Asset, Step2Asset, Step3Asset, Step4Asset, Step5Asset;

    [SerializeField]
    private List<Vector3> Step1OriginalTransform, Step2OriginalTransform, Step3OriginalTransform, Step4OriginalTransform, Step5OriginalTransform;
    [SerializeField]
    private List<Quaternion> Step1OriginalRotation, Step2OriginalRotation, Step3OriginalRotation, Step4OriginalRotation, Step5OriginalRotation;
    private void Start()
    {
        //hide the asset that should be hidden at first
        for (int i = 0; i < SetupAsset.Length; i++)
        {
            SetupAsset[i].SetActive(false);
            //if (i == 0)
            //{
            //    SetupAsset[i].SetActive(true);
            //}
            //else 
            //{
            //    SetupAsset[i].SetActive(false);
            //}
            
        }
        /**
        //get the objectmanipulator and nearinteractiongrabble to inactive to not allow the object to be movable
        for (int i = 0; i < InteractableAsset.Length; i++)
        {
            if (InteractableAsset[i].GetComponent<NearInteractionGrabbable>() != null)
            {
                InteractableAsset[i].GetComponent<NearInteractionGrabbable>().enabled = false;
            }
            if (InteractableAsset[i].GetComponent<ObjectManipulator>() != null)
            {
                InteractableAsset[i].GetComponent<ObjectManipulator>().enabled = false;
            }
                     
        }
        **/
    }
    private void Update()
    {
        if (GameManager.instance.isReseted())
        {
            ResetTransforms();
            GameManager.instance.ToggleReset();
        }

    }
    public void SetOriginalPosition()
    {
        //get the starting transform values of all the interactable objects
        //get the boundscontrol, objectmanipulator and nearinteractiongrabble to active to allow the object to be movable
        for (int i = 0; i < Step1Asset.Length; i++)
        {
           
            Step1OriginalTransform.Add(Step1Asset[i].transform.position);
            Step1OriginalRotation.Add(Step1Asset[i].transform.rotation);
            if (Step1Asset[i].GetComponent<NearInteractionGrabbable>() != null)
            {
                Step1Asset[i].GetComponent<NearInteractionGrabbable>().enabled = true;
            }
            if (Step1Asset[i].GetComponent<ObjectManipulator>() != null)
            {
                Step1Asset[i].GetComponent<ObjectManipulator>().enabled = true;
            }

        }

        for (int i = 0; i < Step2Asset.Length; i++)
        {

            Step2OriginalTransform.Add(Step2Asset[i].transform.position);
            Step2OriginalRotation.Add(Step2Asset[i].transform.rotation);

            if (Step2Asset[i].GetComponent<NearInteractionGrabbable>() != null)
            {       
                Step2Asset[i].GetComponent<NearInteractionGrabbable>().enabled = true;
            }       
            if (Step2Asset[i].GetComponent<ObjectManipulator>() != null)
            {       
                Step2Asset[i].GetComponent<ObjectManipulator>().enabled = true;
            }

        }
        for (int i = 0; i < Step3Asset.Length; i++)
        {

            Step3OriginalTransform.Add(Step3Asset[i].transform.position);
            Step3OriginalRotation.Add(Step3Asset[i].transform.rotation);
            if (Step3Asset[i].GetComponent<NearInteractionGrabbable>() != null)
            {
                Step3Asset[i].GetComponent<NearInteractionGrabbable>().enabled = true;
            }
            if (Step3Asset[i].GetComponent<ObjectManipulator>() != null)
            {
                Step3Asset[i].GetComponent<ObjectManipulator>().enabled = true;
            }

        }
        for (int i = 0; i < Step4Asset.Length; i++)
        {

            Step4OriginalTransform.Add(Step4Asset[i].transform.position) ;
            Step4OriginalRotation.Add(Step4Asset[i].transform.rotation);

            if (Step4Asset[i].GetComponent<NearInteractionGrabbable>() != null)
            {
                Step4Asset[i].GetComponent<NearInteractionGrabbable>().enabled = true;
            }
            if (Step4Asset[i].GetComponent<ObjectManipulator>() != null)
            {
                Step4Asset[i].GetComponent<ObjectManipulator>().enabled = true;
            }

        }
        for (int i = 0; i < Step5Asset.Length; i++)
        {

            Step5OriginalTransform.Add(Step5Asset[i].transform.position) ;
            Step5OriginalRotation.Add(Step5Asset[i].transform.rotation);

            if (Step5Asset[i].GetComponent<NearInteractionGrabbable>() != null)
            {
                Step5Asset[i].GetComponent<NearInteractionGrabbable>().enabled = true;
            }
            if (Step5Asset[i].GetComponent<ObjectManipulator>() != null)
            {
                Step5Asset[i].GetComponent<ObjectManipulator>().enabled = true;
            }

        }
        for (int i = 0; i < SetupAsset.Length; i++)
        {
            //after the setup is done, set the collider to inactive. This is done to not affect the interactable assets from being selected
            SetupAsset[i].GetComponent<BoxCollider>().enabled = false;            
        }
    }

    /// Function to reset the transforms of this object
    /// </summary>
    public void ResetTransforms()
    {
        if(GameManager.instance.currentTask == "prepFilling")
        {
            for (int i = 0; i < Step1Asset.Length; i++)
            {
                Step1Asset[i].transform.position = Step1OriginalTransform[i];
                Step1Asset[i].transform.rotation = Step1OriginalRotation[i];
                Step1Asset[i].SetActive(true);
            }
            
        }
        else if(GameManager.instance.currentTask == "prepDough")
        {
            for (int i = 0; i < Step2Asset.Length; i++)
            {
                Step2Asset[i].transform.position = Step2OriginalTransform[i];
                Step2Asset[i].transform.rotation = Step2OriginalRotation[i];
                if(Step2Asset[i].tag != "Dough")
                {
                    Step2Asset[i].SetActive(true);
                }
                
            }
        }
        else if(GameManager.instance.currentTask == "prepTangYuan")
        {
            for (int i = 0; i < Step3Asset.Length; i++)
            {
                Step3Asset[i].transform.position = Step3OriginalTransform[i];
                Step3Asset[i].transform.rotation = Step3OriginalRotation[i];
                Step3Asset[i].SetActive(true);
            }
        }
        else if(GameManager.instance.currentTask == "prepSoup")
        {
            for (int i = 0; i < Step4Asset.Length; i++)
            {
                Step4Asset[i].transform.position = Step4OriginalTransform[i];
                Step4Asset[i].transform.rotation = Step4OriginalRotation[i];
                if(Step4Asset[i].name == "PandanLeafSet" || Step4Asset[i].name == "Ginger Slices" || Step4Asset[i].name == "brownSugar")
                {
                    Step4Asset[i].SetActive(false);
                }
                else
                {
                    Step4Asset[i].SetActive(true);
                }
                
            }
        }
        else if (GameManager.instance.currentTask == "cookTangYuan")
        {
            for (int i = 0; i < Step5Asset.Length; i++)
            {
                Step5Asset[i].transform.position = Step5OriginalTransform[i];
                Step5Asset[i].transform.rotation = Step5OriginalRotation[i];

                if(Step5Asset[i].name != "Laddle")
                {
                    Step5Asset[i].SetActive(false);
                }
            }
        }
    }   


}
