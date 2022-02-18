using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
public class GrindSequence : MonoBehaviour
{
    public static GrindSequence instance;
    private int grindCount;
    public GameObject[] grindArray;
    public GameObject feedbackTip;

    private void Start()
    {
        instance = this;
    }

    private IEnumerator WaitForSecs(float duration)
    {
        yield return new WaitForSeconds(duration);
    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.relativeVelocity.magnitude);
        if(collision.gameObject.tag == "Mortal")
        {
            if (collision.relativeVelocity.magnitude > 0.5 && collision.relativeVelocity.magnitude < 2 && grindCount < 3)
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

            else if (collision.relativeVelocity.magnitude < 0.5)
            {
                Debug.Log("Too little force!");
                feedbackTip.SetActive(true);
                GameManager.instance.makeMistakes();
                feedbackTip.GetComponent<ToolTip>().ToolTipText = "Too little force!";
                WaitForSecs(1.5f);
                feedbackTip.SetActive(false);

            }
            else if (collision.relativeVelocity.magnitude > 2)
            {
                Debug.Log("Too much force!");
                feedbackTip.SetActive(true);
                GameManager.instance.makeMistakes();
                feedbackTip.GetComponent<ToolTip>().ToolTipText = "Too much force!";
                WaitForSecs(1.5f);
                feedbackTip.SetActive(false);
            }
        }
        
    }

    private void Update()
    {

    }
    public void Reset()
    {
        for (int i = 0; i < grindArray.Length; i++) 
        {
            grindArray[i].SetActive(false);

        }
        grindCount = 0;
    }
}
