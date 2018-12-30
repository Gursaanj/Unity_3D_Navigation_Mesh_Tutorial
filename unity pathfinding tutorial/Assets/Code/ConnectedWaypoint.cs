using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectedWaypoint : Waypoint {


    public float _connectivityRadius;
    List<ConnectedWaypoint> _connections;

	// Use this for initialization
	void Start ()
    {
        //List of all waypoint objects in the scene
        GameObject[] allWaypoints = GameObject.FindGameObjectsWithTag("Waypoint");

        //List of waypoints I will refer to later
        _connections = new List<ConnectedWaypoint>();

        //Need to check if they are a connected waypoint
        for (int i = 0; i < allWaypoints.Length; i++)
        {
            ConnectedWaypoint nextWaypoint = allWaypoints[i].GetComponent<ConnectedWaypoint>();

            // if we found a waypoint
            if (nextWaypoint != null)
            {
                if (Vector3.Distance(this.transform.position, nextWaypoint.transform.position) <= _connectivityRadius && nextWaypoint != this)
                {
                    _connections.Add(nextWaypoint);
                }
            }
        }
	}

    public override void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, debugDrawRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _connectivityRadius);
    }

    public ConnectedWaypoint NextWaypoint(ConnectedWaypoint previousWaypoint)
    {
        if (_connections.Count == 0)
        {
            //No waypoints in list
            Debug.LogError("Not enough waypoints");
            return null;
        }
        // if there is only one way point and it is the same as the previous waypoint
        else if (_connections.Count == 1)
        {
            return previousWaypoint;
        }
        // otherwise, find a random one that isn't the previouse one.
        else
        {
            ConnectedWaypoint nextWaypoint;
            int nextindex = 0;

            do
            {
                nextindex = UnityEngine.Random.Range(0, _connections.Count);
                nextWaypoint = _connections[nextindex];
            } while (nextWaypoint == previousWaypoint); // only continue the loop when the next waypoint is the same as the previous waypoint.

            return nextWaypoint;
        }
    }
}
