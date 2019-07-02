using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    static public WaypointManager Instance { get; private set; }

    [SerializeField]
    private List<Waypoint> waypoints;
    static public List<Waypoint> Waypoints { get { return Instance.waypoints; } }

    private void Awake()
    {
        if (Instance == null)
        { Instance = this; ConnectWaypoints(); }
        else
            Destroy(gameObject);
    }

    void ConnectWaypoints()
    {
        for (int i = 0; i < waypoints.Count - 1; i++)
        {
            waypoints[i].next = waypoints[i + 1];
            waypoints[i + 1].prev = waypoints[i];
        }
    }


    static public Waypoint GetWaypoint(int index)
    {
        if (index >= 0 && index < Waypoints.Count)
            return Waypoints[index];
        Debug.LogWarning("Waypoint id " + index + " doesn't exist");
        return null;
    }
}
