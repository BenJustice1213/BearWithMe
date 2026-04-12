using System.Numerics;
using System.Runtime.Serialization;
using UnityEngine;

public class AIEnemy : MonoBehaviour
{
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float attackRange;
    protected float distanceFromTarget = 0.0f;

    public WaveManager waveManager;
    public TreeTower targetTower;

    protected void Start()
    {

        // Initialize enemy behavior, such as finding the target tower
    }

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

    protected void FixedUpdate()
    {
        distanceFromTarget = Vector3.Distance(transform.position, targetTower.transform.position) - attackRange;

        // Check distance to target tower and attack if in range
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
