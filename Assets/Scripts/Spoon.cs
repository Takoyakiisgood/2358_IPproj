using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spoon : MonoBehaviour
{
    public static Spoon instance;
    public Transform spawnPosition;
    public GameObject prefab;
    public GameObject plate;
    public GameObject[] plateContent;
    public int sesameCount;
    public float timer;

    [SerializeField]
    private GameObject tableToolsParent;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = 0;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Paste")
        {
            if(GameManager.instance.hasAddButter == true)
            {
                if (sesameCount < 4)
                {
                    if (timer == 0)
                    {
                        timer = 1.5f;
                        GameObject mySpoon = Instantiate(prefab, spawnPosition.position, Quaternion.identity);
                        GameManager.instance.scoopPaste();
                        mySpoon.transform.parent = transform;
                        sesameCount++;
                    }

                }
                else if (sesameCount == 3)
                {
                    other.gameObject.SetActive(false);
                    GameManager.instance.scoopPaste();
                }
            }
            
            
        }

        else if(other.gameObject.tag == "Plate")
        {
            Debug.Log("collide with plate");
            foreach (Transform obj in gameObject.transform)
            {
                Debug.Log(obj.name);
                if (obj.tag == "RoundDough")
                {
                    if(sesameCount < 5)
                    {
                        
                        Destroy(obj.gameObject);
                        plateContent[sesameCount-1].SetActive(true);
                       

                        Debug.Log(sesameCount);
                    }
                    if (sesameCount == 4)
                    {
                        Debug.Log("works");
                        for(int i = 0; i < plateContent.Length; i++)
                        {
                            plateContent[i].transform.parent = tableToolsParent.transform;
                        }
                        GameManager.instance.PlacePasteOnPlate();
                        plate.GetComponent<BoxCollider>().enabled = false;
                        plate.GetComponent<NearInteractionGrabbable>().enabled = false;
                        plate.GetComponent<ObjectManipulator>().enabled = false;
                    }
                    
                }
            }
        }
    }

    public void Reset()
    {
        Destroy(GameObject.Find("SesameBall(Clone)"));
        sesameCount = 0;
        for(int i = 0; i<plateContent.Length; i++)
        {
            plateContent[i].SetActive(false);
        }
    }
}
