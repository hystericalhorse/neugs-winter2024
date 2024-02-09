using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mailbox : MonoBehaviour, Interactable
{
    private LightsOutPuzzle puzzle;
    public void OnInteract()
    {
        if (puzzle != null) { puzzle.SetPuzzle(); }
    }

    // Start is called before the first frame update
    void Start()
    {
        puzzle = FindAnyObjectByType<LightsOutPuzzle>();
    }
}
