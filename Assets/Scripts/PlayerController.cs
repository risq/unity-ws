using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public GameObject characterSpriteObject;

    enum STATE { STATE_IDLE, STATE_WALKING, STATE_JUMPING, STATE_CROUCHING };
    int currentState;
    bool isGrounded = false;
    bool isJumping = false;
    bool isMoving = false;
    bool isFalling = false;
    string direction = "right";

    Animator _animator;
    Rigidbody2D _rigidBody2D;

    const float MOVE_SPEED = 200;
    const float JUMP_SPEED = 3000;
    const float MAX_JUMP_HEIGHT = 0.1f;

    // Use this for initialization
    void Start ()
    {
        _animator = characterSpriteObject.GetComponent<Animator>();
        _rigidBody2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.UpArrow))
            IsJumping = true;
        else
            IsJumping = false;
      
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
        if (IsFalling && _rigidBody2D.velocity.y < 0)
        {
            _rigidBody2D.velocity = new Vector2(_rigidBody2D.velocity.x, _rigidBody2D.velocity.y / 2);
        }
        else if (IsJumping)
        {
            if (IsGrounded)
                IsGrounded = false;

            Debug.Log(transform.position.y);

            if (transform.position.y < MAX_JUMP_HEIGHT) 
                _rigidBody2D.AddForce(new Vector2(0, JUMP_SPEED * Time.fixedDeltaTime));
            else
            {
                IsFalling = true;
            }
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
            _rigidBody2D.velocity = new Vector2(0, _rigidBody2D.velocity.y);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        IsGrounded = true;
        IsFalling = false;
        //Debug.Log("Collision");
    }

    public int CurrentState
    {
        get
        {
            return currentState;
        }
        set
        {
            if (currentState != value)
            {
                currentState = value;
                _animator.SetInteger("state", value);
            }
        }
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
