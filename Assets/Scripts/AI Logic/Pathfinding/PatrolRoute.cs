using UnityEngine;
using System.Collections.Generic;

[ExecuteAlways]
public class PatrolRoute : MonoBehaviour
{
    [Header("Path Settings")]
    [Tooltip("The number of waypoints on this path")]
    [Range(0, 10)]
    [SerializeField] private int numOfWaypoints;
    private List<Waypoint> waypoints = new List<Waypoint>();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(Application.IsPlaying(gameObject))
            Debug.Log("Hello Level!");
        else
            Debug.Log("Hello Editor!");
    }

    // Update is called once per frame
    void Update()
    {
        if(Application.IsPlaying(gameObject))
            Debug.Log("Hello Level!");
        else
            InEditor();
    }

    void InEditor()
    {
        if (numOfWaypoints == waypoints.Count)
            return;
        // Assemble prefabs then review code.
        else if (numOfWaypoints > waypoints.Count)
        {
            for (int i = waypoints.Count; i < numOfWaypoints; i++)
            {
                GameObject newWaypoint = new GameObject("Waypoint " + (i + 1));
                newWaypoint.transform.parent = transform;
                Waypoint waypointComponent = newWaypoint.AddComponent<Waypoint>();
                if (waypoints.Count > 0)
                    waypointComponent.nextWaypoint = waypoints[waypoints.Count - 1];
                waypoints.Add(waypointComponent);
            }
        }
        else
        {
            for (int i = waypoints.Count - 1; i >= numOfWaypoints; i--)
            {
                DestroyImmediate(waypoints[i].gameObject);// 
                waypoints.RemoveAt(i);
            }
        }
        Debug.Log(numOfWaypoints);
    }
}
