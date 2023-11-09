using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class AISimplesRange : MonoBehaviour
{
    public FOVEnemies _head;
    public List<Vector3> patrolPointsPositions;
    public float pursuitRange = 40.0f; // Define the pursuit range here
    public float attackRange = 10.0f; // Define the attack range here
    public float waitTime = 6.0f; // Time to wait at each patrol point
    public GameObject projectilePrefab; // Reference to the projectile prefab
    public float projectileSpeed = 10.0f; // Speed at which the projectile moves
    public float timeBetweenAttacks = 2.0f; // Time between attacks
    public Transform shootPoint;
    public FOVEnemies fovEnemies;

    private Animator anim;
    private NavMeshAgent _navMesh;
    private Transform target;
    private Vector3 lastPosKnown;
    private float timerProcura;
    private int currentPatrolPointIndex = 0;
    private float currentWaitTime = 0.0f;
    public float attackTimer = 0.0f;

    // Define the delegate for state functions
    private delegate void StateFunction();
    private StateFunction currentStateFunction;

    // Dictionary to map states to their respective state functions
    private Dictionary<stateOfAi, StateFunction> stateFunctions = new Dictionary<stateOfAi, StateFunction>();

    public enum stateOfAi
    {
        patrolling, following, searchingLostTarget, waiting, attacking
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
        stateFunctions[stateOfAi.attacking] = Attacking;
        // Set the initial state function
        currentStateFunction = stateFunctions[_stateAI];
        
    }

    void Update()
    {
        currentStateFunction.Invoke();
        //Debug.Log(_stateAI);
        attackTimer += Time.deltaTime;
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
                //Attacking();
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

    private void Attacking()
    {
        if (target != null)
        {
            _navMesh.SetDestination(transform.position); // Stop moving while attacking
            transform.LookAt(target);

            if (attackTimer >= timeBetweenAttacks)
            {
                // Reset attack timer
                attackTimer = 0.0f;
                // Calcule a direção do disparo em relação ao alvo
                Vector3 shootDirection = (target.position - shootPoint.position).normalized;

                // Calcule a rotação do projetil com base na direção do disparo
                 Quaternion rotation = Quaternion.LookRotation(-shootDirection);
                
                //Quaternion rotation = Quaternion.Euler(0, Mathf.Atan2(shootDirection.x, shootDirection.z) * Mathf.Rad2Deg, 0);
                // Instancie o projetil no shootPoint
                Rigidbody rb = Instantiate(projectilePrefab, shootPoint.position, rotation).GetComponent<Rigidbody>();
                //Rigidbody rb = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity).GetComponent<Rigidbody>();

                // Corrija a rotação da seta para garantir que a ponta esteja virada para você
                Vector3 eulerAngles = rb.transform.eulerAngles;
                eulerAngles.x = 90; // Mantenha a rotação X 
                eulerAngles.z = 90; // Mantenha a rotação Z
                rb.transform.eulerAngles = eulerAngles;

                rb.AddForce(-shootDirection * projectileSpeed, ForceMode.Impulse);
                rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
                rb.AddForce(transform.up * 3f, ForceMode.Impulse);

                
                
            }

            float distanceToAlvo = Vector3.Distance(transform.position, target.position);

            if (distanceToAlvo > attackRange)
            {
                // Target moved out of attack range
                _stateAI = stateOfAi.following;
            }
        }
        else
        {
            _stateAI = stateOfAi.patrolling;
        }

        CheckForVisibleEnemies();
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

    public AISimplesRange.stateOfAi GetCurrentState()
    {
        return _stateAI;
    }


    // Visualize the pursuit range in the Unity Editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, pursuitRange);
    }
}
