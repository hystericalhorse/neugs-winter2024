using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepSoundFeedback : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform agent;
    [SerializeField] private AudioSource audioSource;

    public void PlayStepSound()
    {
        Collider2D collider = Physics2D.OverlapPoint(agent.position, layerMask);
        if (collider != null)
        {
            StepSoundData data = collider.GetComponent<StepSoundData>();
            if (data != null && (audioSource.isPlaying == false || audioSource.clip != data.StepClip || audioSource.time / audioSource.clip.length > 0.2f))
            {
                audioSource.clip = data.StepClip;
                audioSource.pitch = Random.Range(0.95f, 1.05f);
                audioSource.Play();
            }
            
        }
    }
}

