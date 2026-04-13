using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class WaveManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public SpawnPoint[] spawnPoints; // 6 tower spawn points

    public ForestManager forestManager;

    public int currentWave = 1;
    public int enemiesPerWave = 6;
    public float timeBetweenSpawns = 0.5f;
    public float timeBetweenWaves = 5f;
    public TextMeshProUGUI waveText;
    private int enemiesAlive = 0;
    private bool waveInProgress = false;

    void Start()
    {

        /*
        Loop through spawn points and tree towers. Presumably their indexes should
        match up. If not, setting up a new prefab that pairs a spawn point with a 
        tower may be the best course of action.
        - AJB
        */
        StartCoroutine(WaveDowntime());
    }

    IEnumerator WaveDowntime()
    {
        forestManager.SetDecayActive(false);
        forestManager.ResetForestHealth();
        waveText.gameObject.SetActive(true);
        waveText.text = "Wave " + currentWave;

        yield return new WaitForSeconds(timeBetweenWaves);

        waveText.gameObject.SetActive(false);

        StartCoroutine(StartWave());
    }

    IEnumerator StartWave()
    {
        Debug.LogError("Wave Started");
        forestManager.SetDecayActive(true);
        waveInProgress = true;

        // Spawn 6 enemies + 4 after each wave
        int enemiesToSpawn = enemiesPerWave + ((currentWave - 1) * 4);

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    // Refactor this to modify the enemy?

    void SpawnEnemy()
    {
        // Enemy spawns at a random spawn point
        SpawnPoint randomSpawn = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject enemy = Instantiate(enemyPrefab, randomSpawn.transform.position, Quaternion.identity);
        Debug.LogError("Enemy Spawned");

        enemiesAlive++;

        var enemyScript = enemy.GetComponent<AIEnemy>();
        Debug.Log(enemyScript.waveManager);
        enemyScript.waveManager = this;

        //Enemy will begin to travel to designated tower
        enemyScript.targetTower = randomSpawn.assignedTower;

        if (enemyPrefab == null)
        {
            Debug.LogError("Enemy prefab missing!");
            return;
        }
    }

    public void EnemyChased()
    {
        enemiesAlive--;
        
        if (enemiesAlive <= 0 && waveInProgress)
        {
            NextWave();
        }
    }

    void NextWave()
    {
        waveInProgress = false;

        currentWave++;
        StartCoroutine(WaveDowntime());
    }
}