using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public bool spawn = true;

    public static int spawnedNum;

    public static int defeatedEnemies;

    public GameObject[] enemyPrefab;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(EndlessSpawing());
	}

    // Update is called once per frame
    void Update () {
        if (spawnedNum >= 300)
        {
            spawn = false;
        }
        if(defeatedEnemies >= 300)
        {
            spawnedNum = 0;
            defeatedEnemies = 0;
            GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>().GameWon();
        }
	}

    IEnumerator EndlessSpawing()
    {
        while (spawn)
        {
            yield return new WaitForSeconds(2);
            if (enemyPrefab.Length > 0)
            {
                Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Length)], transform.position, Quaternion.Euler(Vector3.zero));
                spawnedNum++;
            }
            else
            {
                Instantiate(enemyPrefab[0], transform.position, Quaternion.Euler(Vector3.zero));
                spawnedNum++;
            }
        }
    }
}