using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableHighlight : MonoBehaviour
{
    //highlight the object using emission property
    public void OnHover()
    {
        //get all the mesh renderers
        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();

        //go through all the renderers and turn on the emission property in each material
        foreach (MeshRenderer renderer in meshRenderers)
        {
            //turn on the emission property
            renderer.material.EnableKeyword("_EMISSION");
        }

        //for skinned mesh render
        SkinnedMeshRenderer[] SmeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

        //go through all the renderers and turn on the emission property in each material
        foreach (SkinnedMeshRenderer renderer in SmeshRenderers)
        {
            //turn on the emission property
            renderer.material.EnableKeyword("_EMISSION");
        }
    }

    //stop highlight the object
    public void ExitHover()
    {
        //get all the mesh renderers
        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();

        //go through all the renderers and turn on the emission property in each material
        foreach (MeshRenderer renderer in meshRenderers) 
        {
            //turn off the emission property
            renderer.material.DisableKeyword("_EMISSION");
        }

        //for skinned mesh render
        SkinnedMeshRenderer[] SmeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

        //go through all the renderers and turn on the emission property in each material
        foreach (SkinnedMeshRenderer renderer in SmeshRenderers)
        {
            //turn off the emission property
            renderer.material.DisableKeyword("_EMISSION");
        }
    }
}
