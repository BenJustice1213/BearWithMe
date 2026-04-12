using System.Runtime.Serialization;
using Mono.Cecil.Cil;
using Unity.VisualScripting;
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

    protected void MoveToNextPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetTower.transform.position, movementSpeed * Time.deltaTime);
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
        if(targetTower == null) { return; }

        distanceFromTarget = Vector3.Distance(transform.position, targetTower.transform.position);
        if(distanceFromTarget > attackRange)
                { MoveToNextPosition(); }
        else    { targetTower.StartAttack(this.gameObject);}
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
