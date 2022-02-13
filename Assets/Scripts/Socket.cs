using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket : MonoBehaviour
{   
    public Transform target;
    public string TargetName;
    private bool targetInside;
    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.gameObject.name + " is collided");
        if (other.gameObject.tag == TargetName  && !targetInside)
        {
            //set the object position to the target position
            other.gameObject.GetComponent<Rigidbody>().MovePosition(target.position);
            other.gameObject.transform.rotation = target.rotation;
            targetInside = true;
        }

        if (targetInside)
        { 
            //clear a task
        }
    }
    private void Start()
    {
        
    }
}
