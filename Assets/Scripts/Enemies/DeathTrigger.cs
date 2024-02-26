using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    // Canvas
    //[SerializeField] private


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            Debug.Log("Hit");
            playerAnimator.SetBool("Dead", true);
        }
    }
}
