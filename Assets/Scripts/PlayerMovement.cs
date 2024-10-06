using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    private float xInput;
    private Rigidbody2D rig;
    private Animator anim;
    private bool isGrounded;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float groundCheckDistance;

    [Header("Dash Info")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashTime;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Movement();
        CheckInput();
        

        if (dashTime > 0)
        {
            DashMovement();
            dashTime -= Time.deltaTime;
        }
        else if (dashTime <= 0) // Reset dash when duration ends
        {
            dashTime = 0;
        }

        //dashTime = dashTime - Time.deltaTime;
        dashTime -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            dashTime = dashDuration;
        }

        AnimatorController();
        Flip();
    }

    private void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        rig.velocity = new Vector2(xInput * speed, rig.velocity.y);

    }
    private void DashMovement()
    {
        if (dashTime > 0)
        {
            rig.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * dashSpeed, rig.velocity.y);
            Debug.Log("Dash");
        }
        else
        {
            rig.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, rig.velocity.y);
            Debug.Log("Ko Dash");
        }
    }

    private void Jump()
    {
        rig.velocity = new Vector2(rig.velocity.x, jumpForce);
        isGrounded = false;
        Debug.Log("khong cham dat" + isGrounded);
    }
    private void AnimatorController()
    {
        
        bool isMoving = rig.velocity.x != 0;

        anim.SetFloat("yVelocity", rig.velocity.y);
        anim.SetBool("IsMoving", isMoving);
        anim.SetBool("isGrounded", isGrounded);
    }
    private void Flip()
    {
        if (xInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (xInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);
    }

   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
        Debug.Log("Cham dat" + isGrounded);
    }

}
