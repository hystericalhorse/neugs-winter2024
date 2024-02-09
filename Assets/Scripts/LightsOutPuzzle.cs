using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LightsOutPuzzle : MonoBehaviour
{
    [SerializeField] private List<LightsOutPuzzleLight> puzzleLights;
    private bool isSolved = false;
    [SerializeField] private GameObject tempExplosion;

    // Start is called before the first frame update
    void Start()
    {
        puzzleLights = GetComponentsInChildren<LightsOutPuzzleLight>().ToList();
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
    }
    public void ResetPuzzle()
    {
        foreach(var puzzleLight in puzzleLights)
        {
            puzzleLight.SetLightOn(false);
        }
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
}
