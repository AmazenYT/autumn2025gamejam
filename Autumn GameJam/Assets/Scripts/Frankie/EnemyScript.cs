using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    public float health;

    //Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject Projectile;

    //states
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public EnemyKillManager killManager;
    
    public GameObject alternateModel;

    private void Awake()
    {
        player = GameObject.Find("PlayerObj").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();

    }

    private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        //calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ); 

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            ///Attack code here
            Rigidbody rb = Instantiate(Projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);

            
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }

    public void DestroyEnemy()
    {
        // Notify the assigned kill manager if set, otherwise fall back to a parent manager
        if (killManager != null)
        {
            killManager.AddKill();
        }
        else
        {
            var parentKillManager = GetComponentInParent<EnemyKillManager>();
            if (parentKillManager != null)
                parentKillManager.AddKill();
        }

        // Activate alternate model if assigned (keeps this GameObject alive so child can remain),
        // otherwise destroy the GameObject as before.
        ActivateAlternateModel();
    }

    /// <summary>
    /// Enable the assigned alternate model (typically a disabled child) when this enemy dies.
    /// This disables the enemy's renderers/colliders/agent and this script so the alternate model
    /// can remain visible in the scene. If no alternate model is assigned, the GameObject is destroyed.
    /// </summary>
    public void ActivateAlternateModel()
    {
        if (alternateModel != null)
        {
            // enable the alternate model (usually a child object)
            alternateModel.SetActive(true);

            // disable renderers that are not part of the alternate model
            var renderers = GetComponentsInChildren<Renderer>(true);
            foreach (var r in renderers)
            {
                if (r == null) continue;
                if (alternateModel != null && r.transform.IsChildOf(alternateModel.transform))
                    continue;
                r.enabled = false;
            }

            // disable colliders that are not part of the alternate model
            var colliders = GetComponentsInChildren<Collider>(true);
            foreach (var c in colliders)
            {
                if (c == null) continue;
                if (alternateModel != null && c.transform.IsChildOf(alternateModel.transform))
                    continue;
                c.enabled = false;
            }

            // disable navmesh agent and make rigidbody kinematic if present
            if (agent != null) agent.enabled = false;
            var rb = GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = true;

            // disable this script so AI logic stops
            enabled = false;

            // keep the GameObject alive so the alternate child remains in scene
            return;
        }

        // fallback: no alternate assigned, destroy the GameObject
        Destroy(gameObject);
    }

}
