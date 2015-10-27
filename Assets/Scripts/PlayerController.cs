using UnityEngine;
using System.Collections;

public class PlayerController : ACharacterController
{
    bool isGrounded = false;
    bool isJumping = false;
    bool isFalling = false;
    
    float currentMaxJumpHeight = 0;

    public float jumpSpeed = 150;
    public float maxJumpHeight = 2f;

    override public void Start()
    {
        base.Start();
        IsGrounded = true;
    }
	
	override public void Update ()
    {
        base.Update();
        UpdateKeyInputs();
    }

    override public void FixedUpdate()
    {
        base.FixedUpdate();
        UpdateJumpMovement();
    }

    void UpdateKeyInputs()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded)
        {
            StartJumping();
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            StopJumping();
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            MoveRight();
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        else
        {
            StopMoving();
        }
    }

    void UpdateJumpMovement()
    {
        if (IsJumping)
        {
            if (transform.position.y >= currentMaxJumpHeight)
            {
                IsFalling = true;
                IsJumping = false;
                _rigidBody2D.velocity = new Vector2(_rigidBody2D.velocity.x, 0);
            }
            else
            {
                _rigidBody2D.velocity = new Vector2(_rigidBody2D.velocity.x, jumpSpeed * (currentMaxJumpHeight / transform.position.y) * Time.fixedDeltaTime);
            }
        }

        if (IsFalling)
        {
            _rigidBody2D.velocity = new Vector2(_rigidBody2D.velocity.x, _rigidBody2D.velocity.y - 0.1f);
        }
    }

    void StartJumping()
    {
        IsJumping = true;
    }

    void StopJumping()
    {
        IsJumping = false;
    }

    void OnSideCollides()
    {
        
    }

    void OnFootCollides(Collider2D other)
    {
        if (other.gameObject.tag == "Environement")
        {
            IsGrounded = true;
        }
    }

    void OnFootStopsColliding(Collider2D other)
    {
        if (other.gameObject.tag == "Environement")
        {
            IsGrounded = false;
        }
    }

    public bool IsGrounded
    {
        get
        {
            return isGrounded;
        }
        set
        {
            if (isGrounded != value)
            {
                isGrounded = value;
                _animator.SetBool("isGrounded", value);
 
                // Stop falling if setting IsGrounded to true
                if (isGrounded)
                {
                    IsFalling = false;
                }
            }
        }
    }

    public bool IsJumping
    {
        get
        {
            return isJumping;
        }
        set
        {
            if (isJumping != value)
            {
                isJumping = value;
                _animator.SetBool("isJumping", value);

                // Update max jump height from current position if setting IsJumping to true
                if (isJumping)
                {
                    currentMaxJumpHeight = transform.position.y + maxJumpHeight;
                }
                // If setting IsJumping to false and character is not grounded, character is falling
                else if (!IsGrounded)
                {
                    IsFalling = true;
                    _rigidBody2D.velocity = new Vector2(_rigidBody2D.velocity.x, _rigidBody2D.velocity.y / 2);
                }
            }
        }
    }

    public bool IsFalling
    {
        get
        {
            return isFalling;
        }
        set
        {
            if (isFalling != value)
            {
                isFalling = value;
                _animator.SetBool("isFalling", value);
            }
        }
    }
}
