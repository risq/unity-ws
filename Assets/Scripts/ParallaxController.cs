using UnityEngine;
using System.Collections;

public class ParallaxController : MonoBehaviour
{

    public Vector3 parallaxFactor = (Vector2)new Vector2(0.5f, 0.5f);
    public Camera camera;
    private float oldPosition;

    private Vector3 basePos;
    private Vector3 basePlanePos;

    // Use this for initialization
    void Start()
    {
        oldPosition = camera.transform.position.x;
        basePos = camera.transform.position;
        basePlanePos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (camera.transform.position.x != oldPosition)
        {

            Vector3 delta = basePos - camera.transform.position;

            translate(delta);

            oldPosition = camera.transform.position.x;
        }
    }

    void translate(Vector3 delta)
    {

        Vector3 newPos = transform.position;
        delta.Scale(parallaxFactor);
        newPos = basePlanePos - delta;

        transform.position = newPos;
    }
}