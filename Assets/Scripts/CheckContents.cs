using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckContents : MonoBehaviour
{
    public static CheckContents instance;

    private int contentCount;
    public GameObject[] contentArray;
    public GameObject[] finalContent;
    public string[] contentName;
    private bool contentChecked;
    public bool hasChecked;
    [SerializeField]
    private int gingerCount;
    private int tangYuanCount;
    private float timer;
    public Material pasteMaterial;
    public Material waterMaterial;
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
        else if (gameObject.name == "potWithTangyuanWater")
        {
            waterMaterial.color = originalColor;
        }
        
        
    }
    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                finalContent[0].SetActive(false);
                Color color1;
                
                ColorUtility.TryParseHtmlString("#B56500", out color1);
                color1.a = 0.5f;
                waterMaterial.color = color1;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.instance.isSetupComplete())
        {
            if (other.gameObject.name == contentName[0] && GameManager.instance.currentTask == "prepFilling" && gameObject.tag != "Pot")
            {
                if (hasChecked == false)
                {
                    contentArray[0].SetActive(true);
                    contentCount++;
                    hasChecked = true;
                    other.gameObject.SetActive(false);

                }
            }
            else if (other.gameObject.name == contentName[1] && GameManager.instance.currentTask == "prepFilling" && gameObject.tag != "Pot")
            {

                contentArray[1].SetActive(true);
                contentCount++;
                other.gameObject.SetActive(false);

            }
            else if (other.gameObject.name == contentName[2] && GameManager.instance.currentTask == "prepFilling" && gameObject.tag != "Pot")
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
            else if ((other.gameObject.tag == "BrownSugar" || other.gameObject.tag == "FinalIngredients") && GameManager.instance.currentTask == "prepSoup" && gameObject.name != "mortalcontents")
            {
                if (other.gameObject.name == contentName[0])
                {
                    if (gameObject.tag == "Pot")
                    {
                        bool hasChecked;
                        hasChecked = false;
                        if (!hasChecked)
                        {
                            finalContent[0].SetActive(true);
                            timer = 5.0f;
                            hasChecked = true;
                            other.gameObject.SetActive(false);
                            GameManager.instance.AddIngredients();
                        }

                    }

                }
                else if (other.gameObject.name == contentName[1])
                {
                    other.gameObject.SetActive(false);
                    finalContent[1].SetActive(true);
                    GameManager.instance.AddIngredients();
                }
                else if (other.gameObject.name == contentName[2] && GameManager.instance.currentTask == "prepSoup")
                {
                    other.gameObject.SetActive(false);
                    gingerCount++;
                    if (gingerCount == 1)
                    {
                        finalContent[2].SetActive(true);
                    }
                    else if (gingerCount == 2)
                    {
                        gingerCount = 0;
                        finalContent[3].SetActive(true);
                        GameManager.instance.AddIngredients();
                    }

                }

            }

            else if (other.gameObject.name == contentName[3] && GameManager.instance.currentTask == "cookTangYuan" && other.gameObject.tag != "Laddle")
            {
                finalContent[tangYuanCount + 4].SetActive(true);
                other.gameObject.SetActive(false);
                tangYuanCount++;
                if (tangYuanCount == 4)
                {
                    tangYuanCount = 0;
                    Debug.Log("total 4 tangyuan added");
                    StartCoroutine(BoilTangYuan(20));
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
    }

    public void ResetStep4()
    {
        gingerCount = 0;
        waterMaterial.color = originalColor;

    }
    private IEnumerator BoilTangYuan(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            GameManager.instance.BoilTangYuan();
        }
    }
}
