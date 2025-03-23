using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour
{
    public float speed;
    public float JumpForce;
    private float MoveInput;

    private bool facingRight = true;
    private Rigidbody2D rb;

    private bool isGrounded;
    public Transform feetPos;
    public float CheckRadius;
    public LayerMask whatIsGround;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MoveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(MoveInput * speed, rb.velocity.y);
        if (facingRight == false && MoveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && MoveInput < 0)
        {
            Flip();
        }
        if(MoveInput == 0)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
        }
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, CheckRadius, whatIsGround);

        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.up * JumpForce;
            anim.SetTrigger("takeOF");
        }

        if(isGrounded == true)
        {
        anim.SetBool("isJumping", false);
        }
        else
        {
        anim.SetBool("isJumping", true);
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;

        if (MoveInput < 0)
        {
            transform.eulerAngles = new Vector3 (0, 180, 0);
        }

        else if (MoveInput > 0)
        {
            transform.eulerAngles = new Vector3 (0, 0, 0);
        }
    }

}
