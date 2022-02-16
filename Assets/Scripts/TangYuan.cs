using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TangYuan : MonoBehaviour
{
    public GameObject prefab;

    private void Start()
    {
        //set the object to be hidden at first
        this.gameObject.SetActive(false);
    }

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

    private void Update()
    {
        if (GameManager.instance.isRested())
        {
            if (GameManager.instance.currentTask == "cookTangYuan")
            {
                //things to be reseted
                this.gameObject.SetActive(false);

                //set the reset back to false
                GameManager.instance.ToggleReset();
            }
        }
    }
}
