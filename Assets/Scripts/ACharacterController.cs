using UnityEngine;
using System.Collections;

public class ACharacterController : MonoBehaviour {

    public GameObject spriteObject;
    public float directionChangeOffsetX = 0;

    bool isMoving = false;
    bool isDead = false;

    string direction = "right";

    public float moveSpeed = 10;
    public float maxSpeed = 2;

    float currentSpeed = 0;

    [SerializeField]
    int life = 1;

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

    virtual protected void UpdateMovement()
    {
        Vector2 velocity = _rigidBody2D.velocity;
        if (IsMoving)
        {
            if (Direction == "left")
            {
                velocity.x -= moveSpeed * Time.fixedDeltaTime;
                velocity.x = Mathf.Max(velocity.x, -maxSpeed);
            }
            else if (Direction == "right")
            {
                velocity.x += moveSpeed * Time.fixedDeltaTime;
                velocity.x = Mathf.Min(velocity.x, maxSpeed);
            }
        }
        else
        {
            velocity.x *= 0.9f;
        }

        CurrentSpeed = Mathf.Abs(velocity.x);
        _rigidBody2D.velocity = velocity;
    }

    protected void Damage(int amount)
    {
        Life -= amount;
    }

    protected void MoveLeft()
    {
        IsMoving = true;
        Direction = "left";
    }

    protected void MoveRight()
    {
        IsMoving = true;
        Direction = "right";
    }

    protected void ChangeDirection()
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

    protected void StopMoving()
    {
        IsMoving = false;
    }

    virtual protected void Die()
    {
        IsDead = true;
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
                    spriteObject.transform.Translate(directionChangeOffsetX, 0, 0);
                }
                else if (direction == "right")
                {
                    direction = value;
                    spriteObject.transform.Rotate(0, -180, 0);
                    spriteObject.transform.Translate(directionChangeOffsetX, 0, 0);
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
                _animator.SetTrigger("die");
            }
        }
    }

    public float CurrentSpeed
    {
        get
        {
            return currentSpeed;
        }
        set
        {
            if (currentSpeed != value)
            {
                currentSpeed = value;
                _animator.SetFloat("currentSpeed", currentSpeed);
            }
        }
    }

    public int Life
    {
        get
        {
            return life;
        }
        set
        {
            if (value <= 0)
            {
                life = 0;
                Die();
            }
            else
                life = value;
         
        }
    }
}
