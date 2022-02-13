using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour
{
    [Header("Configuration")]
    [Tooltip("The color to be change when the highlightColor is called")]
    [SerializeField]
    private Color color;
    private Color originalColor;
    public void highlightColor()
    {
        this.GetComponent<Image>().color = color;
    }

    public void unHighlightColor()
    {
        this.GetComponent<Image>().color = originalColor;
    }

    // Start is called before the first frame update
    void Start()
    {
        originalColor = this.GetComponent<Image>().color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
