using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrindSequence : MonoBehaviour
{
    private int grindCount;
    public GameObject[] grindArray;


    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.relativeVelocity.magnitude);
        if(collision.gameObject.tag == "Mortal")
        {
            if (collision.relativeVelocity.magnitude > 2 && collision.relativeVelocity.magnitude < 4 && grindCount < 3)
            {
                grindCount++;
                //Debug.Log("GrindCount: " + grindCount);
            }

            if(grindCount != 0)
            {
                grindArray[grindCount - 1].SetActive(false);
            }
            

            if (grindCount == 1)
            {
                grindArray[grindCount].SetActive(true);
            }
            else if(grindCount == 2)
            {
                grindArray[grindCount].SetActive(true);
            }
            else if(grindCount == 3)
            {
                grindArray[grindCount].SetActive(true);
                GameManager.instance.grindProcess();
            }

            else if (collision.relativeVelocity.magnitude < 2)
            {
                Debug.Log("Too much force!");

            }
            else if (collision.relativeVelocity.magnitude > 4)
            {
                Debug.Log("Too little force!");
            }
        }
        
    }
}
