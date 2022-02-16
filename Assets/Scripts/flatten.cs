using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flatten : MonoBehaviour
{
    public GameObject prefab;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "RollingPin")
        { 
            //show the dough into 4 pieces
            if (prefab != null)
            {
                GameObject flatDough = Instantiate(prefab, this.transform.position, Quaternion.identity);
                GameManager.instance.refFlatDough.Add(flatDough);
                this.gameObject.SetActive(false);
                //increase the count for flattening the Dough
                GameManager.instance.flattenDough();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }
}
