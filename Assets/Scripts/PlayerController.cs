using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D theRB;
    public float jumpForce;
    public float runSpeed;
    private float activeSpeed;
    private float horizontal;
    private bool isGrounded;
    public Transform groundCheckPoint;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool canDoubleJump;
    private bool hasDoubleJumped;

    public Animator anim;
    public float knockbackLength, knockbackSpeed;
    private float knockbackCounter;

    private bool isWallSliding;
    private float wallSlidingSpeed = 2f;
    // private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    private Vector2 wallJumpingPower = new Vector2(40f, 40f);

    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    private bool isFacingRight = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);

        if(knockbackCounter <=0)
        {

        activeSpeed = moveSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            activeSpeed = runSpeed;
        }

        theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * activeSpeed, theRB.velocity.y);

        WallSlide();
        
        WallJump();

        if(Input.GetButtonDown("Jump"))
        {
            if(isGrounded == true)
            {
                Jump();
                hasDoubleJumped = false;
                canDoubleJump = true;
            }
            else if (canDoubleJump)
            {
                hasDoubleJumped = true;
                Jump();
                canDoubleJump = false;
            }
        }

        if (theRB.velocity.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        
        if (theRB.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        } else{
            knockbackCounter -= Time.deltaTime;

            theRB.velocity = new Vector2(knockbackSpeed * -transform.localScale.x, theRB.velocity.y);
        }

        // Handle animation
        anim.SetFloat("speed", Mathf.Abs(theRB.velocity.x));
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("ySpeed", theRB.velocity.y);
        anim.SetBool("hasDoubleJumped", hasDoubleJumped);
    }

    void Jump()
    {
        theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
    }

    private bool isWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }
    
    private void WallSlide()
    {
        if (isWalled() && !isGrounded && horizontal != 0f)
        {
            isWallSliding = true;
            theRB.velocity = new Vector2(theRB.velocity.x, Math.Clamp(theRB.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            // isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            // isWallJumping = true;
            theRB.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }
    
    private void StopWallJumping()
    {
        // isWallJumping = false;
    }

    public void Knockback()
    {
        theRB.velocity = new Vector2(0f, jumpForce*.5f);
        anim.SetTrigger("isKnockingBack");

        knockbackCounter = knockbackLength;
    }
}