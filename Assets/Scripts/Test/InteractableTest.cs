using UnityEngine;

public class InteractableTest : MonoBehaviour, Interactable
{
	public void OnInteract()
	{
		LevelManager.instance.SaveLevel();
	}
}
