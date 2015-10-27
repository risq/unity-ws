using UnityEngine;
using System.Collections;

public class FootColliderController : MonoBehaviour
{

    public GameObject player;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        player.SendMessage("OnFootCollides", other);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        player.SendMessage("OnFootStopsColliding", other);
    }
}
