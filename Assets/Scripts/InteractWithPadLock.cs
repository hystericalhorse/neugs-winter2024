using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractWithPadLock : MonoBehaviour, Interactable
{
    [SerializeField] GameObject padlock;
    [SerializeField] GenerateCodeScript answerGener;
    [SerializeField] PadLockedScript padLockLogic;
    private void Start()
    {
       padlock.SetActive(false);
    }

    public void OnInteract()
    {
        //This check ensures Ash checks the location where the answer is told to her before she can input anything.
        if (answerGener.CheckedAnswer())
        {
            padlock.SetActive(true);

        }
        else
        {
            //This part would be what happens when Ash says that she needs to check the whiteboard or whatever it is she needs to look at.
            Debug.Log("Ash needs to check the code first >:(");
        }
       
        if(padLockLogic.GetAnswer())
        {
            padlock.SetActive(false);
        }

    }

}
