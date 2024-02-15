using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PadLockedTempController : MonoBehaviour, Interactable

{
    //I fully expect this file to get reworked
    //It is a very clunky way to get the actual functionality of the padlock to work though it ignores everything evolving the current movement system
    //I don't like how its done at all tbh

    //This is currently intended to be for every single individual padlock chain (so if there are four numbers each number will have this script)
    
    float currentThousandNum = 0;
    float currentHundredNum = 0;
    float currentTenNum = 0;
    float currentOneNum = 0;
    [SerializeField] TextMeshProUGUI[] texts;
    [SerializeField] Button[] dails;
    
    private Transform[] pieces;
    int active = 0;
    float currentDial;
    private PlayerControls playerControls;

  
    GenerateCodeScript answerGener;
    

    private void Awake()
    {
        dails[active].image.color = Color.red;
        playerControls ??= new();
        
    }

    void Start()
    {
        //self explanitory just sets the padlock messages to the defualt
        for(int i = 0; i < texts.Length; i++)
        {
            texts[i].text = "0";

        }
        currentDial = 1;
     
        playerControls.CombinationLock.ChangeNumberUp.performed += PressUp;
        playerControls.CombinationLock.ChangeNumberDown.performed += PressDown;
        playerControls.CombinationLock.SwitchDialRight.performed += GoRight;
        playerControls.CombinationLock.SwitchDialLeflt.performed += GoLeft;
        playerControls.CombinationLock.CheckAnswer.performed += CheckAnswer;
        playerControls.CombinationLock.Exit.performed += Exit;
        Deactivate();
        
    }   

    private void GoRight(InputAction.CallbackContext context)
    {
        

        dails[active].image.color = Color.white;
        active += 1;
        if(active > 4)
        {
            active = 0;
        }
        dails[active].image.color = Color.red;
      
    }
    private void GoLeft(InputAction.CallbackContext context)
    {
       
        dails[active].image.color = Color.white;
        active -= 1;
        if (active < 0)
        {
            active = 4;
        }
        dails[active].image.color = Color.red;  
        
    }

    public void PressUp(InputAction.CallbackContext context)
    {
        //If you press the up button (currently beind done with a mouse)
        //The number will go up and the text is updated to match that
        
        if(active == 0)
        {
            currentThousandNum++;
            //since padlocks only go from 0-9 i have it set where if the number goes to 10 or above it resets to 0
            if (currentThousandNum >= 10)
            {
                currentThousandNum = 0;

            }
            texts[active].text = currentThousandNum.ToString();
        }
        else if(active == 1)
        {
            currentHundredNum++;
            //since padlocks only go from 0-9 i have it set where if the number goes to 10 or above it resets to 0
            if (currentHundredNum >= 10)
            {
                currentHundredNum = 0;

            }
            texts[active].text = currentHundredNum.ToString();
        }
        else if (active == 2)
        {
            currentTenNum++;
            //since padlocks only go from 0-9 i have it set where if the number goes to 10 or above it resets to 0
            if (currentTenNum >= 10)
            {
                currentTenNum = 0;

            }
            texts[active].text = currentTenNum.ToString();
        }
        else if (active == 3)
        {
            currentOneNum++;
            //since padlocks only go from 0-9 i have it set where if the number goes to 10 or above it resets to 0
            if (currentOneNum >= 10)
            {
                currentOneNum = 0;

            }
            texts[active].text = currentOneNum.ToString();
        }





    }

    public void PressDown(InputAction.CallbackContext context)
    {
        //Same idea as PressUp() but in reverse
        if (active == 0)
        {
            currentThousandNum--;
            //since padlocks only go from 0-9 i have it set where if the number goes to 10 or above it resets to 0
            if (currentThousandNum < 0)
            {
                currentThousandNum = 9;

            }
            texts[active].text = currentThousandNum.ToString();
        }
        else if (active == 1)
        {
            currentHundredNum--;
            //since padlocks only go from 0-9 i have it set where if the number goes to 10 or above it resets to 0
            if (currentHundredNum < 0)
            {
                currentHundredNum = 9;

            }
            texts[active].text = currentHundredNum.ToString();
        }
        else if (active == 2)
        {
            currentTenNum--;
            //since padlocks only go from 0-9 i have it set where if the number goes to 10 or above it resets to 0
            if (currentTenNum < 0)
            {
                currentTenNum = 9;

            }
            texts[active].text = currentTenNum.ToString();
        }
        else if (active == 3)
        {
            currentOneNum--;
            //since padlocks only go from 0-9 i have it set where if the number goes to 10 or above it resets to 0
            if (currentOneNum < 0)
            {
                currentOneNum = 9;

            }
            texts[active].text = currentOneNum.ToString();
        }

    }

    public float GetNum()
    {
        return currentThousandNum * 1000 + currentHundredNum * 100 + currentTenNum * 10 + currentOneNum;
    }

    public void Activate()
    {
        playerControls.CombinationLock.Enable();
        FindAnyObjectByType<PlayerController>().DeactivateControls();
        foreach(var dail in dails)
        {
            dail.gameObject.SetActive(true);
        }


       
    }
    public void Exit(InputAction.CallbackContext context)
    {
        Deactivate();
    }

    public void CheckAnswer(InputAction.CallbackContext context)
    {
        if(active == 4)
        {
            CheckIfCorrect();
        }
    }

    private void Deactivate()
    {
        playerControls.CombinationLock.Disable();
        FindAnyObjectByType<PlayerController>().ActivateControls();

       foreach(var dail in dails)
        {
            dail.gameObject.SetActive(false);
        }

    }
   
    private void CheckIfCorrect()
    {
        float currentAnswer = GetNum();

        float correctAnwer;
        correctAnwer = GenerateCodeScript.code;
        //very simple check to see if the generated answer equals the displayed answer filler result
        if (currentAnswer == correctAnwer)
        {
            Debug.Log("Yippie!");
           
        }
        else
        {
            Debug.Log("Womp womp");
           
        }
    }

    public void OnInteract()
    {
        if (answerGener.CheckedAnswer())
        {
            //This check ensures Ash checks the location where the answer is told to her before she can input anything.
            Activate();
        }
        else
        {
            //This part would be what happens when Ash says that she needs to check the whiteboard or whatever it is she needs to look at.
            Debug.Log("Ash needs to check the code first >:(");
        }

    }
}
