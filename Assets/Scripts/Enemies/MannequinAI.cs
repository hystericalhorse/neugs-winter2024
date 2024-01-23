using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MannequinAI : MonoBehaviour {
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriterenderer;
    [SerializeField] private float Speed = 10;

    private GameObject Player;
    private Rigidbody2D RB;
    private Vector2 Velocity = Vector2.zero;

    private void Awake() {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Start() {
        RB = GetComponent<Rigidbody2D>();
        spriterenderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
        Vector2 Direction = Player.transform.position - transform.position;
        Direction.Normalize();
        Velocity = Direction * Speed;

        Velocity.x = Direction.x * Speed;
        Velocity.y = Direction.y * Speed;
        RB.velocity = Velocity;

        // Update the animator
        //animator.SetFloat("Horizontal", Direction.x);
        //animator.SetFloat("Vertical", Direction.y);
        //animator.SetFloat("Speed", Velocity.magnitude);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }
}