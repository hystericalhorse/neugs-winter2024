using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepSoundData : MonoBehaviour
{
    [SerializeField] private List<AudioClip> stepClips;
    public AudioClip StepClip 
    { 
        get 
        {
            if (stepClips.Count == 1) return stepClips[0]; 
            else if(stepClips.Count == 0) return null;
            return stepClips[Random.Range(0,stepClips.Count)];
        } 

    }
}
