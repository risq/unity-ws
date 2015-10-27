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

    public void OnSideCollides()
    {
        ChangeDirection();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y > 0)
        {
            Die();
        }
    }

}
