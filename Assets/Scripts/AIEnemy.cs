using UnityEngine;

public class AIEnemy : MonoBehaviour
{
    private float movementSpeed = 2f;
    private float attackRange = 1.5f;
    private float attackCooldown = 2f;

    public void Attack()
    {
        // Logic to perform a basic attack on the player
    }
}

public class Smoker : AIEnemy
{
    public void StartFire()
    {
        // Logic to emit smoke that obscures vision
    }
}

public class Hunter : AIEnemy
{
    float movementSpeed = 3f;
    float attackRange = 1f;

}

public class Wolf : AIEnemy
{
    float movementSpeed = 4f;

    public void Howl()
    {
        // Logic to howl, potentially attracting other enemies or intimidating the player
    }
}
