using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private bool setUpComplete;
    [SerializeField]
    private bool reseted;

    private bool flattenDoughComplete;
    private bool cutDoughComplete;
    private bool grindProcessComplete;
    private bool mixingProcessComplete;
    private bool scoopPasteComplete;
    private bool boilTangYuanComplete;
    private bool grindPasteSeedsComplete;
    private bool pasteOnPlateComplete;
    private bool addHotWaterCompelte;
    private bool waterBoiledComplete;
    private bool addIngredientsComplete;
    private bool stirMixtureComplete;
    private bool placeDoughComplete;
    private bool chopGingerComplete;
    private bool scoopTangYuanComplete;

    private bool prepFilling, prepDough, prepTangYuan, prepSoup, cookTangYuan;

    [SerializeField]
    private int flatCount = 0;
    private int scoopCount = 0;
    public int sugarCount;
    private int tangYuanCount = 0;
    private int finalIngredients = 0;
    private int scoopTangYuanCount = 0;
    public GameObject Decoration;


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

    public bool isRested()
    {
        return reseted;
    }

    public void ToggleReset()
    {
        if (!reseted)
        {
            reseted = !reseted; //true
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
            }
        }
        else 
        {
            //mark task as done
            prepDough = true;
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
            }
        }
        else
        {
            //mark task as done        
            cookTangYuan = true;
        }
    }
    public void addSugarCount()
    {
        sugarCount++;
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
        if (addIngredientsComplete)
        {
            prepSoup = true;
        }       
    }
    public void GrindFilling()
    {
        grindPasteSeedsComplete = true;
    }

    public void PlacePasteOnPlate()
    {
        pasteOnPlateComplete = true;
    }

    public void AddHotWater()
    {
        addHotWaterCompelte = true;
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
        if (isRested())
        {
            Debug.Log("hello");
            ToggleReset();
        }
    }
}
