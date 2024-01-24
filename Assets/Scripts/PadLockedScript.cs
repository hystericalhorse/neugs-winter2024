using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadLockedScript : MonoBehaviour, Interactable
{
    [SerializeField] GenerateCodeScript answerGener;
    public bool answered = false;
    float currentAnswer;

   //unfortunetly the logic as is requires this script to be friends with every single indivual number. names describe where they are in order

    //This is the one in the farthest left
    [SerializeField] PadLockedTempController thousandsPlace;

    //Middle left
    [SerializeField] PadLockedTempController hundredsPlace;

    //Middle right
    [SerializeField] PadLockedTempController tensPlace;

    //Far right
    [SerializeField] PadLockedTempController onesPlace;

    
 


    public void OnInteract()
    {

       //This adds the 4 numbers inputed into the pad lock to make the code
       //each one is being multiplied by their value in order to make it actually correct
       //if we didnt do the multiply it would instead be 5 + 4 + 6 + 1 = 16 instead of 5000 + 400 + 60 + 1 = 5461 which would be the code
       //we dont multiply the ones place because it would be onesPlace.GetNum() * 1 which just equals onesPlace.GetNum()
       currentAnswer = (thousandsPlace.GetNum() * 1000) + (hundredsPlace.GetNum() * 100) + (tensPlace.GetNum() * 10) + onesPlace.GetNum();
      
            float correctAnwer;
            correctAnwer = answerGener.ReturnCode();
            //very simple check to see if the generated answer equals the displayed answer filler result
            if (currentAnswer == correctAnwer)
            {
                Debug.Log("Yippie!");
                answered= true;
            }
            else
            {
                Debug.Log("Womp womp");
                answered= true;
            }
     

    }

    public bool GetAnswer()
    {
        return answered;
    }
}
