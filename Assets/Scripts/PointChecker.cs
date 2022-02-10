using System.Collections;
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
                other.gameObject.GetComponent<Stiring>().SetPoint1();
            }
            else if (this.name == "Point2")
            {
                other.gameObject.GetComponent<Stiring>().SetPoint2();
            }
        }
    }

}
