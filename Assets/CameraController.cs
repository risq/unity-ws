using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject playerObject;
    public float smooth = 1.5f;
    public float minHeight = 0;
    public float maxHeight = 1;

    private Transform player;
    private Vector3 relCameraPos;
    private float relCameraPosMag;
    private Vector3 newPos;

    void Awake () {
        player = playerObject.transform;

        relCameraPos = transform.position - player.position;
        relCameraPosMag = relCameraPos.magnitude - 0.5f;
    }

    void FixedUpdate()
    {
        Vector3 standardPos = player.position + relCameraPos;
        Vector3 abovePos = player.position + Vector3.up * relCameraPosMag;
        Vector3[] checkPoints = new Vector3[5];

        checkPoints[0] = standardPos;
        checkPoints[1] = Vector3.Lerp(standardPos, abovePos, 0.2f);
        checkPoints[2] = Vector3.Lerp(standardPos, abovePos, 0.5f);
        checkPoints[3] = Vector3.Lerp(standardPos, abovePos, 0.7f);
        checkPoints[4] = abovePos;

        for (int i = 0; i < checkPoints.Length; i++)
        {
            if (ViewingPosCheck(checkPoints[i]))
                break;
        }

        newPos.y = Mathf.Min(newPos.y, maxHeight);

        transform.position = Vector3.Lerp(transform.position, newPos, smooth * Time.deltaTime);
    }


    bool ViewingPosCheck(Vector3 checkPos)
    {
        RaycastHit hit;

        if (Physics.Raycast(checkPos, player.position - checkPos, out hit, relCameraPosMag))
            if (hit.transform != player)
                return false;

        newPos = checkPos;
        return true;
    }

}
