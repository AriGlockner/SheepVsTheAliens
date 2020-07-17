using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTargeting : MonoBehaviour
{
    public Transform target;

    [Header("Attributes")]

    public float range = 15f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Alien";

    public Transform partToRotate;
    public float turnSpeed = 10f;

    public GameObject objectToShoot;
    public Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("UpdatedTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float minDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies) {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < minDistance)
            {
                minDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && minDistance <= range)
        {
            target = nearestEnemy.transform;
        } else
        {
            target = null;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        // Assigns closest alien in range to tower
        UpdateTarget();

        // If there isn't a target the tower can shoot
        if (target == null) return;
        Vector2 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = lookRotation.eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, 0f, rotation.z);

        if (fireCountdown <= 0f)
        {
            shoot(rotation);
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }


    void shoot(Vector2 rotation)
    {


        GameObject proj = (GameObject)Instantiate(objectToShoot, firePoint.position, firePoint.rotation);
        FirePoint projectile = proj.GetComponent<FirePoint>();
        if (projectile != null)
        {
            projectile.chase(target);
        }
    }
}
