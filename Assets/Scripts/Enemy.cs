using UnityEngine;

public class Enemy : MonoBehaviour
{
    public WaveManager waveManager;
    public TreeTower targetTower;

    public void ChaseAway()
    {
        waveManager.EnemyChased();
        Destroy(gameObject);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            ChaseAway();
        }
    }
}