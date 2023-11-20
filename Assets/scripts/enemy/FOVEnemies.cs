using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FOVEnemies : MonoBehaviour
{
    [Header("General")]
    public Transform enemyHead;
    public enum CollisionType
    {
        RayCast, OverlapSphere
    };
    public CollisionType collisionType = CollisionType.RayCast;
    public enum CheckFrequency
    {
        TenPerSecond, TwentyPerSecond, AllTheTime
    };
    public CheckFrequency checkFrequency = CheckFrequency.AllTheTime;
    [Range(1, 50)]
    public float visionDistance = 10;

    [Header("OverlapSphere")]
    public LayerMask enemyLayers = 2;
    public bool drawSphere = true;

    [Header("Raycast")]
    public string enemyTag = "Enemy";
    [Range(2, 180)]
    public float extraRaysPerLayer = 20;
    [Range(5, 180)]
    public float fieldOfViewAngle = 120;
    [Range(1, 10)]
    public int numberOfLayers = 3;
    [Range(0.02f, 0.15f)]
    public float layerSpacing = 0.1f;
    //
    [Space(10)]
    public List<Transform> visibleEnemies = new List<Transform>();
    List<Transform> temporaryCollisionList = new List<Transform>();
    LayerMask obstacleLayer;
    float checkTimer = 0;

    public GameObject player;
    public float hearRadius;
    public float downwardOffset = 0.3f;
    Animator anim;
    PlayerMovement2 pM;

    private void Start()
    {
        checkTimer = 0;
        if (!enemyHead)
        {
            enemyHead = transform;
        }
        // The ~ operator inverts the bits (0 becomes 1, and vice versa)
        obstacleLayer = ~enemyLayers;
        anim = GetComponent<Animator>();
        pM = player.GetComponent<PlayerMovement2>();
    }

    void Update()
    {
        if (checkFrequency == CheckFrequency.TenPerSecond)
        {
            checkTimer += Time.deltaTime;
            if (checkTimer >= 0.1f)
            {
                checkTimer = 0;
                CheckEnemies();
            }
        }
        if (checkFrequency == CheckFrequency.TwentyPerSecond)
        {
            checkTimer += Time.deltaTime;
            if (checkTimer >= 0.05f)
            {
                checkTimer = 0;
                CheckEnemies();
            }
        }
        if (checkFrequency == CheckFrequency.AllTheTime)
        {
            CheckEnemies();
        }

        float hearDistance = Vector3.Distance(transform.position, player.transform.position);
        if (pM.moveSpeed > pM.crouchSpeed && hearDistance < hearRadius)
        {
            //Debug.Log("I can hear you");
            anim.SetTrigger("Hearing");
        }
    }

    private void CheckEnemies()
    {
        if (collisionType == CollisionType.RayCast)
        {
            float halfLayerCount = numberOfLayers * 0.5f;
            for (int x = 0; x <= extraRaysPerLayer; x++)
            {
                for (float y = -halfLayerCount + 0.5f; y <= halfLayerCount; y++)
                {
                    float angleToRay = x * (fieldOfViewAngle / extraRaysPerLayer) + ((180.0f - fieldOfViewAngle) * 0.5f);
                    Vector3 directionMultiplier = (-enemyHead.right) + (enemyHead.up * y * layerSpacing) - (enemyHead.up * downwardOffset);
                    Vector3 rayDirection = Quaternion.AngleAxis(angleToRay, enemyHead.up) * directionMultiplier;
                    //
                    RaycastHit hitRaycast;
                    if (Physics.Raycast(enemyHead.position, rayDirection, out hitRaycast, visionDistance))
                    {
                        if (!hitRaycast.transform.IsChildOf(transform.root) && !hitRaycast.collider.isTrigger)
                        {
                            if (hitRaycast.collider.gameObject.CompareTag(enemyTag))
                            {
                                Debug.DrawLine(enemyHead.position, hitRaycast.point, Color.red);
                                //
                                if (!temporaryCollisionList.Contains(hitRaycast.transform))
                                {
                                    temporaryCollisionList.Add(hitRaycast.transform);
                                }
                                if (!visibleEnemies.Contains(hitRaycast.transform))
                                {
                                    visibleEnemies.Add(hitRaycast.transform);
                                }
                            }
                        }
                    }
                    else
                    {
                        Debug.DrawRay(enemyHead.position, rayDirection * visionDistance, Color.green);
                    }
                }
            }
        }
        if (collisionType == CollisionType.OverlapSphere)
        {
            Collider[] targetsInRadius = Physics.OverlapSphere(enemyHead.position, visionDistance, enemyLayers);
            foreach (Collider targetCollider in targetsInRadius)
            {
                Transform target = targetCollider.transform;
                Vector3 targetDirection = (target.position - enemyHead.position).normalized;
                if (Vector3.Angle(enemyHead.forward, targetDirection) < (fieldOfViewAngle / 2.0f))
                {
                    float targetDistance = Vector3.Distance(transform.position, target.position);
                    if (!Physics.Raycast(enemyHead.position, targetDirection, targetDistance, obstacleLayer))
                    {
                        if (!target.transform.IsChildOf(enemyHead.root))
                        {
                            if (!temporaryCollisionList.Contains(target))
                            {
                                temporaryCollisionList.Add(target);
                            }
                            if (!visibleEnemies.Contains(target))
                            {
                                visibleEnemies.Add(target);
                            }
                        }
                    }
                }
            }
            for (int x = 0; x < visibleEnemies.Count; x++)
            {
                Debug.DrawLine(enemyHead.position, visibleEnemies[x].position, Color.blue);
            }
        }
        // Remove enemies from the list that are no longer visible
        for (int x = 0; x < visibleEnemies.Count; x++)
        {
            if (!temporaryCollisionList.Contains(visibleEnemies[x]))
            {
                visibleEnemies.Remove(visibleEnemies[x]);
            }
        }
        temporaryCollisionList.Clear();
    }

    //private void OnDrawGizmosSelected()
    //{
    //    if (collisionType == CollisionType.OverlapSphere)
    //    {
    //        if (drawSphere)
    //        {
    //            Gizmos.color = Color.white;
    //            Gizmos.DrawWireSphere(enemyHead.position, visionDistance);
    //        }
    //        Gizmos.color = Color.green;
    //        float angleToRay1 = (180.0f - fieldOfViewAngle) * 0.5f;
    //        float angleToRay2 = fieldOfViewAngle + (180.0f - fieldOfViewAngle) * 0.5f;
    //        Vector3 rayDirection1 = Quaternion.AngleAxis(angleToRay1, enemyHead.up) * (-transform.right);
    //        Vector3 rayDirection2 = Quaternion.AngleAxis(angleToRay2, enemyHead.up) * (-transform.right);
    //        Gizmos.DrawRay(enemyHead.position, rayDirection1 * visionDistance);
    //        Gizmos.DrawRay(enemyHead.position, rayDirection2 * visionDistance);
    //        //
    //        UnityEditor.Handles.color = Color.green;
    //        float angle = Vector3.Angle(transform.forward, rayDirection1);
    //        Vector3 pos = enemyHead.position + (enemyHead.forward * visionDistance * Mathf.Cos(angle * Mathf.Deg2Rad));
    //        UnityEditor.Handles.DrawWireDisc(pos, enemyHead.transform.forward, visionDistance * Mathf.Sin(angle * Mathf.Deg2Rad));

    //        Gizmos.color = Color.red;
    //        Gizmos.DrawWireSphere(transform.position, hearRadius);
    //    }
    //}
}
