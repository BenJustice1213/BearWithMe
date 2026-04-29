using System.Collections;
using UnityEngine;

public class Smoker : AIEnemy
{
    [SerializeField] private Fire firePrefab;

    protected override IEnumerator PerformAction()
    {
        alreadyActing = true;
        currentState = EnemyState.Acting;
        yield return new WaitForSeconds(0.5f);
        Fire newFire = Instantiate(firePrefab, transform.position, Quaternion.identity);
        newFire.targetTower = targetTower;
        ChaseAway();
    }
}
