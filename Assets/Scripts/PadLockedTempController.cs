using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PadLockedTempController : MonoBehaviour
{
    //I fully expect this file to get reworked
    //It is a very clunky way to get the actual functionality of the padlock to work though it ignores everything evolving the current movement system
    //I don't like how its done at all tbh

    //This is currently intended to be for every single individual padlock chain (so if there are four numbers each number will have this script)
    
    float currentNum = 0;
    [SerializeField] TextMeshProUGUI text;

    void Start()
    {
        //self explanitory just sets the padlock messages to the defualt 
        text.text = currentNum.ToString();
    }   

    public void PressUp()
    {
        //If you press the up button (currently beind done with a mouse)
        //The number will go up and the text is updated to match that
        currentNum++;
        //since padlocks only go from 0-9 i have it set where if the number goes to 10 or above it resets to 0
        if(currentNum >= 10)
        {
            currentNum= 0;
            
        }
        text.text = currentNum.ToString();

    }

    public void PressDown()
    {
        //Same idea as PressUp() but in reverse
        currentNum--;
        //If the currentNum goes below 0 I have it wrap around into a 9 instead just like before
        if (currentNum < 0)
        {
            currentNum = 9;
        }
        text.text = currentNum.ToString();
    }

    public float GetNum()
    {
        return currentNum;
    }
}
