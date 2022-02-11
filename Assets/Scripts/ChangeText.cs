using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeText : MonoBehaviour
{
    public string Text;
    public TextMeshProUGUI TextObj;

    public void change()
    {
        TextObj.text = Text;
    }
}
