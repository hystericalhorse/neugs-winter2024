using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PadLockedTempController : MonoBehaviour

{
    float currentThousandNum = 0;
    float nextThousandNum = 1;
    float prevThousandNum = 9;
    float currentHundredNum = 0;
    float nextHundredNum = 1;
    float prevHundredNum = 9;
    float currentTenNum = 0;
    float nextTenNum = 1;
    float prevTenNum = 9;
    float currentOneNum = 0;
    float nextOneNum = 1;
    float prevOneNum = 9;
    [SerializeField] TextMeshProUGUI[] texts;
    [SerializeField] TextMeshProUGUI[] nextTexts;
    [SerializeField] TextMeshProUGUI[] prevTexts;
    [SerializeField] Button[] dails;
    [SerializeField] private Animator animator;

    int active = 0;
 
    private PlayerControls playerControls;

    public UnityEvent onUnlock;

    

    private void Awake()
    {
       texts[active].color = Color.yellow;
        playerControls ??= new();
    }

    void Start()
    {
        //self explanitory just sets the padlock messages to the defualt
        for(int i = 0; i < texts.Length - 1; i++)
        {
            texts[i].text = "0";
            nextTexts[i].text = "1";
            prevTexts[i].text = "9";

        }
    
     
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

        texts[active].color = Color.white;

      
        active += 1;
        if(active > 4)
        {
            active = 0;
        }
        texts[active].color = Color.yellow;
        //dails[active].GetComponent<TextMeshProUGUI>().color = Color.yellow;


    }
    private void GoLeft(InputAction.CallbackContext context)
    {
       
       texts[active].color = Color.white;
       
        active -= 1;
        if (active < 0)
        {
            active = 4;
        }
       
       texts[active].color = Color.yellow;
        
    }

    public void PressUp(InputAction.CallbackContext context)
    {
        //If you press the up button (currently beind done with a mouse)
        //The number will go up and the text is updated to match that
        
        if(active == 0)
        {
            currentThousandNum++;
            nextThousandNum++;
            prevThousandNum++;
            
            //since padlocks only go from 0-9 i have it set where if the number goes to 10 or above it resets to 0
            if (currentThousandNum >= 10)
            {
                currentThousandNum = 0;
               

            }
            if(nextThousandNum >= 10)
            {
                nextThousandNum = 0;
            }
            if (prevThousandNum >= 10)
            {
                prevThousandNum = 0;
            }

            texts[active].text = currentThousandNum.ToString();
            nextTexts[active].text = nextThousandNum.ToString();
            prevTexts[active].text = prevThousandNum.ToString();
        }
        else if(active == 1)
        {
            currentHundredNum++;
            nextHundredNum++;
            prevHundredNum++;
            //since padlocks only go from 0-9 i have it set where if the number goes to 10 or above it resets to 0
            if (currentHundredNum >= 10)
            {
                currentHundredNum = 0;
              

            }
            if (nextHundredNum >= 10)
            {
                nextHundredNum = 0;
            }
            if (prevHundredNum >= 10)
            {
                prevHundredNum = 0;
            }
            texts[active].text = currentHundredNum.ToString();
            nextTexts[active].text = nextHundredNum.ToString();
            prevTexts[active].text = prevHundredNum.ToString();
        }
        else if (active == 2)
        {
            currentTenNum++;
            nextTenNum++;
            prevTenNum++;
            //since padlocks only go from 0-9 i have it set where if the number goes to 10 or above it resets to 0
            if (currentTenNum >= 10)
            {
                currentTenNum = 0;
               

            }
            if (nextTenNum >= 10)
            {
                nextTenNum = 0;
            }
            if (prevTenNum >= 10)
            {
                prevTenNum = 0;
            }
            texts[active].text = currentTenNum.ToString();
            nextTexts[active].text = nextTenNum.ToString();
            prevTexts[active].text = prevTenNum.ToString();
        }
        else if (active == 3)
        {
            currentOneNum++;
            nextOneNum++;
            prevOneNum++;
            //since padlocks only go from 0-9 i have it set where if the number goes to 10 or above it resets to 0
            if (currentOneNum >= 10)
            {
                currentOneNum = 0;
               

            }
            if (nextOneNum >= 10)
            {
                nextOneNum = 0;
            }
            if (prevOneNum >= 10)
            {
                prevOneNum = 0;
            }
            texts[active].text = currentOneNum.ToString();
            nextTexts[active].text = nextOneNum.ToString();
            prevTexts[active].text = prevOneNum.ToString();
        }





    }

    public void PressDown(InputAction.CallbackContext context)
    {
        //Same idea as PressUp() but in reverse
        if (active == 0)
        {
            currentThousandNum--;
            nextThousandNum--;
            prevThousandNum--;
            //since padlocks only go from 0-9 i have it set where if the number goes to 10 or above it resets to 0
            if (currentThousandNum < 0)
            {
                currentThousandNum = 9;
              

            }
            if (prevThousandNum < 0)
            {
                prevThousandNum = 9;

            }
            if (nextThousandNum < 0)
            {
                nextThousandNum = 9;

            }


            texts[active].text = currentThousandNum.ToString();
            nextTexts[active].text = nextThousandNum.ToString();
            prevTexts[active].text = prevThousandNum.ToString();
        }
        else if (active == 1)
        {
            currentHundredNum--;
            nextHundredNum--;
            prevHundredNum--;
            //since padlocks only go from 0-9 i have it set where if the number goes to 10 or above it resets to 0
            if (currentHundredNum < 0)
            {
                currentHundredNum = 9;
                

            }
            if (prevHundredNum < 0)
            {
                prevHundredNum = 9;

            }
            if (nextHundredNum < 0)
            {
                nextHundredNum = 9;

            }
            texts[active].text = currentHundredNum.ToString();
            nextTexts[active].text = nextHundredNum.ToString();
            prevTexts[active].text = prevHundredNum.ToString();
        }
        else if (active == 2)
        {
            currentTenNum--;
            nextTenNum--;
            prevTenNum--;
            //since padlocks only go from 0-9 i have it set where if the number goes to 10 or above it resets to 0
            if (currentTenNum < 0)
            {
                currentTenNum = 9;

            }
            if (prevTenNum < 0)
            {
                prevTenNum = 9;

            }
            if (nextTenNum < 0)
            {
                nextTenNum = 9;

            }
            texts[active].text = currentTenNum.ToString();
            nextTexts[active].text = nextTenNum.ToString();
            prevTexts[active].text = prevTenNum.ToString();
        }
        else if (active == 3)
        {
            currentOneNum--;
            nextOneNum--;
            prevOneNum--;
            //since padlocks only go from 0-9 i have it set where if the number goes to 10 or above it resets to 0
            if (currentOneNum < 0)
            {
                currentOneNum = 9;
               

            }
            if (prevOneNum < 0)
            {
                prevOneNum = 9;

            }
            if (nextOneNum < 0)
            {
                nextOneNum = 9;

            }
            texts[active].text = currentOneNum.ToString();
            nextTexts[active].text = nextOneNum.ToString();
            prevTexts[active].text = prevOneNum.ToString();
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
            animator.speed = 0;
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
        FindAnyObjectByType<PlayerController>()?.ActivateControls();

       foreach(var dail in dails)
        {
            gameObject.SetActive(false);
        }

    }
   
    public void CheckIfCorrect()
    {
        float currentAnswer = GetNum();
      
        float correctAnwer;
        correctAnwer = GenerateCodeScript.code;
       
      
        if (currentAnswer == correctAnwer)
        {
            Deactivate();
            onUnlock?.Invoke();
            animator.speed = 1;
        }
    }

}
