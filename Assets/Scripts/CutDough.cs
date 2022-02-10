using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutDough : MonoBehaviour
{
    public Animator myanimator;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "knife")
        {
            //play the animation 
            //show the dough into 4 pieces

        }
    }
}
