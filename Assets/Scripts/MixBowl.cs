
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;

public class MixBowl : MonoBehaviour
{
    public static MixBowl instance;
    [Header("To be Assigned")]
    public GameObject[] contentArray;

    [Header("Check ingredient")]
    [SerializeField]
    private bool waterInside;
    [SerializeField]
    private bool flourInside;

    [Header("Check Stir")]
    [SerializeField]
    private int Count;
    private bool point1;
    private bool point2;

    [SerializeField]
    private bool stirDone;
    [SerializeField]
    private bool mixtureInside;

    [SerializeField]
    private GameObject tableObject;

    private Transform startPos;

    public GameObject feedbackTip;
    private void Start()
    {
        instance = this;
        //set the bowl empty first
        if (contentArray != null)
        {
            for (int i = 0; i < contentArray.Length; i++)
            {
                contentArray[i].SetActive(false);
            }
        }

        //store the current position of the bowl
        startPos = this.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name + " is collided");
        //Check if the content needed is the correct item,
        //if yes, destroy the collider object && set it to appear on the mixing bowl
        if (other.gameObject.tag == "Water" && GameManager.instance.currentTask == "prepDough")
        {
            other.gameObject.SetActive(false);//waterbowl
            contentArray[0].SetActive(true);//water content inside water
            waterInside = true;

            //Update sub task
            GameManager.instance.AddHotWater();
        }
        else if (other.gameObject.tag == "Flour" && GameManager.instance.currentTask == "prepDough")
        {
            other.gameObject.SetActive(false);
            contentArray[1].SetActive(true);
            flourInside = true;
        }

    }

    public void Reset()
    {
        //reset the check values
        waterInside = false;
        flourInside = false;
        Count = 0;
        point1 = false;
        point2 = false;
        stirDone = false;
        mixtureInside = false;

        //set the bowl empty 
        if (contentArray != null)
        {
            for (int i = 0; i < contentArray.Length; i++)
            {
                contentArray[i].SetActive(false);
            }
        }

        //set the dough back to mixbowl parent
        contentArray[2].transform.parent = this.transform;
    }

    private void Update()
    {
        if (waterInside && flourInside)
        {
            //set mixture inside is true
            mixtureInside = true;
        }

        IncreaseStir();

        if (GameManager.instance.isReseted())
        {
            if (GameManager.instance.currentTask == "prepDough")
            {
                //reset the mix bowl interaction
                Reset();

                //set the reset back to false
                GameManager.instance.ToggleReset();
            }
        }
    }

    private IEnumerator WaitForSecs(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }
    public void SetPoint1()
    {
        //check if the mixture is inside before the user can stir 
        if (mixtureInside)
        {
            if (!stirDone)
            {
                point1 = true;
            }
        }

    }

    public void SetPoint2()
    {
        //check if the mixture is inside before the user can stir      
        if (mixtureInside)
        {
            //check if spoon touches point 1 first before point 2 to count 1 stir
            if (point1 && !point2 && !stirDone)
            {
                point2 = true;

            }
            else
            {
                //display error message
                Debug.Log("Stir it in a clockwise manner");
                //add the mistakes count
                GameManager.instance.makeMistakes();
                feedbackTip.SetActive(true);
                feedbackTip.GetComponent<ToolTip>().ToolTipText = "Stir it in a clockwise manner";
                WaitForSecs(1.5f);
                feedbackTip.SetActive(false);
                //Reset the points
                point1 = false;
                point2 = false;
            }
        }

    }

    public void IncreaseStir()
    {
        //Check if Point 1 and 2 is true 
        if (point1 && point2 && !stirDone && mixtureInside)
        {
            ++Count;
            //Reset the points
            point1 = false;
            point2 = false;
            if (Count == 3)
            {
                //do something when the stir is complete
                stirDone = true;
                //display the flour Dough
                //and set the before mixed ingredients to disappear
                if (contentArray != null) {
                    contentArray[0].SetActive(false);//Water
                    contentArray[1].SetActive(false);//Flour
                    contentArray[2].SetActive(true);//Dough

                    //detach parent of dough from mixbowl to tabletools
                    contentArray[2].transform.parent = tableObject.transform;

                    //Update mixture task
                    GameManager.instance.StirMixture();
                }

                //remove the following components from the mix bowl, to allow the player to pick up the dough
                this.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
}
