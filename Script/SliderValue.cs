using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValue : MonoBehaviour
{

    public Text text;

    public void updateText(float value) {
        text.text = value.ToString();
    }
    
}
