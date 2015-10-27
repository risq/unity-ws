using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public GameObject characterSpriteObject;

    bool isGrounded = false;
    bool isJumping = false;
    bool isMoving = false;
    bool isFalling = false;
    string direction = "right";
    float currentMaxJumpHeight = 0;

    Animator _animator;
    Rigidbody2D _rigidBody2D;

    const float MOVE_SPEED = 200;
    const float JUMP_SPEED = 150;
    const float MAX_JUMP_HEIGHT = 2f;

    // Use this for initialization
    void Start ()
    {
        _animator = characterSpriteObject.GetComponent<Animator>();
        _rigidBody2D = GetComponent<Rigidbody2D>();
        IsGrounded = true;
    }
	
	// Update is called once per frame
	void Update ()
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
            IsMoving = true;
            Direction = "right";
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            IsMoving = true;
            Direction = "left";
        }
        else
        {
            IsMoving = false;
        }
    }

    void FixedUpdate()
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
                Debug.Log(currentMaxJumpHeight);
                _rigidBody2D.velocity = new Vector2(_rigidBody2D.velocity.x, JUMP_SPEED * (currentMaxJumpHeight / transform.position.y) * Time.fixedDeltaTime);
            }
        }

        if (IsFalling)
        {
            _rigidBody2D.velocity = new Vector2(_rigidBody2D.velocity.x, _rigidBody2D.velocity.y - 0.1f);
            Debug.Log(_rigidBody2D.velocity.y);
        }

        if (IsMoving)
        {
            if (direction == "left")
            {
                _rigidBody2D.velocity = new Vector2(-MOVE_SPEED * Time.fixedDeltaTime, _rigidBody2D.velocity.y);
            }
            else if (direction == "right")
            {
                _rigidBody2D.velocity = new Vector2(MOVE_SPEED * Time.fixedDeltaTime, _rigidBody2D.velocity.y);
            }
        }
        else
        {
            _rigidBody2D.velocity = new Vector2(_rigidBody2D.velocity.x * 0.8f, _rigidBody2D.velocity.y);
        }
    }

    void OnFootCollides(Collider2D other)
    {
        IsGrounded = true;
        IsFalling = false;
    }

    void OnFootStopsColliding(Collider2D other)
    {
        IsGrounded = false;
    }

    void OnSideCollides()
    {
        IsGrounded = true;
        IsFalling = false;
    }

    public string Direction
    {
        get
        {
            return direction;
        }
        set
        {
            if (direction != value)
            {
                if (direction == "left")
                {
                    direction = value;
                    characterSpriteObject.transform.Rotate(0, 180, 0);
                }
                else if (direction == "right")
                {
                    direction = value;
                    characterSpriteObject.transform.Rotate(0, -180, 0);
                }
            }
        }
    }

    public bool IsMoving
    {
        get
        {
            return isMoving;
        }
        set
        {
            if (isMoving != value)
            {
                isMoving = value;
                _animator.SetBool("isMoving", value);
            }
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

                if (isJumping)
                {
                    currentMaxJumpHeight = transform.position.y + MAX_JUMP_HEIGHT;
                }
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
