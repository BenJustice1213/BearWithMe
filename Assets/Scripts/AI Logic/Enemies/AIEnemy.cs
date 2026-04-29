using System.Collections;
using UnityEngine;

public class AIEnemy : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;
    [SerializeField] float attackRange = 1f;
    [SerializeField] public int spawnWeight = 5;

    [HideInInspector] public WaveManager waveManager;
    [HideInInspector] public SpawnPoint spawnPoint;
    [HideInInspector] public TreeTower targetTower;
    [HideInInspector] protected Animator animator;
    [HideInInspector] protected float distanceFromTarget = 0.0f;
    [HideInInspector] protected Vector3 currentWaypoint;
    [HideInInspector] protected bool alreadyActing = false;

    protected void Start()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void MoveToNextPosition()
    {
        animator.SetTrigger("Moving");
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, movementSpeed * Time.deltaTime);
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Roar")) ChaseAway();
        else if (other.gameObject.CompareTag("Waypoint")) UpdateFocus(other.gameObject.GetComponent<Waypoint>());
    }

    protected void UpdateFocus(Waypoint waypoint)
    {
        if (waypoint.isExitPoint) currentWaypoint = targetTower.transform.position;
        else currentWaypoint = waypoint.nextWaypoint.transform.position;
    }

    protected void ChaseAway()
    {
        animator.SetTrigger("Moving");
        waveManager.EnemyChased();

        // Ensure the tower knows this attacker stopped attacking
        if (targetTower != null)
            targetTower.StopAttack(this.gameObject);

        currentWaypoint = spawnPoint.gameObject.transform.position;
        attackRange = 0f;
        gameObject.GetComponent<Collider2D>().enabled = false;
        StartCoroutine(ClearEnemy());
    }

    protected IEnumerator ClearEnemy()
    {
        yield return new WaitForSeconds(6f);

        // Ensure tower is notified when this enemy is destroyed/removed
        if (targetTower != null)
            targetTower.StopAttack(this.gameObject);

        Destroy(gameObject);
    }

    protected void FixedUpdate()
    {
        if (targetTower == null) { return; }

        distanceFromTarget = Vector3.Distance(transform.position, targetTower.transform.position);
        if (distanceFromTarget > attackRange)
        { MoveToNextPosition(); }
        else
        {
            if (alreadyActing) { return; }
            StartCoroutine(PerformAction());
        }
    }

    protected virtual IEnumerator PerformAction()
    {
        alreadyActing = true;
        animator.SetTrigger("Acting");
        yield return new WaitForSeconds(0.5f);
        targetTower.StartAttack(this.gameObject);
    }

    void Update()
    {
        // Placeholder for testing enemy behavior
        //  if (Input.GetKeyDown(KeyCode.K))
        //    {
        //        ChaseAway();
        //    }
    }

    void OnDestroy()
    {
        // Safety-net: notify tower on destroy
        if (targetTower != null)
            targetTower.StopAttack(this.gameObject);
    }
}
