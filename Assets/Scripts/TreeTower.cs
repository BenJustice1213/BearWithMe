using UnityEngine;
using System.Collections.Generic;

public class TreeTower : MonoBehaviour
{
    public List<GameObject> attackingEnemies = new List<GameObject>();

    /*
    The order of StartAttack and StopAttack is
    important. Please don't change either.
    - AJB
    */

    public void StartAttack(GameObject attacker)
    {
        CheckForAttackers();
        attackingEnemies.Add(attacker);
    }

    public void StopAttack(GameObject attacker)
    {
        attackingEnemies.Remove(attacker);
        CheckForAttackers();
    }

    private void CheckForAttackers()
    {
        if(attackingEnemies.Count > 0)
                { ForestManager.Instance.TowerStartedTakingDamage(); }
        else    { ForestManager.Instance.TowerStoppedTakingDamage(); }
    }
}