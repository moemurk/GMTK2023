using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("IWanna")]
    public float runSpeed;
    public float jumpForce;
    public float movementSmoothing;
    public bool airControl;
    public Transform groundCheck;
    public LayerMask groundLayer;

    public bool canDoubleJump;
    [Header("Isaac")]
    public float moveSpeed_Isaac;
    [Header("Other")]
    public float fireCoolTime;
    public GameObject bulletPrefabs;
    public Transform shootingPoint;
    /****************/
    private bool canMove = true;
    private float horizontalMove = 0f;
    private float verticalMove = 0f;
    private bool jumpInput = false;
    private bool onGround = true;
    private Vector3 velocity = Vector3.zero;
    private bool facingRight = true;
    private bool alreadyDoubleJump = false;
    public Teleport nowTeleport;
    private bool canBeTeleport = true;
    private float coolTime = 0;
    private Vector3 facingDir = Vector3.right;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coolTime -= Time.deltaTime;
        if (coolTime < 0f ) {
            coolTime = 0f;
        }
        GetInput();
    }

    void FixedUpdate()
    {
        StateName gameState = GameStateManager.Instance.GetState();
        switch(gameState) {
            case StateName.IWanna:
                ControlByIWanna();
                break;
            case StateName.Isaac:
                ControlByIsaac();
                break;
            default:
                break;
        }
    }
    
    private void ControlByIWanna()
    {
        Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
        // on ground detected
        bool wasOnGround = onGround;
        onGround = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.2f, groundLayer);
        if (colliders.Length > 0) {
            onGround = true;
            alreadyDoubleJump = false;
            if (!wasOnGround) {
                // land
            }
        }
        // movement
        if (onGround || airControl) {
            float moveDelta = horizontalMove * runSpeed * Time.fixedDeltaTime * 10f;
            Vector3 targetVelociry = new Vector2(moveDelta, rigidbody2D.velocity.y);
            rigidbody2D.velocity = Vector3.SmoothDamp(rigidbody2D.velocity, targetVelociry, ref velocity, movementSmoothing, Mathf.Infinity, Time.fixedDeltaTime);
            if (moveDelta > 0 && !facingRight) {
                // turn to right
                Flip();
                facingDir = Vector3.right;
            } else if (moveDelta < 0 && facingRight) {
                // turn to left
                Flip();
                facingDir = Vector3.left;
            }
        }
        // jump
        if (jumpInput) {
            jumpInput = false;
            if (onGround) {
                onGround = false;
                rigidbody2D.AddForce(new Vector2(0f, jumpForce));
            } else if (!alreadyDoubleJump && canDoubleJump) {
                alreadyDoubleJump = true;
                rigidbody2D.AddForce(new Vector2(0f, jumpForce));
            }
        }
        
    }

    private void ControlByIsaac()
    {
        Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
        Vector2 moveDelta = new Vector2(horizontalMove, verticalMove).normalized * moveSpeed_Isaac * Time.fixedDeltaTime * 10f;

        
        Vector3 targetVelociry = moveDelta;
        float xDelta = Vector3.Dot(targetVelociry, Vector3.right);
        float yDelta = Vector3.Dot(targetVelociry, Vector3.up);
        rigidbody2D.velocity = Vector3.SmoothDamp(rigidbody2D.velocity, targetVelociry, ref velocity, movementSmoothing, Mathf.Infinity, Time.fixedDeltaTime);
        // turn direction
        if (horizontalMove == 0 && verticalMove == 0) {
            return;
        }
        if (Mathf.Abs(xDelta) >= Mathf.Abs(yDelta)) {
            // x direction
            if (xDelta >= 0) {
                facingDir = Vector3.right;
            } else {
                facingDir = Vector3.left;
            }
        } else {
            // y direction
            if (yDelta >= 0) {
                facingDir = Vector3.up;
            } else {
                facingDir = Vector3.down;
            }
        }
    }

    private void GetInput()
    {
        if (!canMove) {
            horizontalMove = 0;
            verticalMove = 0;
            jumpInput = false;
            return;
        }
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("Jump")) {
            jumpInput = true;
        }
        if (Input.GetButtonDown("Fire1")) {
            Fire();
        }
    }

    private void Fire()
    {
        if (coolTime > 0) {
            return;
        }
        coolTime = fireCoolTime;
        GameObject bullet = Instantiate(bulletPrefabs, shootingPoint);
        bullet.transform.SetParent(null);
        StateName gameState = GameStateManager.Instance.GetState();
        if (gameState == StateName.Isaac) {
            bullet.GetComponent<Bullet>().direction = facingDir;
        } else {
            bullet.GetComponent<Bullet>().direction = facingRight ? Vector3.right : Vector3.left;
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
    }

    public void SwitchGravity(bool on) {
        Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
        if (on) {
            rigidbody2D.gravityScale = 3.0f;
        } else {
            rigidbody2D.gravityScale = 0f;
        }
    }

    public bool CanBeTeleport()
    {
        return canBeTeleport;
    }

    public void SetMoveState(bool can)
    {
        canMove = can;
    }
}
