
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
    private GameObject[] InteractableAsset;

    [SerializeField]
    private GameObject[] SetupAsset;
    /*
     * Asset Variables
     */
    [SerializeField]
    private List<Vector3> originalPosition;
    [SerializeField]
    private List<Quaternion> originalRotation;

    private void Start()
    {
        //hide the asset that should be hidden at first
        for (int i = 0; i < SetupAsset.Length; i++)
        {
            if (i == 0)
            {
                SetupAsset[i].SetActive(true);
            }
            else 
            {
                SetupAsset[i].SetActive(false);
            }
            
        }

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
    }

    public void SetOriginalPosition()
    {
        //get the starting transform values of all the interactable objects
        //get the boundscontrol, objectmanipulator and nearinteractiongrabble to active to allow the object to be movable
        for (int i = 0; i < InteractableAsset.Length; i++)
        {
           
            originalPosition.Add(InteractableAsset[i].transform.position);
            originalRotation.Add(InteractableAsset[i].transform.rotation);

            if (InteractableAsset[i].GetComponent<NearInteractionGrabbable>() != null)
            {
                InteractableAsset[i].GetComponent<NearInteractionGrabbable>().enabled = true;
            }
            if (InteractableAsset[i].GetComponent<ObjectManipulator>() != null)
            {
                InteractableAsset[i].GetComponent<ObjectManipulator>().enabled = true;
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
        for (int i = 0; i < InteractableAsset.Length; i++)
        {
            InteractableAsset[i].transform.position = originalPosition[i];
            InteractableAsset[i].transform.rotation = originalRotation[i];
        }
    }
    
}
