using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedPlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public float jumpHeight = 7f;
    public float dashspeed = 20f;
    public float crouchHeight =0.5f;
    public LayerMask whatIsGround;
    public Transform groundCheckPoint;
    public float groundCheckRadius = 0.2f;
    public AudioClip jumpSound;
    public AudioClip dashSound;
    public AudioClip footstepSound;
    private Rigidbody2D rbody;
    private Animator anim;
    private AudioSource audioSource;
private bool grounded, faceRight = true, canDoubleJump = false, isDashing = false, isCrouching = false;
    
    private void Awake() 
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate() {
        //grounded = Physics2D.OverlapCircle(groundCheckPoint, groundCheckRadius, );
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

        if (Input.GetKeyDown(KeyCode.Space) && grounded) {
            canDoubleJump = true;
            jump();
            
        }
        else if (Input.GetKeyDown(KeyCode.Space) && canDoubleJump) {
            canDoubleJump = false;
            jump();
        }    
        
        if (Input.GetKeyDown(KeyCode.DownArrow) && grounded) {
            if (!isCrouching) {
                transform.localScale = new Vector3(transform.localScale.x, crouchHeight, transform.localScale.z);
                isCrouching = true;
            }
            else if (isCrouching) {
                transform.localScale = new Vector3(transform.localScale.x, 1, transform.localScale.z);
                isCrouching = false;
            }

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

    /* private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground"));
        grounded = true;
    } */
}
