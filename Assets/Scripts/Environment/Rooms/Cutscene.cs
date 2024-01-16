using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cutscene : MonoBehaviour, Interactable
{
    [SerializeField] Image cutsceneImages;
    public void OnInteract()
    {
        Debug.Log("PLEASE JUST WORK");
        cutsceneImages.enabled = true;

        
    }


}
