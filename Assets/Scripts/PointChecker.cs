﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointChecker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name + " is collided");
        //Check if the collided Object inside is a Spoon
        if (other.gameObject.name == "Spoon")
        {          
            if (this.name == "Point1")
            {
                this.GetComponentInParent<MixBowl>().SetPoint1();
                //this.GetComponent<MixBowl>().SetPoint1();
            }
            else if (this.name == "Point2")
            {
                this.GetComponentInParent<MixBowl>().SetPoint2();
            }
        }
    }

}
