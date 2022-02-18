using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.UI;
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
    private int mistakes;
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

    public bool isCompleted;
    public GameObject gameCompleteUI;
    public UiLibrary uiLibrary;
    [SerializeField]
    private int flatCount = 0;
    private int scoopCount = 0;
    private int tangYuanCount = 0;
    private int finalIngredients = 0;
    private int scoopTangYuanCount = 0;

    public List<GameObject> refFlatDough;
    public List<GameObject> refTangYuan;
    public List<Vector3> refTangYuanPos;

    public List<string> step1List;
    public List<string> step2List;
    public List<string> step3List;
    public List<string> step4List;
    public List<string> step5List;
    public TextMeshProUGUI[] taskListGUI;

    public TextMeshProUGUI stepText;


    private void Awake()
    {
        instance = this;
       //step1List.Add("1. Grind cooled sesame seeds and sugar until they turn into a paste texture.");
       //step1List.Add("2. Mix the black sesame paste with butter.");
       //step1List.Add("3. Scoop the paste 4 times with a spoon to form paste balls and place them on an empty plate.");

        //step2List.Add("1. In a mixing bowl, pour water into glutinous rice flour.");
        //step2List.Add("2. Stir the mixture with a spoon 3 times in a clock-wise direction to form a dough.");
        //step2List.Add("3. Place the dough onto the chopping board.");
        //step2List.Add("4. Cut the dough using a knife to divide it into 4 balls.");
        //step2List.Add("5. On the chopping board, use the rolling pin and knead the dough until a smooth, soft flattened dough forms.");

        //step3List.Add("1. Place a ball of filling in the middle of a flattened dough to form the Tang Yuan. Repeat until you have all 4 Tang Yuan.");

        //step4List.Add("1.Cut the ginger into thin slices using a knife. You will need only 2 slices.");
        //step4List.Add("2. Boil a large pot of water on the portable gas stove.");
        //step4List.Add("3. Add brown sugar, pandan leaves and the ginger slices into the water");

        //step5List.Add("1. Put the Tang Yuan into the pot with the sweet soup and boil it for 10 seconds.");
        //step5List.Add("2. Dish out the Tangyuan along with some sweet soup into an empty bowl. Repeat until the empty bowl contains 4 Tang Yuan.");
    }

    public void SetupComplete()
    {
        if (!setUpComplete)
        {
            setUpComplete = true;            
        }
    }

    public void makeMistakes()
    {
        mistakes++;
    }

    public void TasklistReset()
    {
        if (taskListGUI != null)
        {
            for (int i = 0; i < taskListGUI.Length; i++)
            {
                taskListGUI[i].gameObject.SetActive(false);
            }
        }
        
    }

    public void SetCurrentTask()
    {
        if (setUpComplete)
        {
            //set the tasklist to inactive first
            TasklistReset();
            if (!prepFilling && !prepDough && !prepTangYuan && !prepSoup && !cookTangYuan)
            {
                currentTask = "prepFilling";
                stepText.text = "Step 1: " + currentTask;

                for (int i = 0; i < step1List.Count; i++)
                {
                    taskListGUI[i].text = step1List[i];
                    taskListGUI[i].gameObject.SetActive(true);
                }
            }
            else if (prepFilling && !prepDough && !prepTangYuan && !prepSoup && !cookTangYuan)
            {
                currentTask = "prepDough";
                stepText.text = "Step 2: " + currentTask;
                for (int i = 0; i < step2List.Count; i++)
                {
                    taskListGUI[i].text = step2List[i];
                    taskListGUI[i].gameObject.SetActive(true);
                }
            }
            else if (prepFilling && prepDough && !prepTangYuan && !prepSoup && !cookTangYuan)
            {
                currentTask = "prepTangYuan";
                stepText.text = "Step 3: " + currentTask;
                for (int i = 0; i < step3List.Count; i++)
                {
                    taskListGUI[i].text = step3List[i];
                    taskListGUI[i].gameObject.SetActive(true);
                }
            }
            else if (prepFilling && prepDough && prepTangYuan && !prepSoup && !cookTangYuan)
            {
                currentTask = "prepSoup";
                stepText.text = "Step 4: " + currentTask;
                for (int i = 0; i < step4List.Count; i++)
                {
                    taskListGUI[i].text = step4List[i];
                    taskListGUI[i].gameObject.SetActive(true);
                }
            }
            else if (prepFilling && prepDough && prepTangYuan && prepSoup && !cookTangYuan)
            {
                currentTask = "cookTangYuan";
                stepText.text = "Step 5: " + currentTask;
                for (int i = 0; i < step5List.Count; i++)
                {
                    taskListGUI[i].text = step5List[i];
                    taskListGUI[i].gameObject.SetActive(true);
                }
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
                    if (taskListGUI != null)
                    {
                        taskListGUI[4].fontStyle = FontStyles.Strikethrough;
                        resetStyle();
                    }
                }
            }
        }
        
        

    }

    public void resetStyle()
    {
        if (taskListGUI != null)
        {
            for (int i = 0; i < taskListGUI.Length; i++)
            {
                taskListGUI[i].fontStyle = FontStyles.Normal;
            }
            
        }
    }

    public void cutDough()
    {
        cutDoughComplete = true;
        if (taskListGUI != null)
        {
            taskListGUI[3].fontStyle = FontStyles.Strikethrough;
        }
    }
    public void grindProcess()
    {
        grindProcessComplete = true;
        if (taskListGUI != null)
        {
            taskListGUI[0].fontStyle = FontStyles.Strikethrough;
        }

    }

    public void mixPasteWithButter()
    {
        mixingProcessComplete = true;
        hasAddButter = mixingProcessComplete;
        if (taskListGUI != null)
        {
            taskListGUI[1].fontStyle = FontStyles.Strikethrough;
        }
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
                if (taskListGUI != null)
                {
                    taskListGUI[2].fontStyle = FontStyles.Strikethrough;
                }
            }
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
                    if (taskListGUI != null)
                    {
                        taskListGUI[1].fontStyle = FontStyles.Strikethrough;
                        resetStyle();
                        isCompleted = true;
                    }
                }
            }
        }
    }

    public void BoilTangYuan()
    {
        boilTangYuanComplete = true;
        if (taskListGUI != null)
        {
            taskListGUI[0].fontStyle = FontStyles.Strikethrough;
        }

    }

    public void BoilWater()
    {
        waterBoiledComplete = true;
        if (taskListGUI != null)
        {
            taskListGUI[1].fontStyle = FontStyles.Strikethrough;
        }
        if (addIngredientsComplete == true && chopGingerComplete == true && waterBoiledComplete == true)
        {
            prepSoup = true;
            resetStyle();
        }
    }

    public void AddIngredients()
    {
        finalIngredients++;
        if(finalIngredients == 3)
        {
            addIngredientsComplete = true;
            if (taskListGUI != null)
            {
                taskListGUI[0].fontStyle = FontStyles.Strikethrough;
            }
        }
        if (addIngredientsComplete == true && chopGingerComplete == true && waterBoiledComplete == true)
        {
            prepSoup = true;
            resetStyle();
        }

        
    }

    public void PlacePasteOnPlate()
    {
        pasteOnPlateComplete = true;

        if (grindProcessComplete == true && mixingProcessComplete == true && scoopPasteComplete == true && pasteOnPlateComplete == true)
        {
            prepFilling = true;
            if (taskListGUI != null)
            {
                taskListGUI[3].fontStyle = FontStyles.Strikethrough;
                resetStyle();
            }
        }
        
    }

    public void AddHotWater()
    {
        addMixtureComplete = true;
        if (taskListGUI != null)
        {
            taskListGUI[0].fontStyle = FontStyles.Strikethrough;
        }
    }
    
    public void StirMixture()
    {
        stirMixtureComplete = true;
        if (taskListGUI != null)
        {
            taskListGUI[1].fontStyle = FontStyles.Strikethrough;
        }
    }

    public void PlaceDoughOnChoppingBoard()
    {
        placeDoughComplete = true;
        if (taskListGUI != null)
        {
            taskListGUI[2].fontStyle = FontStyles.Strikethrough;
        }
    }

    public void DivideDough()
    {
        cutDoughComplete = true;
        if (taskListGUI != null)
        {
            taskListGUI[3].fontStyle = FontStyles.Strikethrough;
        }
    }

    public void AssembleTangYuan()
    {
        tangYuanCount++;
        if(tangYuanCount == 4)
        {
            prepTangYuan = true;
            if (taskListGUI != null)
            {
                taskListGUI[0].fontStyle = FontStyles.Strikethrough;
                resetStyle();
            }
        }
    }

    public void ChopGinger()
    {
        chopGingerComplete = true;
        if (taskListGUI != null)
        {
            taskListGUI[0].fontStyle = FontStyles.Strikethrough;
        }
        if (addIngredientsComplete == true && chopGingerComplete == true && waterBoiledComplete == true)
        {
            prepSoup = true;
            resetStyle();
        }
    }

    public void QuitGame()
    {
        //set to the database if the set did/did not complete the game
        float timeTaken = TimeManager.Instance.GetCurrentSec();
        PlayerData.instance.SetChineseCulturalStats(timeTaken, mistakes, isCompleted);
        Application.Quit();
    }

    private void Reset()
    {
        //reset the UI pos
        uiLibrary.Reset();

        //reset the strikethrough
        resetStyle();

        if (currentTask == "prepFilling")
        {
            prepFilling = false;

            grindProcessComplete = false;

            mixingProcessComplete = false;

            scoopPasteComplete = false;

            pasteOnPlateComplete = false;

            hasAddButter = false;
            GrindSequence.instance.Reset();
            Mortal.instance.ResetStep1();
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

            ToggleReset();

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
            ToggleReset();
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
            ToggleReset();
        }
         
    }
    private IEnumerator WaitForSecs(float duration, float timeTaken)
    {
        yield return new WaitForSeconds(duration);
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //reset the subtask bool values
        if (isReseted())
        {
            Reset();
            
        }

        if (isCompleted)
        {
            uiLibrary.HideAllPages();
            gameCompleteUI.SetActive(true);
           
            gameCompleteUI.GetComponent<FollowMeToggle>().ToggleFollowMeBehavior();
            float timeTaken = TimeManager.Instance.GetCurrentSec();
            
            PlayerData.instance.SetChineseCulturalStats(timeTaken, mistakes, isCompleted);
            PlayerData.instance.SetPlayerStats(timeTaken, mistakes);


            //reset the whole game
            mistakes = 0;
            isCompleted = false;
            setUpComplete = false;
            this.GetComponent<ModelLibrary>().resetGame();
        }
        else
        {
            //Check current task
            SetCurrentTask();
        }
    }
}
