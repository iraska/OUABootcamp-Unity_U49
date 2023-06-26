using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace CihanAkpınar
{
    public class BasicSpawnedEnemyAi : MonoBehaviour
    {
        
        
        private Transform player;
        private Transform findedTarget;
        
        private Rigidbody rb;
        private Animator anim;
        
        private bool isWalking;
        private bool isTriggered;
        
        private float isBasicEnemyRuning;
        private float isBasicEnemyAttacking;

        [SerializeField] private GameObject basicEnemyDiePart;
        
        [SerializeField] private float lookSpawnedEnemyRadius = 10f;
        [SerializeField] private float bombPower;
        [SerializeField] private float basicStopingDistance;
        [SerializeField] private float basicEnemyHealth;
        [SerializeField] private float basicEnemyMovementSpeed;
        
        [SerializeField] private int targetLayer;


        void Start()
        {
            rb = GetComponent<Rigidbody>();
            anim = GetComponentInChildren<Animator>();
            player = GameManager.instance.Player.transform;
            findedTarget = player;
        }
        
        void FixedUpdate()
        {
            BasicEnemyAnim();
            if (isTriggered)
            {
                FindEnemyTarget(); 
            }
            
        }
        private void Update()
        {
            FaceTarget();
        }
        

        void FaceTarget()
        {
            Vector3 direction = (findedTarget.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 6f);
        }
        
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, lookSpawnedEnemyRadius);
        }
        

        void BasicEnemyAnim()
        {
            
            float distance = Vector3.Distance(findedTarget.position, transform.position);
            if (distance<=lookSpawnedEnemyRadius)
            {
                isTriggered = true;
                //Vector3 targetPlayerPosition=new Vector3(target.position.x,0,target.position.z);
                Vector3 direction = findedTarget.position - transform.position;
                Vector3 desiredVelocity = direction.normalized * basicEnemyMovementSpeed;
                Vector3 velocityChange = desiredVelocity - rb.velocity;
                velocityChange=new Vector3(velocityChange.x,rb.velocity.y,velocityChange.z);

                anim.SetFloat("BasicEnemyMove",1f,0.3f,Time.deltaTime);
                if (distance <= basicStopingDistance) 
                {
                    anim.SetBool("BasicEnemyAttacking",true);
                    isWalking = false;

                }
                else
                {
                    anim.SetBool("BasicEnemyAttacking",false);
                    rb.MovePosition(rb.position+velocityChange*Time.fixedDeltaTime);
                    isWalking = true;
                }
            }
            else
            {
                anim.SetFloat("BasicEnemyMove",0f,0.3f,Time.deltaTime);
                isTriggered = false;
            }

            /*if (isWalking=true)
            {
                anim.SetFloat("BasicEnemyMove",rb.velocity.magnitude,0.3f,Time.deltaTime);
            }*/
            
        }
        

        private void FindEnemyTarget()
        {
            RaycastHit hit;
            if (Physics.Raycast(new Vector3(transform.position.x,1f,transform.position.z), player.position - transform.position, out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject == player.gameObject)
                {
                    // If the raycast hits the player, set the player as the target
                    findedTarget = player;
                }
                else if (hit.collider.gameObject.layer == targetLayer)
                {
                    // If the raycast hits an object in the target layer, set the hit object as the target
                    findedTarget = hit.collider.transform;
                }
            }
        }
        
        
        public void TakeDamage(Vector3 exploLocation,float damage)
        {
            
        
            Vector3 jumpDirection=((transform.position-exploLocation)+Vector3.up).normalized*bombPower;
            rb.AddForce(bombPower * jumpDirection, ForceMode.VelocityChange);
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

