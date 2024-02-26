using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioTest : MonoBehaviour, Interactable
{
	public void Start()
	{
		var col = gameObject.AddComponent<BoxCollider2D>();
		col.isTrigger = true;

	}



    public void OnInteract()
	{
		AudioManager.instance.PlaySound("footsteps");
        AudioManager.instance.PlaySound("Ambience");
        AudioManager.instance.PlaySound("HouseMelody");
    }
}
