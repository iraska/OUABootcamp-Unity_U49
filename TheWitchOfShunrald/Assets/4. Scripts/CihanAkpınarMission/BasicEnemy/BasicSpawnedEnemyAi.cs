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
        [SerializeField] private  float lookSpawnedEnemyRadius = 10f;
        
        Transform target;
        NavMeshAgent agent;
        private Rigidbody rb;

        [SerializeField] private GameObject basicEnemyDiePart;
        
        private float isBasicEnemyRuning;
        private float isBasicEnemyAttacking;
        private Animator anim;

        [SerializeField] private float bombPower;

        

        [SerializeField] private float basicEnemyHealth;
        


        void Start()
        {
            rb = GetComponent<Rigidbody>();
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
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 600f);
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
                    anim.SetBool("BasicEnemyAttacking",true);
                    StartCoroutine(AttackDelay());
                    
                }
                else
                {
                    anim.SetBool("BasicEnemyAttacking",false);
                }
            }
            else
            {
                anim.SetFloat("BasicEnemyMove",0f,0.3f,Time.deltaTime);
            }
            
        }
        IEnumerator AttackDelay () {
            //hareketi durdur
            agent.enabled=false;
            yield return new WaitForSeconds (2f);
            agent.enabled = true;
            //hareketi devam ettir
        }

        IEnumerator EnemyStan()
        {
            agent.enabled=false;
            yield return new WaitForSeconds(1f);
            agent.enabled = true;

        }
        public void TakeDamage(Vector3 exploLocation,float damage)
        {
            StartCoroutine(EnemyStan());
        
            Vector3 jumpDirection=((transform.position-exploLocation)+Vector3.up).normalized*bombPower;
            rb.velocity=(jumpDirection);
            basicEnemyHealth -= damage;
            if (basicEnemyHealth<=0)
            {
                anim.SetTrigger("BasicEnemyDie");
                GameObject.Destroy(gameObject);
                Instantiate(basicEnemyDiePart, new Vector3(transform.position.x, transform.position.y, transform.position.z),Quaternion.identity);
            }

        }




    }
 
}

