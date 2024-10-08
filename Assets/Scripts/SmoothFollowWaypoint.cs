using UnityEngine;
using System.Collections;

public class SmoothFollowWaypoint : MonoBehaviour
{
    public Transform[] waypoints;  // Array of waypoint GameObjects
    public float moveSpeed = 5f; // Speed at which the object moves towards the waypoint
    public float rotationSpeed = 5f; // Speed of rotation towards the waypoint
    public float stopDistance = 0.1f; // Distance at which the object stops moving towards the waypoint

    private Animator animator; // Reference to the Animator component
    private int currentWaypointIndex = 0; // Index of the current waypoint
    private bool isMoving = true; // Flag to control movement

    void Start()
    {
        // Get the Animator component attached to the object
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (waypoints.Length > 0 && isMoving)
        {
            // Step 1: Move towards the current waypoint in real time
            MoveTowardsWaypoint();

            // Step 2: Smoothly rotate towards the waypoint's direction
            RotateTowardsWaypoint();
        }
    }

    void MoveTowardsWaypoint()
    {
        // Get the current waypoint
        Transform currentWaypoint = waypoints[currentWaypointIndex];

        // Calculate the direction to the current waypoint
        Vector3 direction = currentWaypoint.position - transform.position;
        float distance = direction.magnitude;

        // Move only if the object is further away than the stop distance
        if (distance > stopDistance)
        {
            // Normalize the direction and move towards the waypoint at a consistent speed
            Vector3 moveDirection = direction.normalized;
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            // Play the walking animation
            animator.SetBool("isMoving", true);
        }
        else
        {
            // Stop the animation when reaching the waypoint
            animator.SetBool("isMoving", false);

            // Move to the next waypoint
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length) // Loop back to the first waypoint
            {
                currentWaypointIndex = 0; // Uncomment this line if you want to loop through waypoints
                // currentWaypointIndex = waypoints.Length - 1; // Uncomment this line if you want to stop after reaching the last waypoint
            }
        }
    }


    void RotateTowardsWaypoint()
    {
        // Get the current waypoint
        Transform currentWaypoint = waypoints[currentWaypointIndex];

        // Calculate the direction to the current waypoint
        Vector3 direction = (currentWaypoint.position - transform.position).normalized;

        // Only rotate if the object is far enough to need rotation
        if (direction.magnitude > 0.01f)
        {
            // Calculate the rotation towards the waypoint
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            // Smoothly rotate towards the waypoint using Slerp
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            // Check if the object collides with any other object
            Debug.Log($"Collided with: {other.gameObject.name}"); // Log the name of the collided object

            // Stop movement upon collision
            isMoving = false; // Stop the movement logic
            animator.SetBool("isMoving", false); // Stop the animation as well

            // Stop any Rigidbody movement
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero; // Stop all movement
                rb.angularVelocity = Vector3.zero; // Stop any rotation
            }

            // Log the state change
            Debug.Log("Stopping movement and animation.");
        }
       
    }
}
