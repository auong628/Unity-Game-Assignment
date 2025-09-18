using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiePatrol : MonoBehaviour
{
    public float speed = 2f; // Speed of the zombie
    public Transform pointA; // Starting patrol point (left)
    public Transform pointB; // Ending patrol point (right)
    private Vector3 targetPoint; // Current target point for the zombie to move towards
    private bool isFacingRight = true; // Check if the zombie is facing right
    void Start()
    {
        targetPoint = pointB.position; // Start by moving to pointB (right)
    }
    void Update()
    {
        Patrol();
    }
    void Patrol()
    {
        // Move towards the current target point
        transform.position = Vector3.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);
        // Check if the zombie needs to flip based on movement direction
        if (targetPoint.x > transform.position.x && !isFacingRight)
        {
            Flip();
        }
        else if (targetPoint.x < transform.position.x && isFacingRight)
        {
            Flip();
        }
        // If the zombie reaches the target point, switch to the other point
        if (Vector3.Distance(transform.position, targetPoint) < 0.1f)
        {
            targetPoint = targetPoint == pointA.position ? pointB.position : pointA.position;
        }
    }
    void Flip()
    {
        isFacingRight = !isFacingRight; // Toggle facing direction
        Vector3 localScale = transform.localScale;
        localScale.x *= -1; // Flip the sprite by reversing the x-axis scale
        transform.localScale = localScale;
    }
}
