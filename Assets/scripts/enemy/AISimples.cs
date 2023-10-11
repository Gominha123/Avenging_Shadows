using System;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.AI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using UnityEngine.TextCore.Text;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.UIElements.VisualElement;

[RequireComponent(typeof(NavMeshAgent))]

public class AISimples : MonoBehaviour
{
    public FOVEnemies _head;
    public List<Vector3> patrolPointsPositions;
    public float pursuitRange = 40.0f; // Define the pursuit range here
    public float attackRange = 2.0f;   // Define the attack range here
    public float waitTime = 6.0f;       // Time to wait at each patrol point
    public float attackCooldown = 5.0f; // Time between attacks
    public int damage = 10;
    public float waitingDistance = 10.0f;

    private Animator anim;
    public NavMeshAgent _navMesh;
    private Transform target;
    private Vector3 lastPosKnown;
    private float timerProcura;
    private int currentPatrolPointIndex = 0;
    private float currentWaitTime = 0.0f;
    public float lastAttackTime = 5.0f; // Time of the last attack
    private bool isAttacking = false;
    private GameObject enemyAttackingPlayer;
   

    // Define the delegate for state functions
    private delegate void StateFunction();
    private StateFunction currentStateFunction;

    // Dictionary to map states to their respective state functions
    private Dictionary<stateOfAi, StateFunction> stateFunctions = new Dictionary<stateOfAi, StateFunction>();
    private List<GameObject> enemiesDetectingPlayer = new List<GameObject>(); // List of enemies detecting the player

    enum stateOfAi
    {
        patrolling, following, searchingLostTarget, waiting, attacking, waiting_to_attack
    };
    stateOfAi _stateAI = stateOfAi.patrolling;

    private void Awake()
    {
        //anim = GetComponent<Animator>();
    }

    void Start()
    {
        _navMesh = GetComponent<NavMeshAgent>();
        target = null;
        lastPosKnown = Vector3.zero;
        _stateAI = stateOfAi.patrolling;
        timerProcura = 0;

        if (patrolPointsPositions.Count == 0)
        {
            Debug.LogWarning("No patrol points assigned. Adding default patrol points.");
            // Add default patrol points (you can customize these)
            patrolPointsPositions.Add(transform.position + new Vector3(10, 0, 0));
            patrolPointsPositions.Add(transform.position - new Vector3(10, 0, 0));
        }

        // Define state functions for each AI state
        stateFunctions[stateOfAi.patrolling] = Patrolling;
        stateFunctions[stateOfAi.waiting] = Waiting;
        stateFunctions[stateOfAi.following] = Following;
        stateFunctions[stateOfAi.searchingLostTarget] = SearchingLostTarget;
        stateFunctions[stateOfAi.attacking] = Attacking; // Add the attacking state
        stateFunctions[stateOfAi.waiting_to_attack] = WaitingToAttack;

        // Set the initial state function
        currentStateFunction = stateFunctions[_stateAI];
    }

    void Update()
    {
        lastAttackTime += Time.deltaTime;

        currentStateFunction.Invoke();

    }

    private void Patrolling()
    {
        // Set the destination for patrolling
        _navMesh.SetDestination(patrolPointsPositions[currentPatrolPointIndex]);

        float distanceToPatrolPoint = Vector3.Distance(transform.position, patrolPointsPositions[currentPatrolPointIndex]);

        if (distanceToPatrolPoint < 1.0f)
        {
            // Start waiting
            _stateAI = stateOfAi.waiting;
            currentWaitTime = 0.0f;
            //anim.SetBool("Walk", false);
        }

        CheckForVisibleEnemies();
    }

    private void Waiting()
    {
        currentWaitTime += Time.deltaTime;

        if (currentWaitTime >= waitTime)
        {
            // Finish waiting, move to the next point
            _stateAI = stateOfAi.patrolling;
            currentPatrolPointIndex = (currentPatrolPointIndex + 1) % patrolPointsPositions.Count;
            //anim.SetBool("Walk", true);
        }

        CheckForVisibleEnemies();
    }

    private void Following()
    {
        if (target != null)
        {
            _navMesh.SetDestination(target.position);

            if (!_head.visibleEnemies.Contains(target))
            {
                lastPosKnown = target.position;
            }

            float distanceToAlvo = Vector3.Distance(transform.position, target.position);

            if (distanceToAlvo > pursuitRange)
            {
                // Target moved out of sight, recalculate the path
                _stateAI = stateOfAi.searchingLostTarget;
                _navMesh.ResetPath(); // Clear the current path
            }
            else if (distanceToAlvo <= attackRange) // Check for attack range
            {
                _stateAI = stateOfAi.attacking; // Transition to the attack state
                _navMesh.ResetPath(); // Clear the current path
            }
        }
        else
        {
            _stateAI = stateOfAi.patrolling;
        }

        CheckForVisibleEnemies();
    }

