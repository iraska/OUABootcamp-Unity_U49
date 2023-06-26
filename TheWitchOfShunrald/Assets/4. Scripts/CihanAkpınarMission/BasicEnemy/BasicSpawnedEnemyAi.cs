using System.Collections;
using Unity.Mathematics;
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
        private float basicEnemyVelocitySpeed;

        [SerializeField] private GameObject basicEnemyDiePart;
        [SerializeField] private GameObject basicManaPot;
        [SerializeField] private GameObject basicHealthPot;
        
        [SerializeField] private float lookSpawnedEnemyRadius = 10f;
        [SerializeField] private float bombPower;
        [SerializeField] private float basicStopingDistance;
        [SerializeField] private float basicEnemyHealth;
        [SerializeField] private float basicEnemyMovementSpeed;
        
        
        [SerializeField] private int targetLayer;
        [SerializeField] private int healthPotProbability;
        [SerializeField] private int manaPotProbability;
        
        private int mainProbability;


        void Start()
        {
            isWalking = true;
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
            basicEnemyVelocitySpeed = rb.velocity.magnitude;
            basicEnemyVelocitySpeed = basicEnemyVelocitySpeed - 1.4f;
            if (basicEnemyVelocitySpeed<0)
            {
                basicEnemyVelocitySpeed = 0;
                    
            }

            basicEnemyVelocitySpeed *= 2;

            if (basicEnemyVelocitySpeed>1)
            {
                basicEnemyVelocitySpeed = 1;
            }
            anim.SetFloat("BasicEnemyMove",basicEnemyVelocitySpeed,0.3f,Time.deltaTime);

            float distance = Vector3.Distance(findedTarget.position, transform.position);
            if (distance<=lookSpawnedEnemyRadius)
            {
                isTriggered = true;
                //Vector3 targetPlayerPosition=new Vector3(target.position.x,0,target.position.z);
                Vector3 direction = findedTarget.position - transform.position;
                
                
                

                if (distance <= basicStopingDistance) 
                {
                    anim.SetBool("BasicEnemyAttacking",true);
                    StartCoroutine(AttackDelay());
                }
                else
                {
                    anim.SetBool("BasicEnemyAttacking",false);
                    if (isWalking)
                    {
                        Vector3 newVelocityValue=new Vector3(direction.normalized.x* Time.fixedDeltaTime*basicEnemyMovementSpeed,rb.velocity.y,direction.normalized.z* Time.fixedDeltaTime*basicEnemyMovementSpeed);
                        rb.velocity = newVelocityValue;
                    }
                    
                }
            }
            else
            {
                //anim.SetFloat("BasicEnemyMove",0f,0.3f,Time.deltaTime);
                isTriggered = false;
            }

            IEnumerator AttackDelay()
            {
                isWalking = false;
                yield return new WaitForSeconds(1.5f);
                isWalking = true;
            }

            
            
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
                DropMath();
                Instantiate(basicEnemyDiePart, new Vector3(transform.position.x, transform.position.y, transform.position.z),Quaternion.identity);
                GameObject.Destroy(gameObject);
            }

        }
        
        
        void DropMath()
        {
            mainProbability = UnityEngine.Random.Range(1, 101);
            if (mainProbability<=healthPotProbability)
            {
                Instantiate(basicHealthPot, transform.position, quaternion.identity);
            }
            else if (healthPotProbability<mainProbability && mainProbability<=healthPotProbability+manaPotProbability)
            {
                Instantiate(basicManaPot, transform.position, quaternion.identity);  
            }

        }




    }
 
}

