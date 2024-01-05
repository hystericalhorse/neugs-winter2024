using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovememn : MonoBehaviour
{
    //This is where all the variables live Obviously
    private Rigidbody2D rb;
    private Vector2 velocity = Vector2.zero;
    [SerializeField] float speed;


    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = Vector2.zero;

        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        velocity.x = direction.x * speed;
        velocity.y = direction.y * speed;
       
        rb.velocity = velocity;
        
    }
}
