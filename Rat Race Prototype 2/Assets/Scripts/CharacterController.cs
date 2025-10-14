using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Adjustable")]
    public float walkSpeed = 10f;
    public float savedSpeed;
    [SerializeField] private float jumpStrength = 10f;
    [SerializeField] private float wallSlideSpeed = 2f;
    

    [Header("References")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;


    //Private Variables
    private bool isWallSliding;
    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 1f;
    private Vector2 wallJumpingPower = new Vector2(5f, 7f);
    private Rigidbody2D rb;
    private float movement;
    private float coyoteTime = 0.1f;
    private float coyoteTimeCounter;
    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;
    private bool facingRight;

    public bool doubleJump;
    public bool canFloat = false;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>(); //Sets rb to the rigidbody2d of the player
    }

    public bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer); //Returns true if circle around the position of ground check is touching/inside the groundlayer
    }

    public bool isWallCling()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer); //Returns true if circle around the position of ground check is touching/inside the walllayer
    }

    private void Update()
    {
        if (isGrounded())
        {
            coyoteTimeCounter = coyoteTime; //Sets the coyote time counter to the default time, lets you jump a bit after you're not touching the ground anymore, game stuff yes
            rb.gravityScale = 1;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime; //if not grounded removes time from timer
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime; //just lets you jump before you actually hit the ground, good for game feel ykyk, does the same thing as the coyote timer
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime; // same thing as the coyote timer except if you didnt press the jump button
        }

        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f || doubleJump == true && Input.GetButtonDown("Jump"))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpStrength); // jump yes
            jumpBufferCounter = 0f;
            doubleJump = false;
            //savedSpeed = walkSpeed;
            Invoke(("DelayFloat"), 1f);
        }

        if (isGrounded() && !Input.GetButton("Jump"))
        {
            doubleJump = false;
            canFloat = false;
        }

        if ((!isGrounded() || !isWallCling()) && Input.GetButtonDown("Jump") && canFloat)
        {
            canFloat = false;
            rb.gravityScale = 0.25f;
            //savedSpeed = walkSpeed;
            walkSpeed = walkSpeed * 0.7f;
        }
        if ((!isGrounded() || !isWallCling()) && Input.GetButtonUp("Jump"))
        {
            walkSpeed = savedSpeed;
            canFloat = true;
            rb.gravityScale = 1f;
        }

        WallSlide();
        WallJump();
        Flip();

        if (!isWallJumping)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        movement = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(movement * walkSpeed, rb.linearVelocity.y); // walk yes

        if (!isWallJumping)
        {
            rb.linearVelocity = new Vector2(movement * walkSpeed, rb.linearVelocity.y);
        }
    }

    private void Flip()
    {
        if (movement > 0 && facingRight || (movement < 0 && !facingRight))
        {
            facingRight = !facingRight; //Flips the facingright bool around
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale; // flips player
        }
    }

    private void DelayFloat()
    {
        canFloat = true;
    }

    private void WallSlide()
    {
        if (isWallCling() && !isGrounded() && movement != 0f)
        {
            isWallSliding = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Clamp(rb.linearVelocity.y, -wallSlideSpeed, float.MaxValue));
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
            isWallJumping = false;
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
            isWallJumping = true;
            rb.linearVelocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                facingRight = !facingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

}