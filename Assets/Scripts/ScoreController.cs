using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreController : MonoBehaviour {

    public Text textObject;
    int points = 0;

	public void AddPoint()
    {
        points++;
        textObject.text = points.ToString();
    }
}
