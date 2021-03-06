﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public bool spawn = true;

    public static int spawnedNum;
    public static int defeatedEnemies;

    public static int spawnEnemies;
    public int enemiesToSpawn = 150;

    public GameObject[] enemyPrefab;

	// Use this for initialization
	void Start ()
    {
        spawn = true;
        spawnEnemies = enemiesToSpawn;
        StartCoroutine(EndlessSpawing());
	}

    // Update is called once per frame
    void Update ()
    {
        if (spawnedNum >= spawnEnemies)
        {
            spawn = false;
        }
        if(defeatedEnemies >= spawnEnemies)
        {
            GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>().GameWon();
            spawnedNum = 0;
            defeatedEnemies = 0;            
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
            else if (enemyPrefab.Length == 1)
            {
                Instantiate(enemyPrefab[0], transform.position, Quaternion.Euler(Vector3.zero));
                spawnedNum++;
            }
            else
                spawn = false;
        }
    }
}