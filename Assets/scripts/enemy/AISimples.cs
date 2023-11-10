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
using System.Collections;

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
    private AttackManager attackManager;
    private EnemyHealth enemyHealth;
    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;
    private bool isDying = false;


    // Define the delegate for state functions
    private delegate void StateFunction();
    private StateFunction currentStateFunction;

    // Dictionary to map states to their respective state functions
    private Dictionary<stateOfAi, StateFunction> stateFunctions = new Dictionary<stateOfAi, StateFunction>();
    private List<GameObject> enemiesDetectingPlayer = new List<GameObject>(); // List of enemies detecting the player

    public enum stateOfAi
    {
        patrolling, following, searchingLostTarget, waiting, attacking, dead
    };
    stateOfAi _stateAI = stateOfAi.patrolling;

    private void Awake()
    {
        //anim = GetComponent<Animator>();
        attackManager = FindObjectOfType<AttackManager>();
    }

    void Start()
    {
        _navMesh = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<EnemyHealth>();
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
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
        stateFunctions[stateOfAi.dead] = Dead;


        // Set the initial state function
        currentStateFunction = stateFunctions[_stateAI];
    }

    void Update()
    {

        Debug.Log(_stateAI);



        lastAttackTime += Time.deltaTime;



        if (_stateAI != stateOfAi.dead)
        {
            currentStateFunction.Invoke();
        }





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
            float distanceToAlvo = Vector3.Distance(transform.position, target.position);

            if (!_head.visibleEnemies.Contains(target))
            {
                lastPosKnown = target.position;
            }

            if (distanceToAlvo > pursuitRange)
            {
                // Player moved out of pursuit range, start searching
                _stateAI = stateOfAi.searchingLostTarget;
                _navMesh.ResetPath(); // Clear the current path
            }
            else if (distanceToAlvo <= attackRange)
            {
                // Player is within attack range, attack
                _stateAI = stateOfAi.attacking;
                _navMesh.ResetPath(); // Clear the current path
            }
            else
            {
                // Player is within pursuit range but outside attack range, continue following
                _navMesh.SetDestination(target.position);
            }
        }
        else
        {
            // No target, go back to patrolling
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

            if (distanceToAlvo > attackRange)
            {
                // Player moved out of attack range, start pursuing
                _stateAI = stateOfAi.following;
                _navMesh.ResetPath(); // Clear the current path
            }
            else if (lastAttackTime >= attackCooldown)
            {
                // Perform the attack when the cooldown is met
                Debug.Log("Attacking");
                AttackPlayer(target.gameObject);
                lastAttackTime = 0;
            }

            // Check enemy health here
            if (enemyHealth.health <= 0)
            {
                StartCoroutine(StartDeathDelay());
            }
        }
        else
        {
            // No target, go back to patrolling
            _stateAI = stateOfAi.patrolling;
        }

        CheckForVisibleEnemies();
    }
    private IEnumerator StartDeathDelay()
    {
        yield return new WaitForSeconds(0.5f); // Ajuste o valor conforme necessário
        if (!isDying)
        {
            isDying = true;

            currentStateFunction = Dead;
        }
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
        if (other.gameObject.CompareTag("Player") && !attackManager.IsEnemyAttacking(gameObject))
        {
            PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();

            if (player != null && lastAttackTime >= attackCooldown)
            {
                player.TakeDamage(damage);
                lastAttackTime = 0f;
                attackManager.RegisterAttackingEnemy(gameObject);
                StartCoroutine(ResetAttackCooldown());
            }
        }
    }

    private IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        attackManager.UnregisterAttackingEnemy(gameObject);
    }


    private void Dead()
    {

        if (isDying)
        {
            Debug.Log("Entering Dead state");
            Vector3 currentPosition = transform.position;
            rb.useGravity = false;
            rb.isKinematic = true;
            capsuleCollider.enabled = false;
            _navMesh.enabled = false;
            _head.enabled = false;
            transform.position = currentPosition;
            // Outras ações que você deseja realizar ao entrar no estado Dead
        }

    }


    public AISimples.stateOfAi GetCurrentState()
    {
        return _stateAI;
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