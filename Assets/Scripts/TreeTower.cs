using UnityEngine;

public class TreeTower : MonoBehaviour
{
    public bool isBeingAttacked = false;

    public void StartAttack()
    {
        if (!isBeingAttacked)
        {
            isBeingAttacked = true;
            ForestManager.Instance.TowerStartedTakingDamage();
        }
    }

    public void StopAttack()
    {
        if (isBeingAttacked)
        {
            isBeingAttacked = false;
            ForestManager.Instance.TowerStoppedTakingDamage();
        }
    }
}