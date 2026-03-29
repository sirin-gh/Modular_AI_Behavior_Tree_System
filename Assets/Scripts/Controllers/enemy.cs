using UnityEngine;
using UnityEngine.AI;   // IMPORTANT for NavMesh

public class enemy : MonoBehaviour
{
    public Transform player;
    public float detectionRadius = 5f;

    private Animator animator;
    private NavMeshAgent agent;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRadius)
        {
            // AI movement
            agent.SetDestination(player.position);

            //  Animation
            animator.SetBool("Run", true);
        }
        else
        {
            // Stop moving
            agent.ResetPath();
            animator.SetBool("Run", false);
        }

        // Optional: smooth face player while chasing
        if (agent.velocity.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
