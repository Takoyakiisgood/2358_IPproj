using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mortal : MonoBehaviour
{
    public static Mortal instance;

    private int contentCount;
    public GameObject[] contentArray;
    public string[] contentName;
    public bool contentChecked;
    public bool hasChecked;
    [SerializeField]
    private int gingerCount;
    private int tangYuanCount;
    private float timer;
    public Material pasteMaterial;
    private Color originalColor;
    private float originalFloat;
    private int sugarCount;
    private void Start()
    {
        instance = this;
        if (gameObject.name == "mortalcontents")
        {
            pasteMaterial.SetFloat("_Smoothness", 0f);
        }

    }
    private void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.instance.isSetupComplete())
        {
            if (other.gameObject.name == contentName[0] && GameManager.instance.currentTask == "prepFilling")
            {
                if (hasChecked == false)
                {
                    contentArray[0].SetActive(true);
                    contentCount++;
                    hasChecked = true;
                    other.gameObject.SetActive(false);

                }
            }
            else if (other.gameObject.name == contentName[1] && GameManager.instance.currentTask == "prepFilling")
            {

                contentArray[1].SetActive(true);
                contentCount++;
                other.gameObject.SetActive(false);

            }
            else if (other.gameObject.name == contentName[2] && GameManager.instance.currentTask == "prepFilling")
            {
                other.gameObject.SetActive(false);
                pasteMaterial.SetFloat("_Smoothness", 0.93f);
                GameManager.instance.mixPasteWithButter();
            }

            if (contentCount == 2 && GameManager.instance.currentTask == "prepFilling")
            {
                if (!contentChecked)
                {
                    for (int i = 0; i < contentArray.Length - 1; i++)
                    {
                        contentArray[i].SetActive(false);
                    }
                    contentArray[contentCount].SetActive(true);
                    contentChecked = true;
                }

            }
        }
    }


    public void ResetStep1()
    {
        contentCount = 0;

        for (int i = 0; i < contentArray.Length; i++)
        {
            contentArray[i].SetActive(false);

        }
        hasChecked = false;
        contentChecked = false;
    }


}
