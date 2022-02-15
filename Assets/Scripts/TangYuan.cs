using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TangYuan : MonoBehaviour
{
    public GameObject prefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FlattenDough")
        {
            Debug.Log(other.gameObject.tag);
            Instantiate(prefab, other.gameObject.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(gameObject);
            GameManager.instance.AssembleTangYuan();
        }
    }




}
