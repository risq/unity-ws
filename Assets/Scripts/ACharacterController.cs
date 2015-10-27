using UnityEngine;
using System.Collections;

public class ACharacterController : MonoBehaviour {

    public GameObject spriteObject;

    bool isMoving = false;
    bool isDead = false;

    string direction = "right";

    public float moveSpeed = 200;

    protected Animator _animator;
    protected Rigidbody2D _rigidBody2D;

    virtual public void Start ()
    {
        _animator = spriteObject.GetComponent<Animator>();
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    virtual public void Update ()
    {
	
	}

    virtual public void FixedUpdate ()
    {
        UpdateMovement();
    }

    void UpdateMovement()
    {
        if (IsMoving)
        {
            if (direction == "left")
            {
                _rigidBody2D.velocity = new Vector2(-moveSpeed * Time.fixedDeltaTime, _rigidBody2D.velocity.y);
            }
            else if (direction == "right")
            {
                _rigidBody2D.velocity = new Vector2(moveSpeed * Time.fixedDeltaTime, _rigidBody2D.velocity.y);
            }
        }
        else
        {
            _rigidBody2D.velocity = new Vector2(_rigidBody2D.velocity.x * 0.8f, _rigidBody2D.velocity.y);
        }
    }

    public void MoveLeft()
    {
        IsMoving = true;
        Direction = "left";
    }

    public void MoveRight()
    {
        IsMoving = true;
        Direction = "right";
    }

    public void ChangeDirection()
    {
        if (Direction == "left")
        {
            MoveRight();
        }
        else if (Direction == "right")
        {
            MoveLeft();
        }
    }

    public void StopMoving()
    {
        IsMoving = false;
    }

    public void Die()
    {
        IsDead = true;
        Destroy(gameObject, 0.5f);
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
                    spriteObject.transform.Rotate(0, 180, 0);
                }
                else if (direction == "right")
                {
                    direction = value;
                    spriteObject.transform.Rotate(0, -180, 0);
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

    public bool IsDead
    {
        get
        {
            return isDead;
        }
        set
        {
            if (isDead != value)
            {
                isDead = value;
                _animator.SetBool("isDead", value);
            }
        }
    }
}
