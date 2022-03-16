using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static GameObject player;
    public static Inventory inventory;
    public static Interactor interactor;

    public bool ControlsEnableled = true;
    public bool EnableDashControls = true;
    public bool EnableJumpControls = true;


    [SerializeField]
    private float m_jumpApexHeight = 3f;

    [SerializeField]
    private float m_jumpApexTime = .133f;

    [SerializeField]
    private float m_terminalVelocity = -20f;

    [SerializeField]
    private float m_jumpBufferTime = 0.2f;

    [SerializeField]
    private float CoyoteTime = 0.2f;

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
    public bool GroundValue;
    public SpriteRenderer PlayerSprite;

    bool CanDash = true;
    public bool isDashing = false;

    public enum FacingDirection { Left, Right }
    bool isQuickTurning;
    //bool isBufferJumpReady = true;
    bool isBufferJumpQueued;
    float m_bufferJumpCountdown;
    float m_CoyoteCountdown;
    public bool canCoyoteJump = true;

    Rigidbody2D m_PlayerRigidBody;
    public Vector2 m_PlayerVelocity = Vector2.zero;
    public Vector2 m_PlayerAccel = Vector2.zero;
    FacingDirection LastFacingDirection = FacingDirection.Right;
    LayerMask GroundLayer = 1 << 8;



    // health
    public static Player instance;

    public float m_FullHealth = 10;
    public float m_CurrentHealth = 10;
    public float RegenCooldown = 6;
    float RegenCountdown = 0;
    float m_damageReduction = 0;

    public GameObject jumpVFX;
    public GameObject dashVFX;
    public GameObject landVFX;
    private bool groundedLastFrame;

    private Animator an;

    public GameObject LastCheckpoint;
    public GameObject DamageVFX;
    public GameObject FeetVFX;

    private void Awake()
    {
        instance = this;

        player = gameObject;
        inventory = GetComponentInChildren<Inventory>();
        interactor = GetComponentInChildren<Interactor>();
        an = GetComponent<Animator>();
    }


    private void Start()
    {
        m_PlayerRigidBody = GetComponent<Rigidbody2D>();
        m_PlayerAccel.y = -3;
        m_CurrentHealth = m_FullHealth;
    }
    private void Update()
    {
        if (ControlsEnableled)
        {
            HandleHorizontalMovement(); ;
        }

        // Vertical Movement    
        GroundIt();
        m_PlayerVelocity.y = m_PlayerVelocity.y + (m_PlayerAccel.y * Time.deltaTime);

        if (ControlsEnableled)
        {
            RegularJump();
            BufferJump();
            //CoyoteJump();
            HandleDash();
            LastFacingDirection = GetFacingDirection();
        }
        else
        {
            m_PlayerVelocity.x = 0;
            m_PlayerVelocity.y = -5;
        }

        // The following function ensures that the knight does not exceed TERMINAL VELOCITY
        TerminalVelocity();

        GroundIt();
        GroundValue = IsGrounded();
        IsBlockedRight();
        IsBlockedLeft();
        IsBlockedUp();

        m_PlayerRigidBody.velocity = m_PlayerVelocity;

        RegenHealth();

        if (Input.GetKeyDown("k"))
        {
            if (ControlsEnableled)
            {
                DisableControls();
            }
            else
            {
                EnableControls();
            }
        }

        an.SetBool("IsGrounded", IsGrounded());
        an.SetBool("IsRunning", Mathf.Abs(m_PlayerVelocity.x) > 0);
    }

    public void LateUpdate()
    {
        if (!groundedLastFrame)
        {
            if (IsGrounded())
            {
                Instantiate(landVFX, transform);
            }
        }

        groundedLastFrame = IsGrounded();
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
        if (Physics2D.BoxCast(transform.position - new Vector3 (0, (BoxSize.y + 0.001f) /2, 0 ), BoxSize, 0f, Vector2.down, GroundedCastDistance, GroundLayer))
        {
            return true;

        }
        else
        {
            return false;
        }
    }

    void IsBlockedRight()
    {
        if (Physics2D.BoxCast(transform.position + new Vector3( (BoxSize.x + 0.001f) / 2, 0, 0), BoxSize, 0f, Vector2.right, GroundedCastDistance, GroundLayer))
        {
            if (m_PlayerVelocity.x > 0)
            {
                m_PlayerVelocity.x = 0;
                print("Blocked Right");
            }
        }
        else
        {

        }
    }

    void IsBlockedLeft()
    {
        if (Physics2D.BoxCast(transform.position - new Vector3( (BoxSize.x + 0.001f) / 2, 0, 0), BoxSize, 0f, Vector2.left, GroundedCastDistance, GroundLayer))
        {
            if (m_PlayerVelocity.x < 0)
            {
                m_PlayerVelocity.x = 0;
                print("Blocked Left");
            }
        }
        else
        {

        }
    }
    void IsBlockedUp()
    {
        if (Physics2D.BoxCast(transform.position + new Vector3(0, (BoxSize.y + 0.001f) / 2, 0), BoxSize, 0f, Vector2.up, GroundedCastDistance, GroundLayer))
        {
            if (m_PlayerVelocity.y > 0)
            {
               m_PlayerVelocity.y = 0;
                print("Blocked Up");
            }
        }
        else
        {

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
        if (( Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown("joystick button 1")) && CanDash)
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
        FeetVFX.gameObject.SetActive(false);
        isDashing = true;
        CanDash = false;
        Instantiate(dashVFX, transform);
    }

    void EnableDash()
    {
        CanDash = true;
        FeetVFX.gameObject.SetActive(true);
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
            if (m_PlayerVelocity.y < 0)
            {
                m_PlayerVelocity.y = 0;
            }
        }
    }


    void Jump()
    {
        an.SetTrigger("Jump");
        Instantiate(jumpVFX, transform);
        // preform jump
        m_PlayerVelocity.y = 2 * m_jumpApexHeight / m_jumpApexTime;
        // prevent coyote jump follow up after regular jump
        m_CoyoteCountdown = -1;
        canCoyoteJump = false;
        Invoke("EnableCoyoteJump", CoyoteTime);
        // Prevent Buffer Jump
        isBufferJumpQueued = false;
        m_bufferJumpCountdown = -1;
    }
    void RegularJump()
    {
        if (IsGrounded())
        {
            // reset Gravitational Acceleration for low jump when grounded
            m_PlayerAccel.y = -2 * m_jumpApexHeight / (m_jumpApexTime * m_jumpApexTime);
            if (InputTracker.WasJumpPressed())
            {
                Jump();
            }

        }
    }

    void BufferJump()
    {
        if (IsGrounded())
        {
            // Use quequed up jump and reset it
            if (isBufferJumpQueued)
            {
                Jump();
            }

        }
        else
        {
            // count down timer
            m_bufferJumpCountdown = m_bufferJumpCountdown - Time.deltaTime;

            // Remove queued up jump if buffer time exceeded
            if (m_bufferJumpCountdown < 0)
            {
                isBufferJumpQueued = false;
            }


            // Queue up jump when jump key was pressed while not grounded
            if (InputTracker.WasJumpPressed())
            {
                m_bufferJumpCountdown = m_jumpBufferTime;
                isBufferJumpQueued = true;
            }
        }

    }

    void CoyoteJump()
    {
        if (IsGrounded())
        {
            // Reset countdown when grounded
            m_CoyoteCountdown = CoyoteTime;
        }
        else
        {
            // Jump if you still have time left in your count down, and key is pressed
            if (InputTracker.WasJumpPressed() && (m_CoyoteCountdown > 0))
            {
                if (canCoyoteJump)
                {
                    Jump();
                }
            }
            else
            {
                //  keep Counting down 
                m_CoyoteCountdown = m_CoyoteCountdown - Time.deltaTime;
            }

        }

    }

    void EnableCoyoteJump()
    {
        canCoyoteJump = true;
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
        GlobalVolumeController.Singleton.CameraFadeDark(m_CurrentHealth, m_FullHealth);
        // Reset time left before regen
        RegenCountdown = RegenCooldown;

        if (m_CurrentHealth <= 0)
        {
            makeDead();
        }

    }

    void RegenHealth()
    {

        if (m_CurrentHealth < m_FullHealth)
        {
            if(RegenCountdown <= 0)
            {
                m_CurrentHealth = m_FullHealth;
                // reset cooldown
                RegenCountdown = RegenCooldown;
                // Reset Dark fade
                GlobalVolumeController.Singleton.CameraFadeDark(m_CurrentHealth, m_FullHealth);
            }
            else
            {
                RegenCountdown = -Time.deltaTime;
            }
        }
        else
        {
            RegenCountdown = RegenCooldown;
        }
    }


    public void makeDead()
    {
        // reset darkness
        GlobalVolumeController.Singleton.ResetCameraFade();
        // reset health 
        m_CurrentHealth = m_maxHorizontalSpeed;
        // Show VFX
        Instantiate(DamageVFX, transform.position + new Vector3(0,0, -2), Quaternion.identity);

        if (LastCheckpoint != null)
        {
            m_PlayerVelocity = Vector3.zero;
            transform.position = LastCheckpoint.transform.position;
        }
        else
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
        

    }


    public void EnableControls()
    {
        ControlsEnableled = true;
    }
    public void DisableControls()
    {
        ControlsEnableled = false;
    }

    //  ************* Gizmos ************* Gizmos ************* Gizmos ************* Gizmos ************* Gizmos ************* Gizmos ************* Gizmos ************* Gizmos ************* Gizmos

    public void OnDrawGizmosSelected()
    {
        
        Gizmos.color = Color.cyan;
        //Gizmos.DrawWireCube(transform.position - new Vector3(0, GroundedCastDistance, 0), BoxSize);
        Gizmos.DrawWireCube(transform.position - new Vector3(0, 0, 0), BoxSize);

        Gizmos.color = Color.red;
        //Gizmos.DrawWireCube(transform.position + new Vector3(0, GroundedCastDistance, 0), BoxSize);
        Gizmos.DrawWireCube(transform.position + new Vector3(0, 0, 0), BoxSize);

        Gizmos.color = Color.yellow;
        //Gizmos.DrawWireCube(transform.position - new Vector3(GroundedCastDistance, 0, 0), BoxSize);
        Gizmos.DrawWireCube(transform.position - new Vector3(0, 0, 0), BoxSize);

        Gizmos.color = Color.black;
        //Gizmos.DrawWireCube(transform.position + new Vector3(GroundedCastDistance, 0, 0), BoxSize);
        Gizmos.DrawWireCube(transform.position + new Vector3(0, 0, 0), BoxSize);

        Gizmos.color = Color.white;
    }

    public void ForceIdleAnimation(bool force = true)
    {
        an.SetBool("ForceIdle", force);
    }
}
