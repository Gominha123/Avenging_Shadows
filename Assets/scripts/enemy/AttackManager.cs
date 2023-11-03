using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public List<GameObject> attackingEnemies = new List<GameObject>();

    public void RegisterAttackingEnemy(GameObject enemy)
    {
        attackingEnemies.Add(enemy);
    }

    public void UnregisterAttackingEnemy(GameObject enemy)
    {
        attackingEnemies.Remove(enemy);
    }

    public bool IsEnemyAttacking(GameObject enemy)
    {
        return attackingEnemies.Contains(enemy);
    }
}