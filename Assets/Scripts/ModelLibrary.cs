
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Experimental.UI.BoundsControl;
using Microsoft.MixedReality.Toolkit.UI;


/******************************************************************************
Author: Ng Hui Ling, Jordan Yeo Xiang Yu

Name of Class: ModelLibrary

Description of Class: This class gets and set all the interactable asset used by the player.

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
    private Vector3[] originalPosition;
    private Quaternion[] originalRotation;

    private void Awake()
    {
        //hide the asset that should be hidden at first
        for (int i = 0; i < SetupAsset.Length; i++)
        {
            SetupAsset[i].SetActive(false);
        }

        //get the boundscontrol, objectmanipulator and nearinteractiongrabble to inactive to not allow the object to be movable
        for (int i = 0; i < InteractableAsset.Length; i++)
        {
            InteractableAsset[i].GetComponent<BoundsControl>().enabled = true;
            InteractableAsset[i].GetComponent<NearInteractionGrabbable>().enabled = true;
            InteractableAsset[i].GetComponent<ObjectManipulator>().enabled = true;
        }
    }

    public void SetOriginalPosition()
    {
        //get the starting transform values of all the interactable objects
        //get the boundscontrol, objectmanipulator and nearinteractiongrabble to active to allow the object to be movable
        for (int i = 0; i < InteractableAsset.Length; i++)
        {
            originalPosition[i] = InteractableAsset[i].transform.position;
            originalRotation[i] = InteractableAsset[i].transform.rotation;
            InteractableAsset[i].GetComponent<BoundsControl>().enabled = true;
            InteractableAsset[i].GetComponent<NearInteractionGrabbable>().enabled = true;
            InteractableAsset[i].GetComponent<ObjectManipulator>().enabled = true;
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

        //transform.position = originalPosition;
        //transform.rotation = originalRotation;
    }
    
}
