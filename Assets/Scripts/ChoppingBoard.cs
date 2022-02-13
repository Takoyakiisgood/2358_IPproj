using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoppingBoard : MonoBehaviour
{
    [Header("To be Assigned")]
    [SerializeField]
    private GameObject ChangeDough;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Dough")
        {
            if (ChangeDough != null)
            {
                //destroy the current dough and change it to the assigned dough
                Destroy(collision.gameObject);
                ChangeDough.SetActive(true);
            }
            else
            {
                Debug.Log("Change Dough game object is not assigned!");
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (ChangeDough != null)
        {
            //set the dough to be inactive at first
            ChangeDough.SetActive(false);
        }
        else
        {
            Debug.Log("Change Dough game object is not assigned!");
        }
    }

}
