using UnityEngine;
using UnityEngine.UI;

public class ForestManager : MonoBehaviour
{
    public static ForestManager Instance;

    public float maxForestHealth = 1000f;
    private float currentForestHealth;

    public float baseDecayPerSecond = 5f;
    public float extraDecayPerTower = 5f;

    public Slider forestHealthBar;

    private int towersUnderAttack = 0;

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

    void Update()
    {
        // 1 tower under attack each adds 5 damage to decay
        float totalDPS = baseDecayPerSecond + (towersUnderAttack * extraDecayPerTower);

        currentForestHealth -= totalDPS * Time.deltaTime;
        currentForestHealth = Mathf.Clamp(currentForestHealth, 0, maxForestHealth);

        forestHealthBar.value = currentForestHealth;
    }

    public void TowerStartedTakingDamage()
    {
        towersUnderAttack++;
    }

    public void TowerStoppedTakingDamage()
    {
        towersUnderAttack = Mathf.Max(0, towersUnderAttack - 1);
    }
}