using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Sidar
{
    public class NecroAI : MonoBehaviour
    {
        private GameObject player;
        private Animator animator;
        private int[] repeat;
        [SerializeField] private float attackRange = 10f;
        [SerializeField] private int random;
        [SerializeField] private float distance;
        private NavMeshAgent agent;
        [SerializeField] private float delay = 10f;
        [SerializeField] private float currentHealth;
        private bool canCrIdle = true; //You may remove this
        private bool canCrMultiProjectile = true;
        private bool canCrSpawnMinions = true;
        private bool canCrStunPLayer = true;
        private bool canCrHealthRegen = true;
        private bool canCrSingleProjectile = true;
        private bool isAttacking = false;
        public Transform projectilePointParent;
        public GameObject projectilePrefab;
        private Transform[] projectilePoints;
        [SerializeField] private float projectileAnimDelay = 4.9f;
        [SerializeField] private float stunAnimDelay = 6f;
        [SerializeField] private float spawnAnimDelay = 3.9f;
        [SerializeField] private float healthAnimDelay = 6.25f;  
        [SerializeField] private float projectileCooldown = 5f;
        [SerializeField] private float stunCooldown = 30f;
        [SerializeField] private float healthCooldown = 40f;
        [SerializeField] private float spawnCooldown = 20f;
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private float rotationSpeed = 3f;
        [SerializeField] private float regenSpeedMin = 5f;
        [SerializeField] private float regenSpeedMax = 20f;
        private float regenSpeed;

        

        

        // Start is called before the first frame update
        void Start()
        {
            currentHealth = maxHealth;
            player = GameObject.FindWithTag("Shunrald");
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            repeat = new int[7];
            projectilePoints = new Transform[projectilePointParent.childCount];
            for (int i = 0; i < projectilePointParent.childCount; i++)
            {
                projectilePoints[i] = projectilePointParent.GetChild(i);
            }
        }

        // Update is called once per frame
        void Update()
        {
           
            distance = Vector3.Distance(transform.position, player.transform.position);
                if(!isAttacking){
                    if(distance > attackRange){
                    random = Random.Range(1,4);
                    switch(random){
                        case 1:
                            ChasePlayer();
                            break;
                        case 2:
                            if(canCrSpawnMinions){
                                StartCoroutine(SpawnMinions());
                            }
                            break;
                        case 3:
                            if(canCrHealthRegen){
                                if(currentHealth < 20){
                                    StartCoroutine(SpawnMinions());
                                    StartCoroutine(HealthRegen());
                                }
                                else if(currentHealth < 80){
                                    StartCoroutine(HealthRegen());
                                }
                                RegenerateHealth();
                            }
                            break;
                        default:
                            break;
                    }
                }
                else{
                    agent.SetDestination(transform.position);
                    animator.SetBool("isChasing", false);
                    random = Random.Range(1,6);
                    switch(random){
                        case 1:
                            if(canCrMultiProjectile){
                                
                                StartCoroutine(MutliProjectileAttack());
                            }
                            break;
                        case 2:
                            if(canCrSingleProjectile){
                                
                                StartCoroutine(SingleProjectileAttack());
                            }
                            break;
                        case 3:
                            if(canCrStunPLayer){
                                StartCoroutine(StunPlayer());
                            }
                            break;
                        case 4:
                            if(canCrSpawnMinions){
                                StartCoroutine(SpawnMinions());
                            }
                            break;
                        case 5:
                            if(canCrHealthRegen){
                                if(currentHealth < 20){
                                    StartCoroutine(SpawnMinions());
                                    StartCoroutine(HealthRegen());
                                }
                                else if(currentHealth < 50){
                                    StartCoroutine(HealthRegen());
                                }
                                
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            
            

        }

        private void LookAtPlayer()
        {
            transform.LookAt(player.transform);
        }

        void ChasePlayer(){
             if(!isAttacking){
                agent.SetDestination(player.transform.position);
                animator.SetBool("isChasing", true);
            }
        }


        IEnumerator StayIdle(){
            canCrIdle = false;
            agent.SetDestination(transform.position);
            animator.SetBool("isIdle", true);
            yield return new WaitForSeconds(3f);
        }

        IEnumerator MutliProjectileAttack(){
            LookAtPlayer();
            agent.SetDestination(transform.position);
            canCrMultiProjectile = false;
            isAttacking = true;
            animator.SetBool("isMutliProjectileAttack",true);
            yield return new WaitForSeconds(projectileAnimDelay);
            isAttacking = false;
            animator.SetBool("isMutliProjectileAttack", false);
            circleProjectile();
            yield return new WaitForSeconds(projectileCooldown);
            canCrMultiProjectile = true;
        }

        IEnumerator SingleProjectileAttack(){
            LookAtPlayer();
            agent.SetDestination(transform.position);
            canCrSingleProjectile = false;
            isAttacking = true;
            animator.SetBool("isSingleProjectileAttack",true);
            yield return new WaitForSeconds(2f);
            isAttacking = false;
            animator.SetBool("isSingleProjectileAttack", false);
            singleProjectile();
            yield return new WaitForSeconds(projectileCooldown);
            canCrSingleProjectile = true;
        }

        void singleProjectile(){
            Vector3 direction = (projectilePointParent.GetChild(4).position - transform.position).normalized;
            GameObject projectile = Instantiate(projectilePrefab, projectilePointParent.GetChild(4).position, Quaternion.identity);
            projectile.GetComponent<NecroProjectile>().SetIsSingle(true);
        }

        void circleProjectile(){
              for (int i = 0; i < projectilePoints.Length; i++)
            {
                Vector3 direction = (projectilePoints[i].position - transform.position).normalized;
                GameObject projectile = Instantiate(projectilePrefab, projectilePoints[i].position, Quaternion.identity);
                projectile.GetComponent<NecroProjectile>().Shoot(direction);
            }
        }


        IEnumerator SpawnMinions(){
            LookAtPlayer();
            agent.SetDestination(transform.position);
            animator.SetBool("isChasing", false);
            canCrSpawnMinions = false;
            animator.SetBool("isSpawningMinions",true);
            isAttacking = true;
            yield return new WaitForSeconds(spawnAnimDelay);
            isAttacking = false;
            animator.SetBool("isSpawningMinions", false);
            yield return new WaitForSeconds(spawnCooldown);
            canCrSpawnMinions = true;
        }

        IEnumerator StunPlayer(){
            LookAtPlayer();
            agent.SetDestination(transform.position);
            animator.SetBool("isChasing", false);
            canCrStunPLayer = false;
            animator.SetBool("isStunPlayer",true);
            isAttacking = true;
            yield return new WaitForSeconds(stunAnimDelay);
            isAttacking = false;
            animator.SetBool("isStunPlayer", false);
            yield return new WaitForSeconds(stunCooldown);
            canCrStunPLayer = true;
        }


        IEnumerator HealthRegen(){
            LookAtPlayer();
            agent.SetDestination(transform.position);
            animator.SetBool("isChasing", false);
            canCrHealthRegen = false;
            animator.SetBool("isHealthRegen",true);
            isAttacking = true;
            yield return new WaitForSeconds(healthAnimDelay);
            isAttacking = false;
            animator.SetBool("isHealthRegen", false);
            yield return new WaitForSeconds(healthCooldown);
            canCrHealthRegen = true;
        }
        
        private void RegenerateHealth()
        {
            if (currentHealth >= maxHealth)
            {
                return;
            }

            float regenAmount = regenSpeed * Time.deltaTime;
            currentHealth += regenAmount;
            currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

            UpdateRegenSpeed();
        }

        private void UpdateRegenSpeed()
        {
            // Adjust the regeneration speed based on the current health percentage
            float healthPercentage = currentHealth / maxHealth;
            regenSpeed = Mathf.Lerp(regenSpeedMin, regenSpeedMax, healthPercentage);
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Debug.Log("Boss defeated!");
        }
    }
}




