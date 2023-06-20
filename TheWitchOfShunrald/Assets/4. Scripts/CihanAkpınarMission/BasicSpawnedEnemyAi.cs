using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSpawnedEnemyAi : MonoBehaviour
{
    public float lookSpawnedEnemyRadius = 10f;
    Transform target;
    UnityEngine.AI.NavMeshAgent agent;


    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        target = DenemeCihan.instance.player.transform;
    }

    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance<=lookSpawnedEnemyRadius)
        {
            agent.SetDestination(target.position);
            if (distance<=agent.stoppingDistance)
            {

            }
        }
    }
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookSpawnedEnemyRadius);
    }
}
