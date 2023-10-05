using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public float jumpHeight = 7f;
    private Rigidbody2D rbody;
    private Animator anim;
    private bool grounded, faceRight = true;
    
    private void Awake() 
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate() {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        rbody.velocity = new Vector2(horizontalInput * speed, rbody.velocity.y);
        anim.SetBool("Walk", horizontalInput != 0);

        if ((horizontalInput > 0 && !faceRight) || (horizontalInput < 0 && faceRight)) {
            flip();
        }

        /* 
        if (horizontalInput > 0) {
            anim.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (horizontalInput < 0) {
            anim.GetComponent<SpriteRenderer>().flipX = true;
        } */

        if (Input.GetKey(KeyCode.Space) && grounded) {
            jump();
        }
        
    }

    private void jump() {
        rbody.velocity = new Vector2(rbody.velocity.x, jumpHeight);
        anim.SetTrigger("Jump");
        grounded = false;
    }

    private void flip() {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        faceRight = !faceRight;
    }   

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground"));
        grounded = true;
    }
}
