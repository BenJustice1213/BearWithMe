using UnityEngine;

public class AIEnemy : MonoBehaviour
{
    [SerializeField] float movementSpeed =    1f;
    [SerializeField] float attackRange =      1f;
    [SerializeField] public int spawnWeight = 5;

    public enum EnemyState { Idling, Moving, Acting }
    [HideInInspector] public EnemyState currentState = EnemyState.Idling;
    [HideInInspector] public WaveManager waveManager;
    [HideInInspector] public TreeTower targetTower;
    [HideInInspector] protected float distanceFromTarget = 0.0f;

    protected virtual void MoveToNextPosition()
    {
        currentState = EnemyState.Moving;
        transform.position = Vector3.MoveTowards(transform.position, targetTower.transform.position, movementSpeed * Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D other)
    { 
        print("E");
        if (other.gameObject.CompareTag("Roar")) ChaseAway(); }

    public void ChaseAway()
    {
        waveManager.EnemyChased();
        Debug.LogError("Enemy Chased");
        // Give time to let the enemy run off the board before destroying it
        ClearEnemy();
    }

    private void ClearEnemy()
    {
        Destroy(gameObject);
    }

    protected void FixedUpdate()
    {
        if (targetTower == null) { return; }

        distanceFromTarget = Vector3.Distance(transform.position, targetTower.transform.position);
        if (distanceFromTarget > attackRange)
        { MoveToNextPosition(); }
        else { targetTower.StartAttack(this.gameObject); }
    }

    void Update()
    {
        // Placeholder for testing enemy behavior
        //  if (Input.GetKeyDown(KeyCode.K))
        //    {
        //        ChaseAway();
        //    }
        //}
    }
}
