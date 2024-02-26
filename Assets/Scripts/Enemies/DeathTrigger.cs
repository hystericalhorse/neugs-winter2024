using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    public Animator playerAnimator;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("GET SHIT ON YOU FUCKING CRAP GRAHHH");
            playerAnimator.SetBool("Dead", true);



        }
    }
 
}
