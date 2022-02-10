using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixBowl : MonoBehaviour
{
    [Header("To be Assigned")]
    public GameObject[] contentArray;
    public string[] contentName;
    //reference the stiring script
    public Stiring Stiring;

    [Header("Check ingredient")]
    [SerializeField]
    private bool waterInside;
    [SerializeField]
    private bool flourInside;

    private void Start()
    {
        //set the bowl empty first
        for (int i = 0; i < contentArray.Length; i++)
        {
            contentArray[i].SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name + " is collided");
        //Check if the content needed is the correct item,
        //if yes, destroy the collider object && set it to appear on the mixing bowl
        if (other.gameObject.name == contentName[0])
        {
            Destroy(other.gameObject);
            contentArray[0].SetActive(true);
            waterInside = true;
        }
        else if (other.gameObject.name == contentName[1])
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
            Stiring.SetMixture();
        }
    }
}
