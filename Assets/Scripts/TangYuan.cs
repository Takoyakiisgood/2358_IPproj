using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TangYuan : MonoBehaviour
{
    public GameObject prefab;
    private Transform originalPos;
    private void Start()
    {


        originalPos = this.gameObject.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FlattenDough")
        {
            Debug.Log(other.gameObject.tag);
            GameObject tangYuan = Instantiate(prefab, other.gameObject.transform.position, Quaternion.identity);
            GameManager.instance.refTangYuan.Add(tangYuan);
            GameManager.instance.refTangYuanPos.Add(tangYuan.transform.position);
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
            GameManager.instance.AssembleTangYuan();
        }
    }

    private void Update()
    {
        if (GameManager.instance.isReseted())
        {
            if (GameManager.instance.currentTask == "prepTangYuan")
            {
                //things to be reseted
                gameObject.transform.position = originalPos.transform.position;
                gameObject.transform.rotation = originalPos.transform.rotation;
                //set the reset back to false
                GameManager.instance.ToggleReset();
            }
            else if(GameManager.instance.currentTask == "prepFilling")
            {
                this.gameObject.SetActive(false);
                gameObject.transform.position = originalPos.transform.position;
                gameObject.transform.rotation = originalPos.transform.rotation;
                //set the reset back to false
                GameManager.instance.ToggleReset();
            }
        }
    }
}
