using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolaroidPickup : MonoBehaviour, Interactable
{    
    [SerializeField] MonsterWall connectedWall;

	public void OnInteract()
    {
        connectedWall.UpdateWall();
        Destroy(gameObject);
    }
}
