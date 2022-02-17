using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

/******************************************************************************
Author: Ng Hui Ling

Name of Class: TextSettings

Description of Class: This class gets and set the font settings.

Date Created: 14/02/2022
******************************************************************************/

public class TextSettings : MonoBehaviour
{
    /*
     * Editor Exposed Variables
     */
    [Header("Configuration")]
    [Tooltip("The reference of the Text")]
    [SerializeField]
    private TextMeshProUGUI[] txtObj;
    [SerializeField]
    private GameObject fontSizeObj, TrackingObj;

    [Header("Text Size")]
    [SerializeField]
    private int maxTextSize = 18;
    [SerializeField]
    private int minTextSize = 12;

    [Header("Tracking")]
    //[SerializeField]
    private int maxTrack;
    //[SerializeField]
    private int minTrack = 0;

    [Header("Current Size")]
    [SerializeField]
    private int currentTxtSize = 16;
    [SerializeField]
    private int currentTrackSize = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Set all the paragraph text to default settings
        if (txtObj != null)
        {
            //default size of the text is 16 and tracking 0
            for (int i = 0; i < txtObj.Length; i++)
            {
                txtObj[i].fontSize = currentTxtSize;
                txtObj[i].fontSizeMin = minTextSize;
                txtObj[i].fontSizeMax = maxTextSize;
                txtObj[i].characterSpacing = currentTrackSize;
            }
        }

        //Set the current size of the text on the settings label
        if (fontSizeObj != null && TrackingObj != null)
        {
            fontSizeObj.GetComponent<TMP_Text>().text = currentTxtSize.ToString();
            TrackingObj.GetComponent<TMP_Text>().text = currentTrackSize.ToString();
        }
    }

    public void IncreaseTxtSize()
    {
        //reset back the tracking so it does not go beyond the maximum
        currentTrackSize = 0;
        //increase the current size
        currentTxtSize++;
        //make sure the size does not go beyond the max size
        if (currentTxtSize >= maxTextSize)
        {
            currentTxtSize = maxTextSize;
        }

        //Assign back the number to the label
        fontSizeObj.GetComponent<TMP_Text>().text = currentTxtSize.ToString();
        //Set the value to back to all the reference text
        SetSize(currentTxtSize);
    }

    public void DecreaseTxtSize()
    {
        //decrease the current size
        currentTxtSize--;
        //make sure the size does not go beyond the min size
        if (currentTxtSize <= minTextSize)
        {
            currentTxtSize = minTextSize;
        }

        //Assign back the number to the label
        fontSizeObj.GetComponent<TMP_Text>().text = currentTxtSize.ToString();
        //Set the value to back to all the reference text
        SetSize(currentTxtSize);
    }

    public void IncreaseTracking()
    {
        if (currentTxtSize == 9)
        {
            maxTrack = 18;
        }
        else if (currentTxtSize == 10)
        {
            maxTrack = 10;
        }
        else if (currentTxtSize == 11)
        {
            maxTrack = 6;
        }

        //increase the current track
        currentTrackSize++;
        //make sure the size does not go beyond the max size
        if (currentTrackSize >= maxTrack)
        {
            currentTrackSize = maxTrack;
        }

        //Assign back the number to the label
        TrackingObj.GetComponent<TMP_Text>().text = currentTrackSize.ToString();
        //Set the value to back to all the reference text
        SetTrack(currentTrackSize);

    }

    public void DecreaseTracking()
    {
        //decrease the current track
        currentTrackSize--;
        //make sure the size does not go beyond the min size
        if (currentTrackSize <= minTrack)
        {
            currentTrackSize = minTrack;
        }

        //Assign back the number to the label
        TrackingObj.GetComponent<TMP_Text>().text = currentTrackSize.ToString();
        //Set the value to back to all the reference text
        SetTrack(currentTrackSize);
    }

    private void SetSize(int size)
    {
        //Set all the paragraph text to default settings
        if (txtObj != null)
        {
            //default size of the text is 16 and tracking 0
            for (int i = 0; i < txtObj.Length; i++)
            {
                txtObj[i].fontSize = size;
            }
        }
    }

    private void SetTrack(int track)
    {
        //Set all the paragraph text to default settings
        if (txtObj != null)
        {
            //default size of the text is 16 and tracking 0
            for (int i = 0; i < txtObj.Length; i++)
            {
                txtObj[i].characterSpacing = track;
            }
        }
    }

    private void Update()
    {
        //Assign back the number to the label
        TrackingObj.GetComponent<TMP_Text>().text = currentTrackSize.ToString();
        //Assign back the number to the label
        fontSizeObj.GetComponent<TMP_Text>().text = currentTxtSize.ToString();
    }
}
