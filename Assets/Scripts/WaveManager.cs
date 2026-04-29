using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class WaveManager : MonoBehaviour
{
    public SpawnPoint[] spawnPoints; // 6 tower spawn points

    public ForestManager forestManager;
    public AudioSource source;
    public AudioClip waveStartSoundEffect;
    public GameObject funFactPanel;

    [SerializeField] public List<AIEnemy> enemyTypes = new List<AIEnemy>();

    public int currentWave = 1;
    public int enemiesPerWave = 6;
    public float timeBetweenSpawns = 0.5f;
    public float timeBetweenWaves = 5f;
    public TextMeshProUGUI waveText;
    private int enemiesAlive = 0;
    private bool waveInProgress = false;

    private int totalWeight = 0;
    void Awake()
    { foreach (AIEnemy enemy in enemyTypes) totalWeight += enemy.spawnWeight;}

    void Start()
    { StartCoroutine(WaveDowntime()); }

    IEnumerator WaveDowntime()
    {
        funFactPanel.SetActive(true);
        forestManager.SetDecayActive(false);
        forestManager.ResetForestHealth();
        waveText.gameObject.SetActive(true);
        waveText.text = "Wave " + currentWave;
        source.PlayOneShot(waveStartSoundEffect);
        yield return new WaitForSeconds(timeBetweenWaves);

        waveText.gameObject.SetActive(false);

        StartCoroutine(StartWave());
    }

    IEnumerator StartWave()
    {
        funFactPanel.SetActive(false);
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
        SpawnPoint randomSpawn = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Enemy spawns at a random spawn point
        AIEnemy enemy = CalculateWeight(randomSpawn);
        Debug.LogError("Enemy Spawned");

        enemiesAlive++;

        var enemyScript = enemy.GetComponent<AIEnemy>();
        Debug.Log(enemyScript.waveManager);
        enemyScript.waveManager = this;

        //Enemy will begin to travel to designated tower
        enemyScript.targetTower = randomSpawn.assignedTower;
    }

    private AIEnemy CalculateWeight(SpawnPoint randomSpawn)
    {
        int weightPassed = 0;
        int randomWeight = Random.Range(0, totalWeight);
        AIEnemy selectedEnemy = null;
        foreach (AIEnemy enemy in enemyTypes)
        {
            weightPassed += enemy.spawnWeight;
            if (randomWeight <= weightPassed)
            {
                selectedEnemy = enemy;
                break;
            }
        }

        return Instantiate(selectedEnemy, randomSpawn.transform.position, Quaternion.identity);
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