using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour {

    public Transform target;
    [Header ("Attributes")]
    public float range = 15f; //Range of turret
    public float fireRate = 1f;
    public int damage;
    public int level = 1;
    
    [Header ("Upgrade Variables")]
    public float rangeUpgrade;

    public float fireRateUpgrade;

    public int damageUpgrade;

    public int upgradeCost;
    public int upgradeInflation;
    private float fireCountdown = 0f;

    [Header ("Unity Fields")]
    public string enemyTag = "Enemy";
    public Transform partToRotate;

    public GameObject bulletPrefab;
    public Transform firePoint; //Where to shoot bullet from

    public Transform firePoint2 = null;

    public float turnSpeed = 10f; //turret rotation speed.

    // Start is called before the first frame update
    void Start () {
        InvokeRepeating ("UpdateTarget", 0f, 0.5f); // repeats function every .5 seconds, rather than doing thousands of updates per second
    }

    void UpdateTarget () {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag (enemyTag); // finds all enemies with the enemyTag
        float shortestDistance = Mathf.Infinity; // if enemy not found = infinity shortest distance as not found
        GameObject nearestEnemy = null; // iniitally doesn't exist;

        foreach (GameObject enemy in enemies) {
            //whole loop basically makes us attack the closest enemy to turret.
            float distanceToEnemy = Vector3.Distance (transform.position, enemy.transform.position); //for each enemy return distance to that enemy
            if (distanceToEnemy < shortestDistance) {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range) {
            target = nearestEnemy.transform; // if enemy isnt null and shortest distance is within range, set target to enemy.
        } else {
            target = null; // no longer in distance set to null
        }
    }
    void Update () {
        if (target == null) {
            return;
        }

        //Quaternion Rotate of head turret. Target Lock On
        Vector3 directionToEnemy = target.position - transform.position; //points to direction of enemy using the 3 coordinates. Point A -> B, take end destination minus point A
        Quaternion lookRotation = Quaternion.LookRotation (directionToEnemy); //how to rotate to look in that direction
        Vector3 rotation = Quaternion.Lerp (partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles; //actual rotation, Lerp = smooth rotation.
        partToRotate.rotation = Quaternion.Euler (0f, rotation.y, 0f); // rotate the turret to lock on to the enemy, we only need to rotate to the y axis using Euler.

        if (fireCountdown <= 0f) {
            Shoot ();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot () {
        GameObject bulletGO = (GameObject) Instantiate (bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet> ();

        if (bullet != null) {
            bullet.Seek (target,damage);
        }
        if (firePoint2 != null) {
            GameObject bulletGO2 = (GameObject) Instantiate (bulletPrefab, firePoint2.position, firePoint2.rotation);
            Bullet bullet2 = bulletGO2.GetComponent<Bullet> ();
            if (bullet2 != null) {
                bullet2.Seek (target,damage);
            }
        }
    }

    //draws turret range using Gizmos
    void OnDrawGizmosSelected () {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere (transform.position, range);
    }
}