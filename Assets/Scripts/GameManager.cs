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
    [SerializeField]
    private bool flattenDoughComplete;
    [SerializeField]
    private int flatCount = 0;
    private void Awake()
    {
        instance = this;
    }

    public void flattenDough() 
    {
        //check if flattenDough is ompleted
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
