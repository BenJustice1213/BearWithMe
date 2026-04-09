using System.Runtime.Serialization;
using UnityEngine;

public class AIEnemy : MonoBehaviour
{
    public float movementSpeed;
    public float attackRange;
    public float attackCooldown;

    public void MoveToNextPosition()
    {
        // Logic to move the enemy along a predefined path
    }

    public void Attack()
    {
        // Logic to perform a basic attack on the player
    }

    public void ScareAway()
    {
        // Scare the enemy off of the board
    }
}

public class Fire : MonoBehaviour
{
    private bool isActive = false;
    public void SetupFire()
    {
        // Give flame warm up period before it starts logic
    }
    public void RegisterFire()
    {
        isActive = true;
        // Let fire start contributing to forest health degradation
    }
    public void PutOut()
    {
        if(isActive) {/* Deregister fire if it's already registered */}

        // Delay to account for animation of putting out fire
        
    }
}

public class Hunter : AIEnemy
{

}

public class Smoker : AIEnemy
{

    List<Fire> activeFires = new List<Fire>();

    public void StartFire()
    {
        // Delay at start to account for animation
        activeFires.Add(new Fire());
        activeFires[activeFires.Count - 1].SetupFire();
    }
}

public class Wolf : AIEnemy
{

    public void FindNextCritter()
    {
        // Logic to howl, potentially attracting other enemies or intimidating the player
    }
}
