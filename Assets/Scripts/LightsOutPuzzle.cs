using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LightsOutPuzzle : MonoBehaviour
{
    [SerializeField] private List<LightsOutPuzzleLight> puzzleLights;

    private bool isSolved = false;
    // INSTANTIATE LIGHTS MINIMAP
    public List<UnityEngine.UI.Image> imgLights; // Get images
    private Color onColor, offColor; // Change color of image
    [SerializeField] private GameObject tempExplosion;

    // Start is called before the first frame update
    void Start()
    {
        puzzleLights = GetComponentsInChildren<LightsOutPuzzleLight>().ToList();
        //imgLights = new List<UnityEngine.UI.Image>(puzzleLights.Count);
        SetPuzzle();
        
    }
    public void SetPuzzle()
    {
        ResetPuzzle();
        System.Random rand = new System.Random(Guid.NewGuid().GetHashCode());
        int index = rand.Next(0, puzzleLights.Count);
        if (puzzleLights.Count != 0)
        {

            //at least one light will be turned on
            puzzleLights[index].SetLightOn(true);
            int count = rand.Next(0, (int)(puzzleLights.Count * 0.5f));
            for (int i = 0; i < count; i++)
            {
                //can activate a light multiple times, but that's okay.
                index = rand.Next(0, puzzleLights.Count);
                puzzleLights[index].SetLightOn(true);
            }
        }
        InitializeMap();
    }
    public void ResetPuzzle()
    {
        foreach (var puzzleLight in puzzleLights)
        {

            puzzleLight.SetLightOn(false);
        }
        InitializeMap();
    }
    public bool CheckSolved()
    {
        bool output = true;
        foreach (var light in puzzleLights)
        {
            if (light.GetOn()) output = false;
        }
        if (output) { isSolved = true; TestSolve(); }
        return output;
    }

    public void TestSolve()
    {
        Instantiate(tempExplosion);
    }




    public void InitializeMap()
    {
        // int index = 0; // Index of the arrays
        onColor = new Color(255, 255, 0);
        offColor = new Color(0, 0, 0);
        // Iterate through the Lists of lights
        // When we iterate we have to make it match the index of the images

        for (int index = 0; index < puzzleLights.Count(); index++)
        {
            if ((puzzleLights[index].GetOn() == true))
            {
                Debug.Log("Current Index: " + puzzleLights[index].GetOn() + " LIGHT INDEX : " + puzzleLights[index] + " imgLights: " + imgLights[index]);
                imgLights[index].color = onColor;
            }
            else if ((puzzleLights[index].GetOn() == false))
            {
                imgLights[index].color = offColor;

            }




        }

    }
}
