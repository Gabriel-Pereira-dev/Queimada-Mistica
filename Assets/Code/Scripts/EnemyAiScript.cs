using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAiScript : MonoBehaviour
{
    [Header("Game Settings")]
    public NavMeshAgent agent;
    public Transform player;
    public GameObject ball;
    // Patrolling
    private Vector3 walkPoint;
    bool walkPointsSet = false;
    private float walkPointRange = 150f;

    //Attacking
    
    [Header("Throw Settings")]
    // Pickup
    public float pickupRange = 5f;

    // States
    public float sightRange;
    [Header("Throw Settings")]
    public float throwRange;
    public float throwCooldown;
    bool alreadyThrowed = false;

    [Header("Layer Settings")]
    public LayerMask whatIsPlayer;
    public LayerMask whatIsGround;
    public LayerMask whatIsBall;
    [Header("Sensor States")]
    
    public bool playerInSightRange;
    public bool playerInThrowRange;

    private void Awake(){
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update(){
        playerInSightRange = Physics.CheckSphere(transform.position,sightRange,whatIsPlayer);
        playerInThrowRange = Physics.CheckSphere(transform.position,throwRange,whatIsPlayer);
        bool isBallOnEnemySide = ball.GetComponent<BallScript>().isOnEnemySide;
        bool isBallPickupedByEnemy = ball.GetComponent<BallScript>().isPickuped && GetComponent<EnemyPickUpBall>().heldObject != null;

        
        if(isBallOnEnemySide && !isBallPickupedByEnemy) ChaseBall();
        else if(playerInSightRange && !playerInThrowRange && isBallPickupedByEnemy) ChasePlayer();
        else if(playerInSightRange && playerInThrowRange && isBallPickupedByEnemy) AttackPlayer();
        else if(!isBallOnEnemySide && !isBallPickupedByEnemy) Patrolling();
    }

    private void Patrolling(){
        Debug.Log("Patrol");
        if(!walkPointsSet) SearchWalkPoint();
        else agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if(distanceToWalkPoint.magnitude < 1f) walkPointsSet = false;
    }

    private void SearchWalkPoint(){
        float randomZ = Random.Range(-walkPointRange,walkPointRange);
        float randomX = Random.Range(-walkPointRange,walkPointRange);
        
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        bool isWalkable = Physics.Raycast(walkPoint,-transform.up,2f,whatIsGround);
        if(isWalkable){
            walkPointsSet = true;
        }
            
    
    }

    private void ChasePlayer(){
        Debug.Log("ChasePlayer");
        agent.SetDestination(player.position);
    }

    private void ChaseBall(){
        Debug.Log("ChaseBall");
        agent.SetDestination(ball.transform.position);
        Vector3 distanceToBall = transform.position - ball.transform.position;
        if(distanceToBall.magnitude < pickupRange){
            GetComponent<EnemyPickUpBall>().PickupObject(ball);
        }
    }

    private void AttackPlayer(){
        Debug.Log("AttackPlayer");
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if(!alreadyThrowed){
            GetComponent<EnemyPickUpBall>().ThrowObject();
            // ShootAction

            alreadyThrowed = true;
            Invoke(nameof(ResetAttack),throwCooldown);
        }
    }

    private void ResetAttack(){
        alreadyThrowed = false;
    }

    private void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, throwRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, pickupRange);
    }
}
