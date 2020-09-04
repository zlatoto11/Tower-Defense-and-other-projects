using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    private Transform target;
    public int bulletDamage;

    public float speed = 70f;
    public float attackAOE = 0f;
    public GameObject impactEffect;

    private void Start() {
        GameObject gcGO = GameObject.Find ("GameController"); 
    }
    public void Seek (Transform _target, int turretDamage) {    //Bullet damage updated here, probably shouldnt be here
        target = _target;                                       //but ran out of time to fix
        bulletDamage = turretDamage;
    }
    // Update is called once per frame
    void Update () {
        if (target == null) {
            Destroy (gameObject);   //If target has been destroyed also destroy the object.
            return;                 // could be updated to instead go to the nearest object.
        }

        Vector3 direction = target.position - transform.position; // which way the bullet should go to get to the target. same as in turret.
        float distanceThisFrame = speed * Time.deltaTime; // how much to move this frame

        if (direction.magnitude <= distanceThisFrame) { // Distance of bullet to target = dir.magnitude. if less than distance this frame = we've hit as we dont want bullet to overshoot.
            TargetHit ();
            return;
        }
        //Object Movement
        transform.Translate (direction.normalized * distanceThisFrame, Space.World);
        //Object Rotation
        transform.LookAt (target); // looks towards target.

    }

    void TargetHit () {
        GameObject effectInstance = (GameObject) Instantiate (impactEffect, transform.position, transform.rotation);    //instantiate impact effect on hit.
        Destroy (effectInstance, 5f);

        if (attackAOE > 0f) {
            Explode (); //Damage in AOE
        } else {
            Damage (target); //Damage single target
        }
        Destroy (gameObject);
    }

    void Explode () {
        Collider[] objectsHit = Physics.OverlapSphere (transform.position, attackAOE);  //Check overlaping colliders of the AOE range sphere.
        foreach (Collider collider in objectsHit) {
            if (collider.tag == "Enemy") {
                Damage (collider.transform);
            }
        }
    }

    void Damage (Transform enemy) {
        enemy.GetComponent<Enemy>().TakeDamage(bulletDamage);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, attackAOE);   //Checking range when turrets are on nodes.
    }
}