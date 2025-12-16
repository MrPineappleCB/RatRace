using UnityEngine;
using Pathfinding;
using System.Data.Common;
using Unity.VisualScripting;
using NUnit.Framework;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Transform losePosititon;
    [SerializeField] private Transform endStartPosition;

    [SerializeField] private float jumpStrength = 10f;
    [SerializeField] private float wallSlideSpeed = 2f;

    private bool isWallSliding;
    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.05f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 1f;
    private Vector2 wallJumpingPower = new Vector2(5f, 7f);

    public Transform target;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    public Transform enemyGFX;

    Path path;
    int currentWaypoint = 0;
    bool EndofPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    private Animator marthAnimator;
    private float movement;

    public GameObject gamemanager;
    public MainMenu mainMenu;


    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        marthAnimator = GetComponent<Animator>();
        gamemanager = GameObject.FindGameObjectWithTag("GameManager");
        mainMenu = gamemanager.GetComponent<MainMenu>();
        Speed();

        InvokeRepeating("UpdatePath", 0f, .5f);
        
    }
    
    void UpdatePath()
    {
        if (seeker.IsDone())
        seeker.StartPath(rb.position, target.position, OnPathComplete);
    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    public bool isGrounded()
    {
        marthAnimator.SetBool("Air", false);
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer); //Returns true if circle around the position of ground check is touching/inside the groundlayer
    }
    public bool isWallCling()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer); //Returns true if circle around the position of ground check is touching/inside the walllayer
    }

    void Speed()
    {
        if (mainMenu.difficulty == 1)
        {
            speed = 500;
        }
        else if (mainMenu.difficulty == 2)
        {
            speed = 600;
        }
        else if (mainMenu.difficulty == 3)
        {
            speed = 700;
        }
    }
    
    void Update()
    {
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

        if (direction.y + 1 > transform.position.y && isWallCling())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpStrength);
            marthAnimator.SetBool("Air", true);
        }

        if (transform.position == target.position)
        {
            this.enabled = false;
        }
    }
    void FixedUpdate()
    {
        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            EndofPath = true;
            return;
        }
        else
        {
            EndofPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        Vector2 velocity = rb.linearVelocity;

        velocity.x = force.x;
        rb.linearVelocity = velocity;

        if (direction.y > transform.position.y && isWallCling())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpStrength);
        }

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (force.x >= 0.01f)
        {
            enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (force.x <= -0.01f)
        {
            enemyGFX.localScale = new Vector3(1f, 1f, 1f);
        }

        WallSlide();
        WallJump();
        
        marthAnimator.SetFloat("Speed", Mathf.Abs(velocity.x));
    }
    private void WallSlide()
    {
        if (isWallCling() && !isGrounded() && rb.linearVelocity.x != 0f)
        {
            isWallSliding = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Clamp(rb.linearVelocity.y, -wallSlideSpeed, float.MaxValue));
            marthAnimator.SetBool("Climb", true);
        }
        else
        {
            isWallSliding = false;
            marthAnimator.SetBool("Climb", false);
        }
    }
    private void WallJump()
    {
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized; 
        //marthAnimator.SetBool("Climb", true);  
        
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (direction.y > transform.position.y && isWallCling() && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.linearVelocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }
    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    public void Lose()
    {
        seeker.enabled = false;
        transform.position = endStartPosition.position;
        target.position = losePosititon.position;
    }

}
