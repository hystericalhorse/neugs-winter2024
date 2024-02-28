using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeathTrigger : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private GameObject gameOver;
    //[SerializeField] private Animator gameOverAnimator;
    // Canvas
    //[SerializeField] private

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit");
            playerAnimator.SetBool("Dead", true);
            Invoke("GameOverCanvas",2);
            //gameOverAnimator.StopPlayback();
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        }
    }

    private void GameOverCanvas()
    {
        Instantiate(gameOver);
    }


}
