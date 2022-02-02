using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private int grindCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("works");
        Debug.Log(collision.relativeVelocity.magnitude);
        if(collision.gameObject.tag == "Mortal")
        {
            if (collision.relativeVelocity.magnitude > 2 && grindCount < 4)
            {
                grindCount++;
                Debug.Log("test" + grindCount);
            }
        }
        
    }
}
