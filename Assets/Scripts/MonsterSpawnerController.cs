using UnityEngine;
using System.Collections;

public class MonsterSpawnerController : MonoBehaviour {

    public GameObject monsterPrefab;

	// Use this for initialization
	void Start () {
        StartCoroutine(WaitSpawnMonster());
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator WaitSpawnMonster()
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(monsterPrefab, transform.position, transform.rotation);
        StartCoroutine(WaitSpawnMonster());
    }
}
