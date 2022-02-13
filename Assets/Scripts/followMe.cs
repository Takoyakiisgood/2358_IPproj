using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;
[RequireComponent(typeof(RadialView))]
public class FollowMe : MonoBehaviour
{
    private bool RadialViewActive = false;
    private void Start()
    {
        RadialViewActive = this.GetComponent<RadialView>().isActiveAndEnabled;
    }
    public void followToggle() {
        if (!RadialViewActive)
        {
            this.GetComponent<RadialView>().enabled = true;
            RadialViewActive = true;
        }
        else
        {
            this.GetComponent<RadialView>().enabled = false;
            RadialViewActive = false;
        }
    }
}
