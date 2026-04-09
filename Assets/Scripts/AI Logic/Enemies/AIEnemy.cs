using System.Runtime.Serialization;
using UnityEngine;

public class AIEnemy : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] float attackRange;
    [SerializeField] float attackCooldown;

    public WaveManager waveManager;
    public TreeTower targetTower;

    public void MoveToNextPosition()
    {
        // Logic to move the enemy along a predefined path
    }

    public void ChaseAway()
    {
        waveManager.EnemyChased();
        // Give time to let the enemy run off the board before destroying it
        ClearEnemy();
    }

    private void ClearEnemy()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        // Placeholder for testing enemy behavior
        if (Input.GetKeyDown(KeyCode.K))
        {
            ChaseAway();
        }
    }
}
