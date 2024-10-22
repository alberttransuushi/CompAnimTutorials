using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavControl : MonoBehaviour
{
    public GameObject Target;
    public Transform EnemyTarget;
    private NavMeshAgent agent;
    private Animator animator;
    private bool isWalking;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.destination = Target.transform.position;
        animator.SetBool("isMoving", true);
        isWalking = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Set the agent's destination
        
        // Check if agent is still walking
        if (isWalking && agent.remainingDistance <= agent.stoppingDistance)
        {
            // If the agent reached the destination, stop the movement animation
            animator.SetBool("isMoving", false);
            animator.SetBool("isAttacking", true);
            Debug.Log("Agent has reached the destination.");
            transform.LookAt(EnemyTarget);

        }
        else if (!isWalking && agent.remainingDistance > agent.stoppingDistance)
        {
            // If the agent is walking again, play the movement animation
            animator.SetBool("isMoving", true);
            animator.SetBool("isAttacking", false);
            Debug.Log("Agent is moving towards the target.");
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        // Debug to check trigger interactions
        Debug.Log("Triggered by: " + other.name);

        if (other.CompareTag("Target"))
        {
            Debug.Log("Player entered the trigger zone.");

            // Stop walking and start attacking
            isWalking = false;
            agent.isStopped = true; // Stop the NavMeshAgent
            animator.SetBool("isMoving", false);
            animator.SetBool("isAttacking", true);
        }
    }
}
