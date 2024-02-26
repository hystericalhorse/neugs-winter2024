using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightsOutPuzzleLight : MonoBehaviour, Interactable
{
    [SerializeField] private GameObject lightObject;
    [SerializeField] private List<LightsOutPuzzleLight> neighbors;
    [SerializeField] private LightsOutPuzzle puzzle;
    
    private bool lightOn = false;

    private void Start()
    {
        //lightObject = GetComponentInChildren<Light2D>().gameObject;
        puzzle = FindAnyObjectByType<LightsOutPuzzle>();

        lightObject.SetActive(false);
    }
    public void OnInteract()
    {
        ToggleLightOn();
        puzzle.InitializeMap();
        foreach(var neighbor in neighbors) 
        {
            neighbor.ToggleLightOn();
            neighbor.puzzle.InitializeMap();
        }
        puzzle.CheckSolved();
    }

    public void SetLightOn(bool on = true)
    {
        lightOn = on;
        lightObject.SetActive(lightOn);
    }

    public void ToggleLightOn()
    {
        lightOn = !lightOn;
        lightObject.SetActive(lightOn);
    }

    public bool GetOn()
    {
        return lightOn;
    }
}
