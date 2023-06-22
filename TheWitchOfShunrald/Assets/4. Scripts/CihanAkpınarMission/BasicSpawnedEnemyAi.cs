using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.AI;

namespace CihanAkpınar
{
    public class BasicSpawnedEnemyAi : MonoBehaviour
    {
        public float lookSpawnedEnemyRadius = 10f;
        Transform target;
        NavMeshAgent agent;
        private float isBasicEnemyRuning;
        private float isBasicEnemyAttacking;
        private Animator anim;
        


        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            anim = GetComponentInChildren<Animator>();
            target = DenemeCihan.instance.player.transform;
           
        }

        void Update()
        {
            
            BasicEnemyAnim();
            
        }
        void FaceTarget()
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 3f);
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, lookSpawnedEnemyRadius);
        }

        void BasicEnemyAnim()
        {
            
            float distance = Vector3.Distance(target.position, transform.position);
            if (distance<=lookSpawnedEnemyRadius)
            {
                agent.SetDestination(target.position);
                anim.SetFloat("BasicEnemyMove",1f,0.3f,Time.deltaTime);
                if (distance<=agent.stoppingDistance+0.5f)
                {
                    anim.SetTrigger("BasicEnemyAttacking");
                }
            }
            else
            {
                anim.SetFloat("BasicEnemyMove",0f,0.3f,Time.deltaTime);
            }
            
        }

        

        
    }
 
}

