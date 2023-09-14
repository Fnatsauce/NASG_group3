using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIFixedRoute : MonoBehaviour
{
    public List<Transform> wayPoints;
    public float patrolSpeed;

    private int currentWaypointIndex = 0;


    
    void Update()
    {
        if (wayPoints.Count == 0) {
            Debug.Log("No point found.");
            return;
        }

        Transform targetWaypoint = wayPoints[currentWaypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, patrolSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.5f) {
            currentWaypointIndex++;
            if (currentWaypointIndex >= wayPoints.Count) {
                currentWaypointIndex = 0;
            }
        }
    }
}
