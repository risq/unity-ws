using UnityEngine;
using System.Collections;

public class FootColliderController : MonoBehaviour
{

    public GameObject parentGameObject;

    void OnTriggerStay2D(Collider2D other)
    {
        parentGameObject.SendMessage("OnFootCollides", other);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        parentGameObject.SendMessage("OnFootStopsColliding", other);
    }
}
