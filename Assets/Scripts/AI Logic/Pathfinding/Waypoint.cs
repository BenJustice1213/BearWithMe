using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] public Waypoint nextWaypoint;
    [SerializeField] private bool isExitPoint = false;
    private float gizmoSize = 0.25f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(!nextWaypoint)
        {
            Debug.LogError(gameObject.name + " does not have its nextWaypoint field assigned.");
            return;
        }
        CallibrateWaypoint();
        OnDrawGizmos();
    }

    void CallibrateWaypoint()
    {
        Vector3 directionToAim = 
            Vector3.RotateTowards(transform.forward, 
                nextWaypoint.transform.position, 
                360f, 0.0f);
        transform.rotation = Quaternion.LookRotation(directionToAim);
    }

    void OnDrawGizmos()
    {
        if (nextWaypoint)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, nextWaypoint.transform.position);
            if (isExitPoint) { Gizmos.color = Color.yellow; }
            Gizmos.DrawSphere(transform.position, gizmoSize);
        }
        else
        {    
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, gizmoSize);
        }
    }
}