    private void SearchingLostTarget()
    {
        _navMesh.SetDestination(lastPosKnown);
        timerProcura += Time.deltaTime;

        if (timerProcura > 5)
        {
            timerProcura = 0;
            _stateAI = stateOfAi.patrolling;
            currentPatrolPointIndex = 0;
        }

        CheckForVisibleEnemies();
    }
    private void WaitingToAttack()
    {
        // Check if the player is being attacked by an enemy with the "enemy" tag
        if (IsPlayerBeingAttacked())
        {
            // Wait at the specified distance from the player until the attacking enemy is defeated
            if (enemyAttackingPlayer != null && !enemyAttackingPlayer.activeSelf && IsWithinWaitingRange(enemyAttackingPlayer))
            {
                // Attacking enemy is defeated and within waiting range, now attack the player
                AttackPlayer(target.gameObject);
                _stateAI = stateOfAi.attacking; // Transition to the attacking state
                _navMesh.ResetPath(); // Clear the current path
            }
            else if (enemyAttackingPlayer != null && IsWithinWaitingRange(enemyAttackingPlayer))
            {
                // Wait at the specified distance from the player
                _navMesh.SetDestination(enemyAttackingPlayer.transform.position);
            }
        }
        else
        {
            // No enemy is attacking the player or outside waiting range, transition back to patrolling
            _stateAI = stateOfAi.patrolling;
            _navMesh.ResetPath(); // Clear the current path
        }
    }


    private void Attacking()
    {
        if (target != null)
        {
            _navMesh.SetDestination(target.position);

            if (!_head.visibleEnemies.Contains(target))
            {
                lastPosKnown = target.position;
            }

            float distanceToAlvo = Vector3.Distance(transform.position, target.position);

            if (distanceToAlvo > pursuitRange)
            {
                // Target moved out of sight, recalculate the path
                _stateAI = stateOfAi.searchingLostTarget;
                _navMesh.ResetPath(); // Clear the current path
            }
            else if (distanceToAlvo <= attackRange && lastAttackTime >= attackCooldown)
            {
                Debug.Log("ataking");
                // attack animation
            }
        }
        else
        {
            _stateAI = stateOfAi.patrolling;
        }

        CheckForVisibleEnemies();
    }

    private void AttackPlayer(GameObject player)
    {

        if (player != null)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {


                // Reduce the player's health with the specified damage value (e.g., 10)
                playerHealth.TakeDamage(damage);


            }
            else
            {
                Debug.LogWarning("PlayerHealth component not found on the player object.");
            }
        }
        else
        {
            Debug.LogWarning("Player object is null.");
        }
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Player") // Assuming the player has a "Player" tag
    //    {
    //        // Perform the attack action here, e.g., reducing player's health
    //        Debug.Log("attack");
    //        AttackPlayer(other.gameObject);
    //        lastAttackTime = 0;
    //    }
    //}

    //private void OnTriggerEnter(Collider collision)
    //{
    //    if (collision.tag == "Player")
    //    {
    //        PlayerHealth player = collision.GetComponent<PlayerHealth>();

    //        if (lastAttackTime >= attackCooldown)
    //        {
    //            player.TakeDamage(damage);
    //            lastAttackTime = 0f;
    //        }
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        

        if (other.gameObject.tag == "Player")
        {
            
            PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();

            if (player != null)
            {
                

                if (lastAttackTime >= attackCooldown)
                {
                    
                    player.TakeDamage(damage);
                    lastAttackTime = 0f;
                }
            }
            
        }
    }
    private bool IsAttackingEnemyDefeated()
    {
        // Check if the first enemy detecting the player is defeated
        if (enemiesDetectingPlayer.Count > 0)
        {
            GameObject firstEnemy = enemiesDetectingPlayer[0];
            return firstEnemy == null || !firstEnemy.activeSelf;
        }
        return true; // No enemy is attacking, return true to proceed with the attack
    }
    private bool IsPlayerBeingAttacked()
    {
        // Implement your logic to check if the player is being attacked by an enemy
        // For example, you can use raycasting to detect if an enemy is attacking the player
        RaycastHit hit;
        if (Physics.Raycast(transform.position, target.position - transform.position, out hit, pursuitRange))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                GameObject attackingEnemy = hit.collider.gameObject;
                if (!enemiesDetectingPlayer.Contains(attackingEnemy))
                {
                    enemiesDetectingPlayer.Add(attackingEnemy); // Add the enemy to the list if not already present
                }
                enemyAttackingPlayer = attackingEnemy;
                return true;
            }
        }

        return false;
    }

    // Call this method when an enemy stops detecting the player (e.g., enemy is defeated or player moves out of range)
    private void EnemyStoppedDetectingPlayer(GameObject enemy)
    {
        if (enemiesDetectingPlayer.Contains(enemy))
        {
            enemiesDetectingPlayer.Remove(enemy);
        }
    }

    private bool IsWithinWaitingRange(GameObject enemy)
    {
        float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
        return distanceToEnemy <= waitingDistance;
    }



    private void CheckForVisibleEnemies()
    {
        if (_head.visibleEnemies.Count > 0)
        {
            target = _head.visibleEnemies[0];
            lastPosKnown = target.position;

            if (Vector3.Distance(transform.position, target.position) <= pursuitRange)
            {
                _stateAI = stateOfAi.following;
            }
        }

        // Update the current state function based on the new state
        currentStateFunction = stateFunctions[_stateAI];
    }

    // Visualize the pursuit range in the Unity Editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, pursuitRange);
        Gizmos.DrawWireSphere(transform.position, waitingDistance);
    }
}
