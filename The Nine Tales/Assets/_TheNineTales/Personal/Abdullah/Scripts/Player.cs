using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float m_jumpApexHeight = 3f;

    [SerializeField]
    private float m_jumpApexTime = .133f;

    [SerializeField]
    private float m_terminalVelocity = -20f;

    [SerializeField]
    private float m_jumpBufferTime = 0.2f;

    [SerializeField]
    private float m_accelerationTimeFromRest = .5f;

    [SerializeField]
    private float m_decelerationTimeToRest = .25f;

    [SerializeField]
    private float m_maxHorizontalSpeed = 7f;

    [SerializeField]
    private float m_accelerationTimeFromQuickturn = .125f;

    [SerializeField]
    private float m_decelerationTimeFromQuickturn = .125f;

    [SerializeField]
    private float DashDuration = 1;

    [SerializeField]
    private float DashCooldown = 3;

    [SerializeField]
    private float DashSpeed = 10;

    [SerializeField]
    private float GroundedCastDistance = 0.1f;
    public Vector2 BoxSize = new Vector2(0.9f, 1.3f);
    public SpriteRenderer PlayerSprite;

    bool CanDash = true;
    public bool isDashing = false;

    public enum FacingDirection { Left, Right }
    bool isQuickTurning;
    bool isBufferJumpReady;
    float m_bufferJumpCountdown;
    float m_CoyoteCountdown;

    Rigidbody2D m_PlayerRigidBody;
    public Vector2 m_PlayerVelocity = Vector2.zero;
    public Vector2 m_PlayerAccel = Vector2.zero;
    FacingDirection LastFacingDirection = FacingDirection.Right;
    LayerMask GroundLayer = 1 << 8;



    // health
    public static Player instance;

    public float m_FullHealth = 10;
    public float m_CurrentHealth = 10;
    float m_damageReduction = 0;


    private void Awake()
    {
        instance = this;
        print(instance.gameObject);
    }


    private void Start()
    {

        m_PlayerRigidBody = GetComponent<Rigidbody2D>();
        m_PlayerAccel.y = -3;
        m_CurrentHealth = m_FullHealth;
    }
    private void Update()
    {

        HandleHorizontalMovement();

        // Vertical Movement    
        GroundIt();
        m_PlayerVelocity.y = m_PlayerVelocity.y + (m_PlayerAccel.y * Time.deltaTime);

        RegularJump();
        //BufferJump();
        // The following function ensures that the knight does not exceed TERMINAL VELOCITY
        TerminalVelocity();
        HandleDash();

        m_PlayerRigidBody.velocity = m_PlayerVelocity;
        LastFacingDirection = GetFacingDirection();
    }

    //**********************************************************************************************************************************


    public bool IsWalking()
    {

        if (Mathf.Abs(m_PlayerRigidBody.velocity.x) > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsGrounded()
    {
        if (Physics2D.BoxCast(transform.position, BoxSize, 0f, Vector2.down, GroundedCastDistance, GroundLayer))
        {
            return true;

        }
        else
        {
            return false;
        }
    }

    public FacingDirection GetFacingDirection()
    {
        if (InputTracker.GetDirectionalInput().x > 0)
        {
            PlayerSprite.flipX = true;


            return FacingDirection.Right;
        }
        else if (InputTracker.GetDirectionalInput().x < 0)
        {
            PlayerSprite.flipX = false;


            return FacingDirection.Left;
        }
        else
        {

            PlayerSprite.flipX = PlayerSprite.flipX;

            return LastFacingDirection;
        }

    }

    //***** WALK ******** WALK ******** WALK ******** WALK ******** WALK ******** WALK ******** WALK ******** WALK ******** WALK ******** WALK ******** WALK ******** WALK ******** WALK *******

    void HandleHorizontalMovement()
    {
        if (InputTracker.GetDirectionalInput().x != 0)
        {
            if (Mathf.Sign(InputTracker.GetDirectionalInput().x) != Mathf.Sign(m_PlayerRigidBody.velocity.x))
            {
                //QuickTurn decceleration
                isQuickTurning = true;
                // Set decceleration value
                m_PlayerAccel.x = m_maxHorizontalSpeed / m_decelerationTimeFromQuickturn;
                // Set decceleration direction...... opposite to veleocity
                m_PlayerAccel.x = m_PlayerAccel.x * Mathf.Sign(m_PlayerRigidBody.velocity.x) * -1;

                // Set X Velocity
                m_PlayerVelocity.x = m_PlayerVelocity.x + (m_PlayerAccel.x * Time.deltaTime);
            }
            else if (isQuickTurning)
            {
                // Quickturn Acceleration
                // Set acceleration value
                m_PlayerAccel.x = m_maxHorizontalSpeed / m_accelerationTimeFromQuickturn;
                // Set accelertaion direction
                m_PlayerAccel.x = m_PlayerAccel.x * Mathf.Sign(InputTracker.GetDirectionalInput().x);

                // Set X Velocity
                m_PlayerVelocity.x = m_PlayerVelocity.x + (m_PlayerAccel.x * Time.deltaTime);
                LimitSpeed();
            }
            else
            {
                // Regular Acceleration
                // Set acceleration value
                m_PlayerAccel.x = m_maxHorizontalSpeed / m_accelerationTimeFromRest;
                // Set accelertaion direction
                m_PlayerAccel.x = m_PlayerAccel.x * Mathf.Sign(InputTracker.GetDirectionalInput().x);

                // Set X Velocity
                m_PlayerVelocity.x = m_PlayerVelocity.x + (m_PlayerAccel.x * Time.deltaTime);
                LimitSpeed();
            }
        }
        else if (IsWalking())
        {
            // Set decceleration value
            m_PlayerAccel.x = m_maxHorizontalSpeed / m_decelerationTimeToRest;
            // Set decceleration direction...... opposite to veleocity
            m_PlayerAccel.x = m_PlayerAccel.x * Mathf.Sign(m_PlayerRigidBody.velocity.x) * -1;

            // Set X Velocity
            m_PlayerVelocity.x = m_PlayerVelocity.x + (m_PlayerAccel.x * Time.deltaTime);
            // prevent going back in reverse
            if (Mathf.Sign(m_PlayerVelocity.x) != Mathf.Sign(m_PlayerRigidBody.velocity.x))
            {
                m_PlayerVelocity.x = 0;
            }
            // If no input present, reset quickturning 
            isQuickTurning = false;
        }
    }


    void LimitSpeed()
    {
        if (!isDashing)
        {
            // Prevent player from going over speed limit
            if (m_PlayerVelocity.x > m_maxHorizontalSpeed)
            {
                m_PlayerVelocity.x = m_maxHorizontalSpeed;
                // Reset quick turning
                isQuickTurning = false;
            }
            if (m_PlayerVelocity.x < -m_maxHorizontalSpeed)
            {
                isQuickTurning = false;
                m_PlayerVelocity.x = -m_maxHorizontalSpeed;
            }
        }




    }

    void HandleDash()
    {
        if (Input.GetKeyDown("s") && CanDash)
        {
            StartDash();
        }
        if (isDashing)
        {
            if (LastFacingDirection == FacingDirection.Right)
            {
                m_PlayerVelocity.x = DashSpeed;
            }
            else
            {
                m_PlayerVelocity.x = -DashSpeed;
            }
            m_PlayerVelocity.y = 0;
        } 
    }
    void StartDash()
    {
        Invoke("EnableDash", DashCooldown);
        Invoke("ResetSpeedLimit", DashDuration);
        isDashing = true;
        CanDash = false;

    }

    void EnableDash()
    {
        CanDash = true;
    }
    void ResetSpeedLimit()
    {
        isDashing = false;
    }
    //************** JUMP ************** JUMP ************* JUMP *********** JUMP ********* JUMP ************** JUMP ********** JUMP ********* JUMP *************** JUMP ********************

    void GroundIt()
    {
        if (IsGrounded())
        {
            m_PlayerVelocity.y = 0;
        }
    }


    void Jump()
    {
        // preform jump
        m_PlayerVelocity.y = 2 * m_jumpApexHeight / m_jumpApexTime;
        // prevent coyote jump follow up after regular jump
        m_CoyoteCountdown = -1;
        // Prevent Buffer Jump
        isBufferJumpReady = false;
        m_bufferJumpCountdown = -1;
    }
    void RegularJump()
    {
        if (IsGrounded())
        {
            // reset Gravitational Acceleration for low jump when grounded
            m_PlayerAccel.y = -2 * m_jumpApexHeight / (m_jumpApexTime * m_jumpApexTime);
            if (InputTracker.IsJumpPressed())
            {
                Jump();
            }

        }
    }
    void ResetSnakeSpeed()
    {
        m_maxHorizontalSpeed = 7;
    }


    void BufferJump()
    {
        if (IsGrounded())
        {
            // Use quequed up jump and reset it
            if (isBufferJumpReady)
            {
                Jump();
                print("BufferJump");
            }
        }
        else
        {
            // count down timer
            m_bufferJumpCountdown = m_bufferJumpCountdown - Time.deltaTime;

            // Remove queued up jump if buffer time exceeded
            if (m_bufferJumpCountdown < 0)
            {
                isBufferJumpReady = false;
            }


            // Queue up jump when jump key was pressed while not grounded
            if (InputTracker.WasJumpPressed())
            {
                m_bufferJumpCountdown = m_jumpBufferTime;
                isBufferJumpReady = true;
            }
        }

    }



    void TerminalVelocity()
    {
        if (m_PlayerVelocity.y < m_terminalVelocity)
        {
            m_PlayerVelocity.y = m_terminalVelocity;
        }
    }




    //  ************** Damage ************** Damage ************** Damage ************** Damage ************** Damage ************** Damage ************** Damage ************** Damage ***********

    public void addDamage(float damage)
    {
        if (damage <= 0)
        {
            return;
        }
        damage = damage - (damage * m_damageReduction / 100);
        m_CurrentHealth -= damage;


        if (m_CurrentHealth <= 0)
        {
            makeDead();
        }

    }

    public void makeDead()
    {
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position - new Vector3(0, GroundedCastDistance, 0), BoxSize);

        Gizmos.color = Color.white;
    }
}
