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

    private int lastValidNumOfWaypoints = 0;
    private bool isDataValid = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(Application.IsPlaying(gameObject))
            return;
        else
            CheckChildren();
    }

    void CheckChildren()
    {
        Waypoint[] childWaypoints = GetComponentsInChildren<Waypoint>();
        if (childWaypoints.Length > 0)
        {
            waypoints = new List<Waypoint>(childWaypoints);
            numOfWaypoints = waypoints.Count;
        }
    }

    // void OnValidate()
    // {
    //     if (numOfWaypoints != lastValidNumOfWaypoints)
    //     {
    //         lastValidNumOfWaypoints = numOfWaypoints;
    //         isDataValid = false;
    //     }
    //     else
    //     {
    //         isDataValid = true;
    //     }
    // }

    // Update is called once per frame
    void Update()
    {
        if(Application.IsPlaying(gameObject))
            return;
        else
            InEditor();
    }

    void InEditor()
    {
        if (numOfWaypoints == waypoints.Count)
            return; 
        else if (numOfWaypoints > waypoints.Count)
            addWaypoints();
        else
            cullWaypoints();
    }

    void addWaypoints()
    {
        for (int i = waypoints.Count; i < numOfWaypoints; i++)
        {
            GameObject newWaypoint = new GameObject("Waypoint " + (i + 1));
            newWaypoint.transform.parent = transform;
            newWaypoint.AddComponent<CircleCollider2D>();
            newWaypoint.GetComponent<CircleCollider2D>().isTrigger = true;
            newWaypoint.GetComponent<CircleCollider2D>().radius = 0.25f;
            newWaypoint.tag = "Waypoint";
            Waypoint waypointComponent = newWaypoint.AddComponent<Waypoint>();
            if (i > 0)
                waypoints[i-1].nextWaypoint = waypointComponent;
            waypoints.Add(waypointComponent);
        }
    }

    void cullWaypoints()
    {
        for (int i = waypoints.Count - 1; i >= numOfWaypoints; i--)
        {
            DestroyImmediate(waypoints[i].gameObject);// 
            waypoints.RemoveAt(i);
        }
    }
}
