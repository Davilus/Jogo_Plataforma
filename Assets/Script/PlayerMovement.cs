using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

public class PlayerMovement : MonoBehaviour
{
    private bool isFacingRight = true;
    private Vector2 movementInput;
    private bool doubleJump = false;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpingPower = 16f;

    public ParticleSystem dust;

    private bool start = true;

    PlayerLife playerLife;
    [SerializeField] GameObject player;

    private float coyoteTimeCounter;
    [SerializeField] private float coyoteTime = 0.1f;

    private bool isWallSliding;
    [SerializeField] private float wallSlidingSpeed = 2f;

    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    [SerializeField] private float wallJumpingDuration = 0.4f;
    [SerializeField] private Vector2 wallJumpingPower = new Vector2(8f, 16f);

    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private float dashingPower = 24f;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 1f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private TrailRenderer tr;

    private bool isLadder;
    private bool isClimbing;

    [SerializeField] private bool tesouro1 = false;
    [SerializeField] private bool tesouro2 = false;
    [SerializeField] private bool tesouro3 = false;

    [SerializeField] private AudioSource jumpSoundEffect;
    [SerializeField] private AudioSource dashSoundEffect;

    [SerializeField] private InputActionReference movement, jump, dash, hold;

    private Animator anim;
    private enum MovementState {idle, running, jumping, falling, wallSliding, climbing, climbingIdle, dying, dashing}

    private void OnEnable()
    {
        jump.action.performed += PerformJump;
        jump.action.started += StartJump;
        jump.action.canceled += CancelJump;
        dash.action.performed += PerformDash;
        hold.action.performed += PerformHold;
        hold.action.started += StartHold;
    }

    private void OnDisable()
    {
        jump.action.performed -= PerformJump;
        jump.action.started -= StartJump;
        jump.action.canceled -= CancelJump;
        dash.action.performed -= PerformDash;
        hold.action.performed -= PerformHold;
        hold.action.started -= StartHold;
    }

    private void StartHold(InputAction.CallbackContext obj)
    {
        if (isLadder)
        {
            isClimbing = true;
            rb.gravityScale = 0f;
            speed = 5f;
        }
    }

    private void PerformHold(InputAction.CallbackContext obj)
    {
        if (isLadder)
        {
            isClimbing = true;
            rb.gravityScale = 0f;
            speed = 5f;
        }
    }

    private void PerformDash(InputAction.CallbackContext obj)
    {
        if (tesouro2)
        {
            if (canDash && rb.bodyType == RigidbodyType2D.Dynamic)
            {
                dashSoundEffect.Play();
                StartCoroutine(Dash());
            }
        }
        if (isDashing)
        {
            return;
        }
    }

    private void CancelJump(InputAction.CallbackContext obj)
    {
        if (rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            coyoteTimeCounter = 0f;
        }
    }

    private void StartJump(InputAction.CallbackContext obj)
    {
        if ((rb.bodyType == RigidbodyType2D.Dynamic))
        {
            if (coyoteTimeCounter > 0f || isClimbing || (doubleJump && tesouro3))
            {
                CreateDust();
                jumpSoundEffect.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);

                doubleJump = !doubleJump;
                isClimbing = false;
            }
        }

        if (wallJumpingCounter > 0f && rb.bodyType == RigidbodyType2D.Dynamic)
        {
            doubleJump = true; // doubleJumpAllowed?
            CreateDust();
            jumpSoundEffect.Play();
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
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

    private void PerformJump(InputAction.CallbackContext obj)
    {
        if ((isGrounded()|| (coyoteTimeCounter > 0f)))
        {
            doubleJump = true; // doubleJumpAllowed?
        }
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb.bodyType = RigidbodyType2D.Static;
        Invoke("StartLevel", 1f);
        playerLife = player.GetComponent<PlayerLife>();
    }

    void Update()
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = (Vector2)Vector3.ClampMagnitude((Vector3)rb.velocity, maxSpeed);
        }

        if (isGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }


        if (rb.bodyType == RigidbodyType2D.Dynamic)
        {
            movementInput = movement.action.ReadValue<Vector2>();
        }

        if (movementInput.x != 0)
        {
            gameObject.transform.SetParent(null);
        }

        if (tesouro1 && rb.bodyType == RigidbodyType2D.Dynamic)
        {
            if (isWalled() && !isGrounded() && movementInput.x != 0f)
            {
                isWallSliding = true;
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
            }
            else
            {
                isWallSliding = false;
            }
            WallJump();
        }

        if (!isWallJumping)
        {
            Flip();
        }

        if (!isLadder)
        {
            speed = 8f;
        }

        // TIRAR QUANDO LANÇAR O JOGO
        if (Input.GetKeyDown(KeyCode.K))
        {
            isClimbing = true;
            speed = 10f;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            tesouro1 = true;
            tesouro2 = true;
            tesouro3 = true;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            isClimbing = false;
            speed = 8f;
        }

        UpdateAnimationState();

    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        if (!isWallJumping)
        {
            rb.velocity = new Vector2(movementInput.x * speed, rb.velocity.y);
        }
        if (isClimbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, movementInput.y * speed);
        }
        else
        {
            rb.gravityScale = 7f;
        }
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private bool isWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
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
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private void Flip()
    {
        if (isFacingRight && movementInput.x < 0f || !isFacingRight && movementInput.x > 0f)
        {
            CreateDust();
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
        }

        if (collision.gameObject.CompareTag("Tesouro1"))
        {
            Destroy(collision.gameObject);
            tesouro1 = true;
        }
        if (collision.gameObject.CompareTag("Tesouro2"))
        {
            Destroy(collision.gameObject);
            tesouro2 = true;
        }
        if (collision.gameObject.CompareTag("Tesouro3"))
        {
            Destroy(collision.gameObject);
            tesouro3 = true;
        }
        if (collision.gameObject.CompareTag("Finished"))
        {
            start = true;
            rb.bodyType = RigidbodyType2D.Static;
        }
        if (collision.gameObject.CompareTag("JumpPad"))
        {
            doubleJump = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
        }
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (movementInput.x != 0f && !isClimbing && !isDashing)
        {
            state = MovementState.running;
        }
        else if ((movementInput.x != 0f && isClimbing) || (movementInput.y != 0f && isClimbing))
        {
            state = MovementState.climbing;
        }
        else if (isClimbing)
        {
            state = MovementState.climbingIdle;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f && !isClimbing)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f && !isClimbing && !isDashing)
        {
            state = MovementState.falling;
        }

        if (isWallSliding)
        {
            state = MovementState.wallSliding;
        }

        if ((movementInput.x != 0f && isClimbing) || (movementInput.y != 0f && isClimbing))
        {
            state = MovementState.climbing;
        }

        if (isDashing)
        {
            state = MovementState.dashing;
        }

        if (playerLife.morto == true)
        {
            state = MovementState.dying;
        }
        if (start)
        {
            state = MovementState.idle;
        }


        anim.SetInteger("state", (int)state);

    }

    private void CreateDust()
    {
        dust.Play();
    }

    private void StartLevel()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        start = false;
    }
}
