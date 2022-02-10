using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket : MonoBehaviour
{
    public Transform target;
    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.gameObject.name + " is collided");
        if (other.gameObject.name == "kneadDough")
        {
            //set the object position to the target position
            other.gameObject.GetComponent<Rigidbody>().MovePosition(target.position);
            //other.gameObject.transform.position = target.position;
        }
    }
}
