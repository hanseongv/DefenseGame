using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private List<Transform> waypoints;

    [SerializeField]
    private float moveSpeed = 2f;

    private int currentIndex = 0;

    void Update()
    {
        if (waypoints == null || waypoints.Count == 0) return;

        Transform target = waypoints[currentIndex];
        transform.position = Vector2.MoveTowards(
            transform.position,
            target.position,
            moveSpeed * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            currentIndex = (currentIndex + 1) % waypoints.Count;
        }
    }

    public void SetWaypoints(List<Transform> wp)
    {
        waypoints = wp;
    }
}