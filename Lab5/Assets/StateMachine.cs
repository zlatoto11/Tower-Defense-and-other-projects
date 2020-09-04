using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IState {
	void Enter ();
	void Execute ();
	void Exit ();
}

public class StateMachine {
	IState currentState;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	public void Update () {
		if (currentState != null) currentState.Execute ();
	}

	public void ChangeState (IState newState) {
		if (currentState != null)
			currentState.Exit ();
		currentState = newState;
		currentState.Enter ();
	}
}

public class State_Patrol : IState {
	EnemyController owner;
	NavMeshAgent agent;
	Waypoint waypoint;

	public State_Patrol (EnemyController owner) { this.owner = owner; }

	public void Enter () {
		Debug.Log ("entering patrol state");
		waypoint = owner.waypoint;
		agent = owner.GetComponent<NavMeshAgent> ();
		agent.destination = waypoint.transform.position;
		// start moving, in case we were previously stopped
		agent.isStopped = false;
	}
	public void Execute () {
		Debug.Log ("updating patrol state");
		// same as before
		if (!agent.pathPending && agent.remainingDistance < 0.5f) {
			Waypoint nextWaypoint = waypoint.nextWaypoint;
			waypoint = nextWaypoint;
			agent.destination = waypoint.transform.position;
		}
		if (owner.seenTarget) {
			owner.stateMachine.ChangeState (new State_Attack (owner));
		}
	}

	public void Exit () {
		Debug.Log ("exiting patrol state");
		// stop moving
		agent.isStopped = true;
	}
}

public class State_Attack : IState {
	EnemyController owner;
	NavMeshAgent agent;

	Waypoint waypoint;
	public State_Attack (EnemyController owner) { this.owner = owner; }
	public void Enter () {
		Debug.Log ("entering attack state");
		agent = owner.GetComponent<NavMeshAgent> ();
		if (owner.seenTarget) {
			agent.destination = owner.lastSeenPosition;
			agent.isStopped = false;
		}
	}
	public void Execute () {
		Debug.Log ("updating attack state");
		agent.destination = owner.lastSeenPosition;
		agent.isStopped = false;
		if (!agent.pathPending && agent.remainingDistance < 5.0f) {
			agent.isStopped = true;
		}
		if (owner.seenTarget != true) {
			Debug.Log ("lost sight");
			owner.stateMachine.ChangeState (new State_Search (owner));

		}
		// fire on the player
		if (owner.seenTarget == true) {
			owner.Fire ();
		}
	}

	public void Exit () {
		Debug.Log ("exiting attack state");
	}

}

public class State_Search : IState {
	EnemyController owner;
	NavMeshAgent agent;

	Waypoint waypoint;

	float timestart;
	float secondTime;
	public State_Search (EnemyController owner) { this.owner = owner; }
	public void Enter () {
		Debug.Log ("entering search state");
		agent = owner.GetComponent<NavMeshAgent> ();
		agent.destination = owner.lastSeenPosition;
		agent.isStopped = false;
		timestart = Time.time;
	}
	public void Execute () {
		Debug.Log ("updating search state");
		secondTime = Time.time;
		if (owner.seenTarget) {
			owner.stateMachine.ChangeState (new State_Attack (owner));
		}

		if (Vector3.Distance (owner.gameObject.transform.position, owner.lastSeenPosition) <= 3.0f) {
			if (secondTime - timestart > 5) {
				owner.stateMachine.ChangeState (new State_Patrol (owner));
			}
		}
	}

	public void Exit () {
		Debug.Log ("exiting search state");
		agent.isStopped = true;
	}

}