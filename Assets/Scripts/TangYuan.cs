using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TangYuan : MonoBehaviour
{
    public GameObject prefab;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "FlattenDough")
        {
            Debug.Log(collision.gameObject.tag);
            Instantiate(prefab, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }




}
