using UnityEngine;
using System.Collections.Generic;

public class TreeTower : MonoBehaviour
{
    private bool loggedAsAttacked = false;
    public List<GameObject> attackingEnemies = new List<GameObject>();

    public void StartAttack(GameObject attacker)
    {
        attackingEnemies.Add(attacker);
        CheckForAttackers();
    }

    public void StopAttack(GameObject attacker)
    {
        attackingEnemies.Remove(attacker);
        CheckForAttackers();
    }

    private void CheckForAttackers()
    {
        if(attackingEnemies.Count > 0)
                { SetDamageState(true); }
        else    { SetDamageState(false); }
    }

    private void SetDamageState(bool isTakingDamage)
    {
        if(isTakingDamage && !loggedAsAttacked)
        {
            ForestManager.Instance.TowerStartedTakingDamage();
            loggedAsAttacked = true;
        }
        else if(!isTakingDamage && loggedAsAttacked)
        {
            ForestManager.Instance.TowerStoppedTakingDamage();
            loggedAsAttacked = false;
        }
    }
}