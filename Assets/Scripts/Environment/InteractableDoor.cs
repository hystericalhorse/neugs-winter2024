using UnityEngine;

public class InteractableDoor : MonoBehaviour, Interactable
{
    public bool Locked = false;
    public Transform targetTransform;
    RotaryPadLocks padlockUnlock;

    public void OnInteract()
    {
        Debug.Log("HasKey Current Value: " + padlockUnlock.hasKey);
        if (!Locked && padlockUnlock.hasKey)
        {
            Debug.Log("Door is locked");
            return;
        }
        else if (Locked && (padlockUnlock.hasKey == true))
        {
            PlayerManager.instance.PlacePlayerController(targetTransform.position);
        }
    }
}
