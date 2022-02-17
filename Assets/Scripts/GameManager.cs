using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/******************************************************************************
Author: Ng Hui Ling, Jordan Yeo Xiang Yu

Name of Class: GameManager

Description of Class: This class control and check the overall game flow such as checking which task have been completed.

Date Created: 11/02/2022
******************************************************************************/

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("Check Task Complete")]
    public string currentTask;

    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    private bool setUpComplete;
    [SerializeField]
    private bool reseted;

    public bool hasAddButter;
    [SerializeField]
    //Step 1.1, 1.2, 1.3, 1.4
    private bool grindProcessComplete, mixingProcessComplete, scoopPasteComplete, pasteOnPlateComplete;
    [SerializeField]
    //Step 2.1, 2.2, 2.3, 2.4, 2.5
    private bool addMixtureComplete, stirMixtureComplete, placeDoughComplete, cutDoughComplete, flattenDoughComplete;

    //Step 3
    //No Sub task
    [SerializeField]
    //Step 4.1, 4.2, 4.3
    private bool chopGingerComplete, waterBoiledComplete, addIngredientsComplete;
    [SerializeField]
    //Step 5.1, 5.2
    private bool boilTangYuanComplete, scoopTangYuanComplete;
    [SerializeField]
    //Main Task
    private bool prepFilling, prepDough, prepTangYuan, prepSoup, cookTangYuan;

    [SerializeField]
    private int flatCount = 0;
    private int scoopCount = 0;
    private int tangYuanCount = 0;
    private int finalIngredients = 0;
    private int scoopTangYuanCount = 0;


    public List<GameObject> refFlatDough;
    public List<GameObject> refTangYuan;
    public List<Vector3> refTangYuanPos;
    private void Awake()
    {
        instance = this;
    }

    public void SetupComplete()
    {
        if (!setUpComplete)
        {
            setUpComplete = true;            
        }
    }

    public void SetCurrentTask()
    {
        if (setUpComplete)
        {
            if (!prepFilling && !prepDough && !prepTangYuan && !prepSoup && !cookTangYuan)
            {
                currentTask = "prepFilling";
            }
            else if (prepFilling && !prepDough && !prepTangYuan && !prepSoup && !cookTangYuan)
            {
                currentTask = "prepDough";
            }
            else if (prepFilling && prepDough && !prepTangYuan && !prepSoup && !cookTangYuan)
            {
                currentTask = "prepTangYuan";
            }
            else if (prepFilling && prepDough && prepTangYuan && !prepSoup && !cookTangYuan)
            {
                currentTask = "prepSoup";
            }
            else if (prepFilling && prepDough && prepTangYuan && prepSoup && !cookTangYuan)
            {
                currentTask = "cookTangYuan";
            }
            else
            {
                currentTask = "completed";
            }
        }
    }

    public bool isSetupComplete()
    {
        return setUpComplete;
    }

    public bool isReseted()
    {
        return reseted;
    }
    
    public void ToggleReset()
    {
        if (reseted == false)
        {
            reseted = true; //true
        }
        else
        {
            reseted = false;
        }
    }

    public void flattenDough() 
    {
        //check if flattenDough is Completed
        if (!flattenDoughComplete)
        {
            ++flatCount;
            if (flatCount == 4)
            {
                flattenDoughComplete = true;

                if(flattenDoughComplete == true && cutDoughComplete == true && placeDoughComplete == true && stirMixtureComplete == true && addMixtureComplete == true)
                {
                    //mark task as done
                    prepDough = true;
                }
            }
        }
        
        

    }
    public void cutDough()
    {
        cutDoughComplete = true;
    }
    public void grindProcess()
    {
        grindProcessComplete = true;
    }

    public void mixPasteWithButter()
    {
        mixingProcessComplete = true;
        hasAddButter = mixingProcessComplete;
    }

    public void scoopPaste()
    {
        //check if flattenDough is Completed
        if (!scoopPasteComplete)
        {
            
            ++scoopCount;
            if (scoopCount == 4)
            {
                scoopPasteComplete = true;
            }
        }
        else
        {
            //mark task as done        
            
        }
    }
    public void scoopTangYuan()
    {
        //check if flattenDough is Completed
        if (!scoopTangYuanComplete)
        {

            ++scoopTangYuanCount;
            if (scoopTangYuanCount == 4)
            {
                scoopTangYuanComplete = true;
                if(boilTangYuanComplete == true && scoopTangYuanComplete == true)
                {
                    //mark task as done        
                    cookTangYuan = true;
                }
            }
        }
    }

    public void BoilTangYuan()
    {
        boilTangYuanComplete = true;
    }

    public void BoilWater()
    {
        waterBoiledComplete = true;
    }

    public void AddIngredients()
    {
        finalIngredients++;
        if(finalIngredients == 3)
        {
            addIngredientsComplete = true;
        }
        if (addIngredientsComplete == true && chopGingerComplete == true && waterBoiledComplete == true)
        {
            prepSoup = true;
        }

        
    }

    public void PlacePasteOnPlate()
    {
        pasteOnPlateComplete = true;

        if (grindProcessComplete == true && mixingProcessComplete == true && scoopPasteComplete == true && pasteOnPlateComplete == true)
        {
            prepFilling = true;
        }
        
    }

    public void AddHotWater()
    {
        addMixtureComplete = true;
    }
    
    public void StirMixture()
    {
        stirMixtureComplete = true;
    }

    public void PlaceDoughOnChoppingBoard()
    {
        placeDoughComplete = true;
    }

    public void DivideDough()
    {
        cutDoughComplete = true;
    }

    public void AssembleTangYuan()
    {
        tangYuanCount++;
        if(tangYuanCount == 4)
        {
            prepTangYuan = true;
        }
    }

    public void ChopGinger()
    {
        chopGingerComplete = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void Reset()
    {
        if (currentTask == "prepFilling")
        {
            prepFilling = false;

            grindProcessComplete = false;

            mixingProcessComplete = false;

            scoopPasteComplete = false;

            pasteOnPlateComplete = false;

            hasAddButter = false;
            GrindSequence.instance.Reset();
            CheckContents.instance.ResetStep1();
            Spoon.instance.Reset();
            GetComponent<ModelLibrary>().ResetTransforms();
            ToggleReset();
        }
        else if (currentTask == "prepDough")
        {
            prepDough = false;

            addMixtureComplete = false;

            stirMixtureComplete = false;

            placeDoughComplete = false;

            cutDoughComplete = false;

            flattenDoughComplete = false;

            MixBowl.instance.Reset();
            ChoppingBoard.instance.Reset();
            GameObject[] obj = GameObject.FindGameObjectsWithTag("FlattenDough");
            foreach (GameObject x in obj)
            {
                Destroy(x);
            }
            Knife.instance.Reset();
            GetComponent<ModelLibrary>().ResetTransforms();
            ToggleReset();

        }
        else if (currentTask == "prepTangYuan")
        {
            prepTangYuan = false;
            foreach(GameObject x in refFlatDough)
            {
                x.SetActive(true);
            }
            
            GameObject[] obj2 = GameObject.FindGameObjectsWithTag("FinalIngredients");
            foreach (GameObject x in obj2)
            {
                if(x.name == "TangYuan(Clone)")
                {
                    Destroy(x);
                }
                
            }

        }
        else if (currentTask == "prepSoup")
        {
            prepSoup = false;

            chopGingerComplete = false;

            waterBoiledComplete = false;

            addIngredientsComplete = false;

            CheckContents.instance.ResetStep4();
            Knife.instance.ResetStep4();
            Boiling.instance.StartFire();
        }
        else if (currentTask == "cookTangYuan")
        {
            cookTangYuan = false;
            
            boilTangYuanComplete = false;

            scoopTangYuanComplete = false;

            for(int i = 0; i < refTangYuan.Count; i++)
            {
                refTangYuan[i].SetActive(true);
                refTangYuan[i].transform.position = refTangYuanPos[i];
            }
            Laddle.instance.Reset();
        }
         
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Check current task
        SetCurrentTask();
        //reset the subtask bool values
        if (isReseted())
        {
            Reset();
            
        }
    }
}
