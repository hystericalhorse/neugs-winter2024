using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolaroidPickup : MonoBehaviour, Interactable
{
    public string key;

    public void OnInteract()
    {
        LevelManager.instance.SetFlag(key, true);
        Destroy(gameObject);
    }
}
