using UnityEngine;
using System.Collections;

public class MonsterController : ACharacterController {

    override public void Start ()
    {
        base.Start();
        StartAutoMove();
    }

    void StartAutoMove()
    {
        MoveLeft();
    }

    public void OnSideCollides(Collider2D collider)
    {
        if (collider.gameObject.tag == "Ennemy")
            _rigidBody2D.AddForce(new Vector2(Random.Range(-40, 40), Random.Range(100, 200)));

        else if (collider.gameObject.tag == "Environement")
            ChangeDirection();            
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            if (collision.relativeVelocity.y > 0 && Mathf.Abs(collision.contacts[0].normal.x) < 0.9)
            {
                if (!IsDead)
                {
                    collision.rigidbody.velocity = new Vector2(collision.rigidbody.velocity.x, 4);
                    Damage(1);
                }
                
            }
            else
            {
                ChangeDirection();
                collision.gameObject.SendMessage("Hurt", collision.contacts[0]);
            }
        }
    }

    protected override void Die()
    {
        base.Die();
        Destroy(gameObject, 0.5f);
    }
}

