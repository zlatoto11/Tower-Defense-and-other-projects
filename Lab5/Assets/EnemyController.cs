using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

	public Waypoint waypoint;
	public Vector3 lastSeenPosition;
	public GameObject target;
	public Transform destination;
	NavMeshAgent agent;
	public int sightFov = 110;
	public bool seenTarget = false;
	SphereCollider collider;
	public StateMachine stateMachine = new StateMachine ();
	Vector3 rightPeripheral;
	Vector3 leftPeripheral;



	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		agent.destination = waypoint.transform.position;
		collider = GetComponent<SphereCollider> ();

		stateMachine.ChangeState (new State_Patrol (this));
	}

	// Update is called once per frame
	void Update () {
		if (!agent.pathPending && agent.remainingDistance < 0.5f) {
			Waypoint nextWaypoint = waypoint.nextWaypoint;
			waypoint = nextWaypoint;
			agent.destination = waypoint.transform.position;
		}
		stateMachine.Update ();
	}

	private void OnTriggerStay (Collider other) {
		// is it the player?
		if (other.gameObject == target) {
			// angle between us and the player
			Vector3 direction = other.transform.position - transform.position;
			float angle = Vector3.Angle (direction, transform.forward);
			// reset whether we’ve seen the player
			seenTarget = false;

			RaycastHit hit;

			// is it less than our field of view
			if (angle < sightFov * 0.5f) {
				// if the raycast hits the player we know
				// there is nothing in the way
				// adding transform.up raises up from the floor by 1 unit
				if (Physics.Raycast (transform.position + transform.up,
						direction.normalized,
						out hit,
						collider.radius)) {
					if (hit.collider.gameObject == target) {
						// flag that we've seen the player
						// remember their position
						seenTarget = true;
						lastSeenPosition = target.transform.position;
					}
				}
			}
		}
	}

	void OnDrawGizmos () {
		Gizmos.color = Color.blue;
		if (collider != null) {
			Gizmos.DrawWireSphere (transform.position, collider.radius);

			if (seenTarget)
				Gizmos.DrawLine (transform.position, lastSeenPosition);

			if (lastSeenPosition != Vector3.zero)
				// draw a small sphere
				Gizmos.DrawWireSphere (lastSeenPosition, 1.0f);

			// calculate left fov vector
			rightPeripheral = (Quaternion.AngleAxis (sightFov * 0.5f, Vector3.up) *
				transform.forward * collider.radius);
			leftPeripheral = (Quaternion.AngleAxis (sightFov * -0.5f, Vector3.up) *
				transform.forward * collider.radius);
			// draw lines for the left and right edges of the field of view
			Gizmos.DrawLine (transform.position, transform.position + rightPeripheral);
			Gizmos.DrawLine (transform.position, transform.position + leftPeripheral);

		}
	}

	public float fireRate = 0.5f;
	public float nextFire = 0.0f;
	public GameObject shot;
	public Transform shotTransform;

	public void Fire () {
		Debug.Log("in fire");
		if (Time.time > nextFire) {
			Debug.Log("fired");
			nextFire = Time.time + fireRate;
			 GameObject bulletGO = GameObject.Instantiate(
				shot,
				shotTransform.position,
				shotTransform.rotation);
		}		
		float horizontalMovement = Input.GetAxis ("Horizontal");
		float verticalMovement = Input.GetAxis ("Vertical");
	}
}