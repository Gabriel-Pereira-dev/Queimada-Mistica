using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAiScript : MonoBehaviour
{
    public NavMeshAgent agent;
    Transform player;
    public LayerMask whatIsPlayer,whatisGround;
    public int stamina;

    // Patrolling
    public Vector3 walkPoint;
    bool walkPointsSet = false;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked = false;

    // States
    public float sightRange,attackRange;
    public bool playerInSightRange,playerInAttackRange;

    private void Awake(){
        player = GameObject.Find("FirstPersonPlayer").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update(){
        playerInSightRange = Physics.CheckSphere(transform.position,sightRange,whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position,attackRange,whatIsPlayer);
        if(!playerInSightRange && !playerInAttackRange) Patrolling();
        if(playerInSightRange && !playerInAttackRange) ChasePlayer();
        if(playerInSightRange && playerInAttackRange) AttackPlayer();
    }

    private void Patrolling(){
        if(!walkPointsSet) SearchWalkPoint();
        else agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if(distanceToWalkPoint.magnitude < 1f) walkPointsSet = false;
    }

    private void SearchWalkPoint(){
        float randomZ = Random.Range(-walkPointRange,walkPointRange);
        float randomX = Random.Range(-walkPointRange,walkPointRange);
        
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        bool isWalkable = Physics.Raycast(walkPoint,-transform.up,2f,whatisGround);
        if(isWalkable){
            walkPointsSet = true;
        }
            
    
    }

    private void ChasePlayer(){
        agent.SetDestination(player.position);
    }

    private void AttackPlayer(){
        agent.SetDestination(transform.position);

        // transform.LookAt(player);

        if(!alreadyAttacked){
            
            // ShootAction

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack),timeBetweenAttacks);
        }
    }

    private void ResetAttack(){
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage){
        stamina-= damage;

        if(stamina <= 0){
            Invoke(nameof(Die),2f);
        }
    }

    private void Die(){
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
