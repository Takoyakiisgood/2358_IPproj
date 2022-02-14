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
    private TextMeshProUGUI fontSizelb, trackinglb;
  
    [Header("Text Size")]
    [SerializeField]
    private int maxTextSize = 18;
    [SerializeField]
    private int minTextSize = 12;

    [Header("Tracking")]
    [SerializeField]
    private int maxTrack = 30;
    [SerializeField]
    private int minTrack = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (txtObj != null)
        {
            //default size of the text is 16 and tracking 0
            for (int i = 0; i < txtObj.Length; i++)
            {
                txtObj[i].fontSize = 16;
                txtObj[i].fontSizeMin = minTextSize;
                txtObj[i].fontSizeMax = maxTextSize;
                txtObj[i].characterSpacing = 0;
            }
        }
        
    }

    public void IncreaseTxtSize(int step = 1)
    {
        //get the current size
        int size = Int32.Parse(fontSizelb.text);
        size += step;
        //make sure the size does not go beyond the max size
        if (size > maxTextSize)
        {
            size = maxTextSize;
        }

        Debug.Log(size);
        //Assign back the number to the label
        fontSizelb.text = size.ToString();
    }

    public void DecreaseTxtSize(int step = 1)
    {
        //get the current size
        int size = Int32.Parse(fontSizelb.text);
        size -= step;
        //make sure the size does not go beyond the min size
        if (size < minTextSize)
        {
            size = minTextSize;
        }

        Debug.Log(size);
        //Assign back the number to the label
        fontSizelb.text = size.ToString();
    }

    public void IncreaseTracking(int step = 1)
    {
        //get the current size
        int track = Int32.Parse(trackinglb.text);
        track += step;
        //make sure the size does not go beyond the max track
        if (track > maxTrack)
        {
            track = maxTrack;
        }

        Debug.Log(track);
        //Assign back the number to the label
        trackinglb.text = track.ToString();
    }

    public void DecreaseTracking(int step = 1)
    {
        //get the current size
        int track = Int32.Parse(trackinglb.text);
        track += step;
        //make sure the size does not go beyond the min track
        if (track < minTrack)
        {
            track = minTrack;
        }

        Debug.Log(track);
        //Assign back the number to the label
        trackinglb.text = track.ToString();
    }
}
