using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

//[RequireComponent(typeof())]

public class TurretAI : MonoBehaviour
{

    [Tooltip("The Object that will rotate")]
    public GameObject turretHead;
    [Tooltip("The Object that the bullets will fire from *Best to parent to Turret Head*")]
    public Transform barrel;

    //public GameObject bulletPrefab;

    [Tooltip("")]
    public int cost;
    [Tooltip("")]
    public string turretName;
    [Tooltip("Damage dealt to enemies when hit")]
    public int damage;
    [Tooltip("Damage dealt to enemies when hit")]
    public float fireRange;
    //[Tooltip("")]
    //public float bulletSpeed;
    [Tooltip("Time between shots in seconds")]
    public float fireWait;
    //[Tooltip("")]
    //public Image turretIcon;

    GameObject target;

    float currentTargetDis = 0;

    public LayerMask mask;

    Collider[] enemiesInRange;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RangeMonitor();
        Aim();
        Fire();
    }

    void RangeMonitor()
    {
        enemiesInRange = Physics.OverlapSphere(transform.position, fireRange, mask);
        if (enemiesInRange.Length > 0)
        {
            foreach (Collider eCol in enemiesInRange)
            {
                if (eCol.gameObject.GetComponent<EnemyAI>().disLeft >= currentTargetDis)
                {
                    currentTargetDis = eCol.gameObject.GetComponent<EnemyAI>().disLeft;
                    target = eCol.gameObject;
                    currentTargetDis = 0;
                }
            }
        }
        else
        {
            target = null;
            currentTargetDis = 0;
        }  
    }

    void Aim()
    {
        if (target != null)
        {
            Vector3 posDif = target.transform.position - transform.position;
            Vector3 turretRot = turretHead.transform.rotation.eulerAngles;
            turretRot.y = (Mathf.Atan2(posDif.x, posDif.z) * Mathf.Rad2Deg);
            turretHead.transform.rotation = Quaternion.Euler(turretRot);
        }
    }

    bool fire = true;

    void Fire()
    {
        if (fire && target != null)
        {
            //Firing with a bullet code.
            //GameObject bullet = Instantiate(bulletPrefab, barrel.position, Quaternion.Euler(turretHead.transform.eulerAngles));
            //bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * bulletSpeed, ForceMode.Impulse);
            //bullet.GetComponent<BulletManager>().shooter = gameObject;

            RaycastHit hit;
            if(Physics.Raycast(barrel.position, barrel.forward, out hit, fireRange, mask))
            {
                hit.collider.gameObject.GetComponent<EnemyAI>().TakeDamage(damage);
            }
            barrel.GetComponent<ParticleSystem>().Play();
            float waitTime = fireWait;
            StartCoroutine(FireWait(waitTime));
            fire = false;
            waitTime = 0.2f;
            StartCoroutine(FireWait(waitTime));
        }
    }

    IEnumerator FireWait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (waitTime == fireWait)
            fire = true;
        else
            barrel.GetComponent<ParticleSystem>().Stop();
    }
}