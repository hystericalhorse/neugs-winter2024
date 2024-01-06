using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTest : MonoBehaviour, Interactable
{
	public void OnInteract()
	{
		Debug.Log($"You interacted with {gameObject.name}");
	}
}
