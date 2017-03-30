using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour {

    public GameObject shooter;

    public int damage;

	// Use this for initialization
	void Start ()
    {
        shooter = GameObject.FindGameObjectWithTag("Player");
        damage = shooter.GetComponent<PlayerTurretControl>().damage;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Vector3.Distance(transform.position, shooter.transform.position) >= 500)
            Destroy(gameObject);
	}

    void OnTriggerEnter(Collider hit)
    {
        if (hit.tag == "Enemy")
        {
            hit.gameObject.GetComponent<EnemyAI>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}