using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeathTrigger : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private GameObject gameOver; 
    // Canvas
    //[SerializeField] private

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerAnimator.SetBool("Dead", true);
            Invoke("SummonGameOverUI", 2);
        }
    }

    void SummonGameOverUI()
    {
        gameOver.SetActive(true);
    }
 
}
