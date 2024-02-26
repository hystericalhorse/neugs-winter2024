using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeprecatedCutscene : MonoBehaviour, Interactable
{
    [SerializeField] Image cutsceneImages;

    private void Awake()
    {
        cutsceneImages.enabled = false;
    }

    public void OnInteract()
    {
        //So true bestie
        Debug.Log("PLEASE JUST WORK");
        cutsceneImages.enabled = true;

    }
}
