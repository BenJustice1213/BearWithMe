using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public SpawnPoint[] spawnPoints; // 6 tower spawn points

    public int currentWave = 1;
    public int enemiesPerWave = 6;
    public float timeBetweenSpawns = 0.5f;
    public float timeBetweenWaves = 5f;

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
        StartCoroutine(StartWave());
    }

    IEnumerator StartWave()
    {
        waveInProgress = true;

        // Spawn 6 enemies + 4 after each wave
        int enemiesToSpawn = enemiesPerWave + ((currentWave - 1) * 4);

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    void SpawnEnemy()
    {
        // Enemy spawns at a random spawn point
        SpawnPoint randomSpawn = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject enemy = Instantiate(enemyPrefab, randomSpawn.transform.position, Quaternion.identity);

        enemiesAlive++;

        var enemyScript = enemy.GetComponent<Enemy>();
        enemyScript.waveManager = this;

        //Enemy will begin to travel to designated tower
        enemyScript.targetTower = randomSpawn.assignedTower;
        enemyScript.MoveToNextPosition();

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
            StartCoroutine(NextWave());
        }
    }

    IEnumerator NextWave()
    {
        waveInProgress = false;

        //Downtime to add UI and fun facts later
        yield return new WaitForSeconds(timeBetweenWaves);

        currentWave++;
        StartCoroutine(StartWave());
    }
}