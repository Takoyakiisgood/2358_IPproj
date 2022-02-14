
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;

public class MixBowl : MonoBehaviour
{
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

    private void Start()
    {
        //set the bowl empty first
        if (contentArray != null)
        {
            for (int i = 0; i < contentArray.Length; i++)
            {
                contentArray[i].SetActive(false);
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name + " is collided");
        //Check if the content needed is the correct item,
        //if yes, destroy the collider object && set it to appear on the mixing bowl
        if (other.gameObject.tag == "Water" && GameManager.instance.isSetupComplete())
        {
            Destroy(other.gameObject);
            contentArray[0].SetActive(true);
            waterInside = true;
        }
        else if (other.gameObject.tag == "Flour" && GameManager.instance.isSetupComplete())
        {
            Destroy(other.gameObject);
            contentArray[1].SetActive(true);
            flourInside = true;
        }

    }

    private void Update()
    {
        if (waterInside && flourInside)
        {
            //set mixture inside is true
            mixtureInside = true;
        }

        IncreaseStir();
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
                    Destroy(contentArray[0]); //Water
                    Destroy(contentArray[1]); //Flour
                    contentArray[2].SetActive(true);

                    //detach parent of dough from mixbowl to tabletools
                    contentArray[2].transform.parent = tableObject.transform;
                }

                //remove the following components from the mix bowl, to allow the player to pick up the dough
                this.GetComponent<BoxCollider>().enabled = false;
                this.GetComponent<NearInteractionGrabbable>().enabled = false;
                this.GetComponent<ObjectManipulator>().enabled = false;
            }
        }
    }
}
