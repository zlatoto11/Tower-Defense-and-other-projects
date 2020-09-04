using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable] // serializable so we can see and configure in editor
public class WaveEnemies {
    public string name;

    public float delayBetweenEnemies; 
    public Transform enemyPrefab;

    public Transform spawnPoint; 

    public int numberOfEnemies;

}

[System.Serializable]
public class Wave {
    public string name;
    public static int totalWaves;
    public List<WaveEnemies> waveEnemies;
}
public class WaveSpawning : MonoBehaviour {
    public List<Wave> waves;
    private Wave m_CurrentWave;
    public Wave CurrentWave { get { return m_CurrentWave; } }
    public Text waveCountdownText;
    public Text enemiesLeftText;
    public int enemiesLeft = 0;

    public float waitInbetweenWaveRounds = 0f;

    public float timeBetweenWaves = 15;
    public float countdown;
    public bool betweenBigWaves = false;
    public bool levelStarted = false;

    public GameObject NextLevel;

    Management management;

    private void Start () {
        GameObject gcGO = GameObject.Find ("GameController");
        management = gcGO.GetComponent<Management> ();
    }
    public void StartLevel () {
        if (levelStarted == false) {
            StartCoroutine (SpawnLoop ());
            levelStarted = true; // used for only updating timer when the level is running
        }
    }

    private void Update () {

        if (levelStarted) {
            if (countdown <= 0f) {
                countdown = timeBetweenWaves;
            }
            if (betweenBigWaves == true){
            countdown -= Time.deltaTime;    //Reupdate timer between big waves
            }

            waveCountdownText.text = "Time Until Next Wave: " + Mathf.Floor (countdown).ToString ();
            enemiesLeftText.text = "Enemies Left: " + enemiesLeft;
        }

    }
    IEnumerator SpawnLoop () {
        foreach (Wave W in waves) {
            m_CurrentWave = W;
            foreach (WaveEnemies Wenemies in W.waveEnemies) {
                if (Wenemies.delayBetweenEnemies > 0)
                    yield return new WaitForSeconds (Wenemies.delayBetweenEnemies); 
                if (Wenemies.enemyPrefab != null && Wenemies.numberOfEnemies > 0) {
                    for (int i = 0; i < Wenemies.numberOfEnemies; i++) {
                        betweenBigWaves = false;
                        SpawnEnemy (Wenemies.enemyPrefab, Wenemies.spawnPoint);
                        enemiesLeft++;
                        yield return new WaitForSeconds (Wenemies.delayBetweenEnemies);
                    }
                }
            }
            betweenBigWaves = true;
            timeBetweenWaves = 20;
            countdown = timeBetweenWaves;
            yield return new WaitForSeconds (countdown); // IN BETWEEN BIG WAVES
        }
        timeBetweenWaves = 10;
        countdown = timeBetweenWaves;
        yield return new WaitForSeconds (countdown);
        
        if (management.isGameOver != true) {
            NextLevel.SetActive (true);
        }
        levelStarted = false;
    }

    void SpawnEnemy (Transform enemyPrefab, Transform spawnPoint) {
        Instantiate (enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}