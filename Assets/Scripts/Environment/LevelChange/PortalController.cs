using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Apple;

public class PortalController : MonoBehaviour
{
   // public Transform prevLevel; // Spawn in other room transform
    GameObject player;
    

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    public void OnTeleport( Transform nextLevel)
    {
        if (Vector2.Distance(player.transform.position, transform.position) > 0.3f)
            player.transform.position = nextLevel.transform.position;

        

    }
}
