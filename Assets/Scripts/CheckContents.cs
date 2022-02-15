﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckContents : MonoBehaviour
{
    private int contentCount;
    public GameObject[] contentArray;
    public GameObject[] finalContent;
    public string[] contentName;
    private bool contentChecked;
    public bool hasChecked;
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
        if (other.gameObject.name == contentName[0] && GameManager.instance.isSetupComplete() && gameObject.tag != "Pot")
        {
            if (hasChecked == false)
            {
                contentArray[0].SetActive(true);
                contentCount++;
                hasChecked = true;
                GameManager.instance.addSugarCount();
            }
            if (GameManager.instance.sugarCount == 2)
            {
                Destroy(other.gameObject);
            }
        }
        else if (other.gameObject.name == contentName[1] && GameManager.instance.isSetupComplete() && gameObject.tag != "Pot")
        {
        
            Destroy(other.gameObject);
            contentArray[1].SetActive(true);
            contentCount++;

        
        }
        else if (other.gameObject.name == contentName[2] && GameManager.instance.isSetupComplete() && gameObject.tag != "Pot")
        {
            Destroy(other.gameObject);
            pasteMaterial.SetFloat("_Smoothness", 0.93f);
            GameManager.instance.mixPasteWithButter();
        }
        
        if (contentCount == 2 && GameManager.instance.isSetupComplete())
        {
            if (!contentChecked)
            {
                for (int i = 0; i < contentArray.Length -1; i++)
                {
                    contentArray[i].SetActive(false);
                }
                contentArray[contentCount].SetActive(true);
                GameManager.instance.mixPasteWithButter();
                contentChecked = true;
            }

        }
        else if ((other.gameObject.tag == "BrownSugar" || other.gameObject.tag == "FinalIngredients") && GameManager.instance.isSetupComplete())
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
                        GameManager.instance.addSugarCount();
                    }
                    
                }
                if (GameManager.instance.sugarCount == 2)
                {
                    Destroy(other.gameObject);
                }

            }
            else if (other.gameObject.name == contentName[1])
            {
                Destroy(other.gameObject);
                finalContent[1].SetActive(true);
            }
            else if (other.gameObject.name == contentName[2] && GameManager.instance.isSetupComplete())
            {
                Destroy(other.gameObject);
                gingerCount++;
                if (gingerCount == 1)
                {
                    finalContent[2].SetActive(true);
                }
                else if (gingerCount == 2)
                {
                    finalContent[3].SetActive(true);
                }

            }
            else if (other.gameObject.name == contentName[3] && GameManager.instance.isSetupComplete() && other.gameObject.tag != "Laddle")
            {
                finalContent[tangYuanCount + 4].SetActive(true);
                Destroy(other.gameObject);
                tangYuanCount++;
                if (tangYuanCount == 4)
                {
                    Debug.Log("total 4 tangyuan added");
                    StartCoroutine(BoilTangYuan(20));
                }
            }
        }
        
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
