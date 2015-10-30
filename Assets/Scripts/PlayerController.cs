using UnityEngine;
using System.Collections;

public class PlayerController : ACharacterController
{

    bool isGrounded = false;
    bool isJumping = false;
    bool isFalling = false;
    bool isHurt = false;

    const float HURT_TIME = 0.5f;
    
    float currentMaxJumpHeight = 0;

    public bool isCollidingEnvironement = false;
    public string environementCollisionDirection;

    public float jumpSpeed = 150;
    public float maxJumpHeight = 2f;

    public Canvas scoreCanvas;
    private ScoreController scoreController;

    override public void Start()
    {
        base.Start();
        IsGrounded = true;
        scoreController = scoreCanvas.GetComponent<ScoreController>();
    }
	
	override public void Update ()
    {
        UpdateKeyInputs();
    }

    override public void FixedUpdate()
    {
        if (!isHurt)
        {
            base.FixedUpdate();
            UpdateJumpMovement();
        }
    }

    void UpdateKeyInputs()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded)
        {
            IsJumping = true;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            IsJumping = false;
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

    public void Hurt(ContactPoint2D contact)
    {
        if (!isHurt)
        {
            StartCoroutine(WaitHurt());
            _rigidBody2D.velocity = new Vector3(-3 * contact.normal.x, 3);
            if (!IsDead)
            {
                Damage(1);
            }
        }
        
    }

    IEnumerator WaitHurt()
    {
        IsHurt = true;
        yield return new WaitForSeconds(HURT_TIME);
        IsHurt = false;
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

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Jewel")
        {
            scoreController.AddPoint();
            Destroy(collider.gameObject);
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

    public bool IsHurt
    {
        get
        {
            return isHurt;
        }
        set
        {
            if (isHurt != value)
            {
                isHurt = value;
                _animator.SetBool("isHurt", value);
            }
        }
    }
}
