using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Enemy
{
    void TakeDamage(Vector3 exploLocation, float damage);
    float Health();

    void SetEnemyStats(float health, float damage);
}
