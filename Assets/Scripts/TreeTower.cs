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
        if (ForestManager.Instance == null)
        {
            Debug.LogWarning($"ForestManager.Instance is null when tower '{name}' changed damage state to {isTakingDamage}");
            return;
        }

        if(isTakingDamage && !loggedAsAttacked)
        {
            ForestManager.Instance.TowerStartedTakingDamage();
            loggedAsAttacked = true;
            Debug.Log($"Tower '{name}' STARTED taking damage (attackers: {attackingEnemies.Count}).");
        }
        else if(!isTakingDamage && loggedAsAttacked)
        {
            ForestManager.Instance.TowerStoppedTakingDamage();
            loggedAsAttacked = false;
            Debug.Log($"Tower '{name}' STOPPED taking damage (attackers: {attackingEnemies.Count}).");
        }
    }
}