using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public bool spawn = true;

    public GameObject[] enemyPrefab;

	// Use this for initialization
	void Start () {
        StartCoroutine(EndlessSpawing());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator EndlessSpawing()
    {
        while (spawn)
        {
            yield return new WaitForSeconds(2);
            if (enemyPrefab.Length > 0)
                Instantiate(enemyPrefab[Random.Range(0,enemyPrefab.Length)], transform.position, Quaternion.Euler(Vector3.zero));
            else
                Instantiate(enemyPrefab[0], transform.position, Quaternion.Euler(Vector3.zero));
        }
    }
}