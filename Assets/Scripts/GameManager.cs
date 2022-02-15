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

    private bool setUpComplete;

    [Header("Check Task Complete")]
    [SerializeField]
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
    private bool kneadDoughComplete;
    private bool chopGingerComplete;

    private bool prepFilling, prepDough, prepTangYuan, prepSoup, cookTangYuan;

    [SerializeField]
    private int flatCount = 0;
    private int scoopCount = 0;
    public int sugarCount;
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

    public bool isSetupComplete()
    {
        return setUpComplete;
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
        addIngredientsComplete = true;
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

    public void KneadDough()
    {
        kneadDoughComplete = true;
    }

    public void AssembleTangYuan()
    {
        prepTangYuan = true;
    }

    public void ChopGinger()
    {
        chopGingerComplete = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
