using UnityEngine;

public class Door : MonoBehaviour, Interactable
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
			FindObjectOfType<PlayerController>().transform.position = targetTransform.position;
		}
	}
}
