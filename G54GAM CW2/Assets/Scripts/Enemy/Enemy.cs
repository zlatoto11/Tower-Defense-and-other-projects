using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour {
    [Header ("Enemy Attributes")]
    public float enemyMS = 10f; // MS NOW DONE BY NAV MESH AGENT
    public int enemyHP = 100;
    public int enemyCredits = 25;
    public int enemyArmor = 0; // TODO 
    private Transform target;
    [Header ("Assignable Attributes")]
    public Waypoints waypoints;
    NavMeshAgent agent;
    public Transform destination;

    PlayerInfo playerInfo;

    WaveSpawning wSpawner;

    void Start () {
        GameObject gcGO = GameObject.Find ("GameController");
        waypoints = GameObject.FindGameObjectWithTag ("FirstWaypoint").GetComponent<Waypoints> ();
        agent = GetComponent<NavMeshAgent> ();
        if (agent.enabled && !agent.isOnNavMesh) {  //Problems with Nav Mesh Agent not spawning properly on Nav Mesh
            var position = transform.position;  //Teleport him on to it
            NavMeshHit hit;
            NavMesh.SamplePosition (position, out hit, 10.0f, NavMesh.AllAreas);
            position = hit.position;
            agent.Warp (position);
        }
        agent.destination = waypoints.transform.position;   //For waypoints
        agent.speed = enemyMS;
        wSpawner = gcGO.GetComponent<WaveSpawning> ();
        playerInfo = gcGO.GetComponent<PlayerInfo> ();

    }

    void Update () {
        if (!agent.pathPending && agent.remainingDistance < 0.1f) {  // If within .1 of destination get next waypoint from waypoint
            Waypoints nextWaypoint = waypoints.nextWaypoint;
            waypoints = nextWaypoint;
            agent.destination = waypoints.transform.position;
        }
    }

    private void EnemyDeath () {    //Enemy death from turret
        Destroy (gameObject);
        wSpawner.enemiesLeft -= 1;
        PlayerInfo.Credits += enemyCredits;
    }
    public void ReachedEnd () { //Enemy death from reaching the end
        Destroy (gameObject);
        PlayerInfo.Lives--;
        wSpawner.enemiesLeft -= 1;
        Debug.Log ("reached End");
    }

    public void TakeDamage (int damageDealt) {  
        if (enemyHP <= 0) { //Problem with Blaster turret sometimes calling enemy death twice
            return;         // == issues with enemies left going below 0 into minus numbers
        }
        enemyHP -= damageDealt;
        if (enemyHP <= 0) {
            EnemyDeath ();
        }
    }

}