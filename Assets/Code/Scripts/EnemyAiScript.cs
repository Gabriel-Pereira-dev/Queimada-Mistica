using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAiScript : MonoBehaviour
{
    public NavMeshAgent agent;
    Transform player;
    GameObject ball;
    public LayerMask whatIsPlayer,whatisGround,whatIsBall;
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
        ball = GameObject.Find("Ball");
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update(){
        playerInSightRange = Physics.CheckSphere(transform.position,sightRange,whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position,attackRange,whatIsPlayer);
        bool isballIsOnEnemySide = ball.GetComponent<BallScript>().isOnEnemySide;
        bool isBallPickuped = ball.GetComponent<BallScript>().isPickuped;
        bool hasHeldObject = GetComponent<EnemyPickUpBall>().heldObject != null;

        if((!playerInSightRange && !playerInAttackRange) || (!isBallPickuped && !isballIsOnEnemySide && !hasHeldObject)) Patrolling();
        else if(isballIsOnEnemySide && !isBallPickuped && !hasHeldObject) ChaseBall();
        else if(playerInSightRange && !playerInAttackRange && isBallPickuped) ChasePlayer();
        else if(playerInSightRange && playerInAttackRange && isBallPickuped) AttackPlayer();
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
        bool isWalkable = Physics.Raycast(walkPoint,-transform.up,2f,whatisGround);
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
        if(distanceToBall.magnitude < 5f){
            GetComponent<EnemyPickUpBall>().PickupObject(ball);
        }
    }

    private void AttackPlayer(){
        Debug.Log("AttackPlayer");
        agent.SetDestination(transform.position);

        // transform.LookAt(player);

        if(!alreadyAttacked){
            GetComponent<EnemyPickUpBall>().ThrowObject();
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
