using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ForestManager : MonoBehaviour
{
    public static ForestManager Instance;

    public float maxForestHealth = 1000f;
    private float currentForestHealth;

    public float baseDecayPerSecond = 5f;
    public float extraDecayPerTower = 5f;

    public Slider forestHealthBar;
    private bool decayActive = false;
    private int towersUnderAttack = 0;

    public GameObject gameOverPanel;
    private bool isGameOver = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentForestHealth = maxForestHealth;

        forestHealthBar.maxValue = maxForestHealth;
        forestHealthBar.value = currentForestHealth;
    }

    public void ResetForestHealth()
    {
        currentForestHealth = maxForestHealth;
        forestHealthBar.value = currentForestHealth;
        towersUnderAttack = 0;
    }

    void Update()
    {
        if (!decayActive) return;

        // 1 tower under attack each adds 5 damage to decay
        float totalDPS = baseDecayPerSecond + (towersUnderAttack * extraDecayPerTower);

        currentForestHealth -= totalDPS * Time.deltaTime;
        currentForestHealth = Mathf.Clamp(currentForestHealth, 0, maxForestHealth);

        forestHealthBar.value = currentForestHealth;

        if (!isGameOver && currentForestHealth <= 0)
        {
            GameOver();
        }
    }

    public void SetDecayActive(bool active)
    {
        decayActive = active;
    }

    public void TowerStartedTakingDamage()
    {
        towersUnderAttack++;
    }

    public void TowerStoppedTakingDamage()
    {
        towersUnderAttack = Mathf.Max(0, towersUnderAttack - 1);
    }

    void GameOver()
    {
        isGameOver = true;

        Time.timeScale = 0f; // freeze game
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // change name if needed
    }
}