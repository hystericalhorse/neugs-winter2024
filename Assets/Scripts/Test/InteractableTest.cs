using UnityEngine;

public class InteractableTest : MonoBehaviour, Interactable
{
	public void OnInteract()
	{
		Debug.Log($"You interacted with {gameObject.name}");
	}
}
