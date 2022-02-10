using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stiring : MonoBehaviour
{
    [Header("Check Stir")]
    [SerializeField]
    private int Count;
    private bool point1;
    private bool point2;
    [SerializeField]
    private bool stirDone;
    private bool mixtureInside;

    [Header("To be Assigned")]
    public GameObject flourDough;
    // Start is called before the first frame update
    void Start()
    {
        if (flourDough != null)
        {
            flourDough.SetActive(false);
        }

    }

    public void SetPoint1()
    {
        if (!stirDone)
        {
            point1 = true;
        }
        
    }

    public void SetMixture()
    {
        mixtureInside = true;
    }

    public void SetPoint2()
    {
        //check if spoon touches point 1 first before point 2 to count 1 stir
        if (point1 && !point2 && !stirDone && mixtureInside)
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

    public void IncreaseStir()
    {
        //Check if Point 1 and 2 is true 
        if(point1 && point2 && !stirDone && mixtureInside)
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
                if (flourDough != null)
                {
                    flourDough.SetActive(true);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        IncreaseStir();
    }
}
