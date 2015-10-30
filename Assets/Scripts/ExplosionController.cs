using UnityEngine;
using System.Collections;

public class ExplosionController : MonoBehaviour {

    bool hasExploded = false;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ennemy" && 
            other.gameObject.GetComponent<MonsterController>() != null && 
            this.GetComponentInParent<MonsterController>().IsDead)
        {
            other.gameObject.GetComponent<MonsterController>().PublicDie();
        }
    }
}
