using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class AISimples : MonoBehaviour
{
    public FOVInimigos _cabeca;
    public List<Transform> patrolPoints;
    NavMeshAgent _navMesh;
    Transform alvo;
    Vector3 posicInicialDaAI;
    Vector3 ultimaPosicConhecida;
    float timerProcura;
    public float pursuitRange = 40.0f; // Define the pursuit range here

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    enum estadoDaAI
    {
        patrulhando, seguindo, procurandoAlvoPerdido, esperando
    };
    estadoDaAI _estadoAI = estadoDaAI.patrulhando;
    int currentPatrolPointIndex = 0;
    public float waitTime = 6.0f; // Time to wait at each patrol point
    float currentWaitTime = 0.0f; // Current wait time

    void Start()
    {
        _navMesh = GetComponent<NavMeshAgent>();
        alvo = null;
        ultimaPosicConhecida = Vector3.zero;
        _estadoAI = estadoDaAI.patrulhando;
        posicInicialDaAI = transform.position;
        timerProcura = 0;

        if (patrolPoints.Count == 0)
        {
            Debug.LogWarning("No patrol points assigned. Adding default patrol points.");
            // Create and add some default patrol points (you can customize these)
            GameObject patrolPoint1 = new GameObject("PatrolPoint1");
            patrolPoint1.transform.position = transform.position + new Vector3(10, 0, 0);
            patrolPoints.Add(patrolPoint1.transform);

            GameObject patrolPoint2 = new GameObject("PatrolPoint2");
            patrolPoint2.transform.position = transform.position - new Vector3(10, 0, 0);
            patrolPoints.Add(patrolPoint2.transform);
        }
    }

    void Update()
    {
        Navigation();
    }



    private void Navigation()
    {
        if (_cabeca)
        {
            switch (_estadoAI)
            {
                case estadoDaAI.patrulhando:
                    _navMesh.SetDestination(patrolPoints[currentPatrolPointIndex].position);

                    if (Vector3.Distance(transform.position, patrolPoints[currentPatrolPointIndex].position) < 1.0f)
                    {
                        // Start waiting
                        _estadoAI = estadoDaAI.esperando;
                        currentWaitTime = 0.0f;
                        anim.SetBool("Walk", false);
                    }

                    if (_cabeca.inimigosVisiveis.Count > 0)
                    {
                        alvo = _cabeca.inimigosVisiveis[0];
                        ultimaPosicConhecida = alvo.position;
                        if (Vector3.Distance(transform.position, alvo.position) <= pursuitRange)
                        {
                            _estadoAI = estadoDaAI.seguindo;
                        }
                    }
                    break;

                case estadoDaAI.esperando:
                    currentWaitTime += Time.deltaTime;

                    if (currentWaitTime >= waitTime)
                    {
                        // Finish waiting, move to the next point
                        _estadoAI = estadoDaAI.patrulhando;
                        currentPatrolPointIndex = (currentPatrolPointIndex + 1) % patrolPoints.Count;
                        anim.SetBool("Walk", true);
                    }

                    if (_cabeca.inimigosVisiveis.Count > 0)
                    {
                        alvo = _cabeca.inimigosVisiveis[0];
                        ultimaPosicConhecida = alvo.position;
                        if (Vector3.Distance(transform.position, alvo.position) <= pursuitRange)
                        {
                            _estadoAI = estadoDaAI.seguindo;
                        }
                    }
                    break;

                case estadoDaAI.seguindo:
                    _navMesh.SetDestination(alvo.position);

                    if (!_cabeca.inimigosVisiveis.Contains(alvo))
                    {
                        ultimaPosicConhecida = alvo.position;
                    }

                    // Continue chasing within the pursuit range
                    if (Vector3.Distance(transform.position, alvo.position) > pursuitRange)
                    {
                        _estadoAI = estadoDaAI.procurandoAlvoPerdido;
                    }
                    break;

                case estadoDaAI.procurandoAlvoPerdido:
                    _navMesh.SetDestination(ultimaPosicConhecida);
                    timerProcura += Time.deltaTime;

                    if (timerProcura > 5)
                    {
                        timerProcura = 0;
                        _estadoAI = estadoDaAI.patrulhando;
                        currentPatrolPointIndex = 0;
                    }

                    if (_cabeca.inimigosVisiveis.Count > 0)
                    {
                        alvo = _cabeca.inimigosVisiveis[0];
                        ultimaPosicConhecida = alvo.position;
                        if (Vector3.Distance(transform.position, alvo.position) <= pursuitRange)
                        {
                            _estadoAI = estadoDaAI.seguindo;
                        }
                    }
                    break;
            }
        }
    }


    // Visualize the pursuit range in the Unity Editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, pursuitRange);
    }
}