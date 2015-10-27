using UnityEngine;
using System.Collections;

public class SideColliderController : MonoBehaviour {

    public GameObject parentGameObject;

    void OnTriggerEnter2D(Collider2D other)
    {
        parentGameObject.SendMessage("OnSideCollides", other);
    }

    // void OnTriggerExit2D(Collider2D other)
    // {
    //     parentGameObject.SendMessage("OnSideStopsColliding", other);
    // }
}
