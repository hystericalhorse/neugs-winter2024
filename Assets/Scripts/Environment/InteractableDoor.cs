using UnityEngine;

public class InteractableDoor : MonoBehaviour, Interactable
{
    public bool Locked = false;
	public Transform targetTransform;

	public void OnInteract()
	{
		if (Locked)
		{
			Debug.Log("Door is locked");
			return;
		}
		else
		{
			PlayerManager.instance.PlacePlayerController(targetTransform.position);
		}
	}
}
