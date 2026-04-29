using System.Runtime.Serialization;
using UnityEngine;

public class AIEnemy : MonoBehaviour
{
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float attackRange;
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

    protected virtual void EnemyAction()
    {
        currentState = EnemyState.Acting;
        Debug.LogWarning("EnemyAction method not implemented for " + gameObject.name);
    }

    public void OnTriggerEnter2D(Collider2D other)
    { if (other.tag == "Roar") ChaseAway(); }

    protected void ChaseAway()
    {
        currentState = EnemyState.Moving;
        waveManager.EnemyChased();
        Debug.LogError("Enemy Chased");
        // Give time to let the enemy run off the board before destroying it
        ClearEnemy();
    }

    protected void ClearEnemy()
    { Destroy(gameObject);}

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
