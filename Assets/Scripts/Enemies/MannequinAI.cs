using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class MannequinAI : MonoBehaviour {
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriterenderer;
    [SerializeField] private float Speed = 10;
    private GameObject Player;
    private Rigidbody2D RB;
    private Animator Anim;
    private Vector2 Velocity = Vector2.zero;
    private bool Detected = false;

    void Start() {
		Player = PlayerManager.instance.GetPlayerController().gameObject;
		RB = GetComponent<Rigidbody2D>();
        spriterenderer = GetComponent<SpriteRenderer>();
        Anim = GetComponent<Animator>();
    }

    void Update() {
        if (Detected) { 
            RB.velocity = Vector2.zero;
            return;
        }

        Vector2 Direction = Player.transform.position - transform.position;
        Direction.Normalize();
        Velocity = Direction * Speed;
        
        Velocity.x = Direction.x * Speed;
        Velocity.y = Direction.y * Speed;
        RB.velocity = Velocity;

        // Update the animator
        animator.SetFloat("XPosition", Direction.x);
        animator.SetFloat("YPosition", Direction.y);
        //animator.SetFloat("Speed", Velocity.magnitude);
    }

   


    private void OnTriggerEnter2D(Collider2D collision) {
      
        if (collision.transform.CompareTag("PlayerDetection")) {
            Detected = true;
            Anim.speed = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.transform.CompareTag("PlayerDetection")) {
            Detected = true;
            Anim.speed = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.transform.CompareTag("PlayerDetection")) {
            Detected = false;

            Anim.speed = 1;
        }
    }
}