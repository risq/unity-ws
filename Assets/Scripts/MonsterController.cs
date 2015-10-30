using UnityEngine;
using System.Collections;

public class MonsterController : ACharacterController {

    public ParticleSystem dieParticleSystem;
    public GameObject explosion;

    private PointEffector2D explosionEffector;

    public AudioClip dieAudio;
    AudioSource source;

    public void Awake()
    {
        dieParticleSystem.Stop();
        explosionEffector = explosion.GetComponent<PointEffector2D>();
        explosionEffector.enabled = false;

        source = GetComponent<AudioSource>();
    }

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
        {
            _rigidBody2D.velocity = new Vector2(Random.Range(-4, 4), Random.Range(0, 2));
            ChangeDirection();
        }
            

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
                    //collision.rigidbody.velocity = new Vector2(collision.rigidbody.velocity.x, 4);
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

    public void PublicDie()
    {

        Die();
    }

    protected override void Die()
    {
        if (!IsDead)
        {
            base.Die();
            if (source.isPlaying)
                source.Stop();
            source.PlayOneShot(dieAudio);
            explosionEffector.enabled = true;
            dieParticleSystem.Play();
            Destroy(gameObject, 0.5f);
        }
    }
}

