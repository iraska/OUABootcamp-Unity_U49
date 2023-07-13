using System.Collections;
using Shunrald;
using UnityEngine;
using UnityEngine.AI;

namespace Sidar
{
    public class NecroAI : MonoBehaviour, Enemy
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
        private bool isAttacking;
        private bool canMultiProjectile = true;
        public Transform projectilePointParent;
        public GameObject projectilePrefab;
        private Transform[] projectilePoints;
        [SerializeField] private float projectileAnimDelay = 1f;
        [SerializeField] private float multiProjectileTimeBetween = 1f;
        [SerializeField] private float stunAnimDelay = 6f;
        [SerializeField] private float spawnAnimDelay = 3.9f;
        [SerializeField] private float healthAnimDelay = 6.25f;  
        [SerializeField] private float projectileCooldown = 5f;
        [SerializeField] private float stunCooldown = 30f;
        [SerializeField] private float healthCooldown = 40f;
        [SerializeField] private float spawnCooldown = 20f;
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private float rotationSpeed = 3f;
        [SerializeField] private float regenSpeedMin = 1f;
        [SerializeField] private float regenSpeedMax = 5f;
        private float regenSpeed;
        [SerializeField] private ParticleSystem healthRegenParticle;
        
        
        
        [SerializeField] private GameObject enemyClonePrefab; // The prefab for the enemy clone
        [SerializeField] private int numClones = 8; // Number of clones to spawn
        [SerializeField] private float radius = 5f; // Radius of the circle
        [SerializeField] private float spawnDelay = 1f; // Delay between each clone spawn

        private float angleIncrement; // Angle increment between clones
        private float spawnTimer; // Timer to control spawn timing
        private bool isRegenDone = false;
        private bool isRegenInterrupt = false;
        private int explosionsCount;
        private int maxExplosions = 4;
        private float explosionAttackDelay = 4f;
        private bool isMultiProjectile;

        [SerializeField] private float damage;
        [SerializeField] private float playerDamage;
        [SerializeField] private float objectDamage;

        // Start is called before the first frame update
        private void Start()
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
            angleIncrement = 360f / numClones;
            spawnTimer = spawnDelay;
        }

        // Update is called once per frame
        private void Update()
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
                                else if(currentHealth < 50){
                                    StartCoroutine(HealthRegen());
                                }
                            }
                            break;
                    }
                }
                else{
                        if (isMultiProjectile)
                        {
                            ChasePlayer();
                        }
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
                                    if(currentHealth < 20)
                                    {
                                        SpawnClone();
                                        StartCoroutine(HealthRegen());
                                    }
                                    else if(currentHealth < 50){
                                        StartCoroutine(HealthRegen());
                                    }
                                }

                                break;
                        }
                }
            }
            
            

        }

        private void LookAtPlayer()
        {
            transform.LookAt(player.transform);
        }

        private void ChasePlayer(){
                agent.SetDestination(player.transform.position);
                animator.SetBool("isChasing", true);
        }


        private IEnumerator StayIdle(){
            canCrIdle = false;
            agent.SetDestination(transform.position);
            animator.SetBool("isIdle", true);
            yield return new WaitForSeconds(3f);
        }

        private IEnumerator MutliProjectileAttack()
        {
            isMultiProjectile = true;
            int count = 0;
            LookAtPlayer();
            agent.SetDestination(transform.position);
            canCrMultiProjectile = false;
            isAttacking = true;
            animator.SetBool("isMutliProjectileAttack",true);
            yield return new WaitForSeconds(projectileAnimDelay);
            animator.SetBool("isMutliProjectileAttack", false);
            while (true)
            {
                if(distance>attackRange)
                    ChasePlayer();
                else
                {
                    transform.LookAt(player.transform);
                    agent.SetDestination(transform.position);
                    animator.SetBool("isChasing", false);
                }
                    
                if (canMultiProjectile)
                {
                    canMultiProjectile = false;
                    CircleProjectile();
                    Invoke(nameof(ResetMultiProjectile), multiProjectileTimeBetween);
                    count++;
                }
                if(count == 10)
                    break;
                yield return null;
            }
            isAttacking = false;
            yield return new WaitForSeconds(projectileCooldown);
            canCrMultiProjectile = true;
        }


        private void ResetMultiProjectile()
        {
            canMultiProjectile = true;
        }

        private IEnumerator SingleProjectileAttack(){
            LookAtPlayer();
            agent.SetDestination(transform.position);
            canCrSingleProjectile = false;
            isAttacking = true;
            animator.SetBool("isSingleProjectileAttack",true);
            yield return new WaitForSeconds(2f);
            isAttacking = false;
            animator.SetBool("isSingleProjectileAttack", false);
            SingleProjectile();
            yield return new WaitForSeconds(projectileCooldown);
            canCrSingleProjectile = true;
        }

        private void SingleProjectile()
        {
            GameObject projectile = Instantiate(projectilePrefab, projectilePointParent.GetChild(4).position, Quaternion.identity);
            NecroProjectile projectileComponent = projectile.GetComponent<NecroProjectile>();
            projectileComponent.SetIsSingle(true);
            projectileComponent.PlayerDamage = playerDamage;
            projectileComponent.ObjectDamage = objectDamage;
        }

        private void CircleProjectile()
        {
            
            foreach (var pj in projectilePoints)
            {
                Vector3 direction = (pj.position - transform.position).normalized;
                GameObject projectile = Instantiate(projectilePrefab, pj.position, Quaternion.identity);
                NecroProjectile projectileComponent = projectile.GetComponent<NecroProjectile>();
                projectileComponent.Shoot(direction);
                projectileComponent.PlayerDamage = playerDamage;
                projectileComponent.ObjectDamage = objectDamage;
            }
        }

        /*
        private IEnumerator areaAttack()
        {
            isMultiProjectile = true;
            int count = 0;
            canCrAreaAttack = false;
            isAttacking = true;
            animator.SetBool("isMutliProjectileAttack",true);
            yield return new WaitForSeconds(projectileAnimDelay);
            animator.SetBool("isMutliProjectileAttack", false);
            while (true)
            {
                if(distance>attackRange)
                    ChasePlayer();
                else
                {
                    transform.LookAt(player.transform);
                    agent.SetDestination(transform.position);
                    animator.SetBool("isChasing", false);
                }
                    
                if (canMultiProjectile)
                {
                    canMultiProjectile = false;
                    CircleProjectile();
                    Invoke(nameof(ResetMultiProjectile), multiProjectileTimeBetween);
                    count++;
                }
                if(count == 10)
                    break;
                yield return null;
            }
            isAttacking = false;
            yield return new WaitForSeconds(projectileCooldown);
            canCrMultiProjectile = true;
        }
        */


        private IEnumerator SpawnMinions(){
            LookAtPlayer();
            agent.SetDestination(transform.position);
            animator.SetBool("isChasing", false);
            canCrSpawnMinions = false;
            animator.SetBool("isSpawningMinions",true);
            isAttacking = true;
            yield return new WaitForSeconds(spawnAnimDelay);
            SpawnClone();
            isAttacking = false;
            animator.SetBool("isSpawningMinions", false);
            yield return new WaitForSeconds(spawnCooldown);
            canCrSpawnMinions = true;
        }
        private void SpawnClone()
        {
            Vector3 bossPosition = transform.position;

            for (int i = 0; i < numClones; i++)
            {
                float angle = angleIncrement * i;
                float xPos = radius * Mathf.Cos(Mathf.Deg2Rad * angle) + bossPosition.x;
                float zPos = radius * Mathf.Sin(Mathf.Deg2Rad * angle) + bossPosition.z;

                Vector3 clonePosition = new Vector3(xPos, bossPosition.y, zPos);

                // Instantiate the clone GameObject at the calculated position
                GameObject clone = Instantiate(enemyClonePrefab, clonePosition, Quaternion.identity);

                // Set the forward direction of the clone to face outward
                Vector3 direction = bossPosition - clonePosition;
                clone.transform.forward = direction.normalized;
            }
        }

        private IEnumerator StunPlayer(){
            LookAtPlayer();
            agent.SetDestination(transform.position);
            animator.SetBool("isChasing", false);
            canCrStunPLayer = false;
            animator.SetBool("isStunPlayer",true);
            isAttacking = true;
            StartCoroutine(player.GetComponent<ShunraldHangInTheAir>().HangInTheAir());
            yield return new WaitForSeconds(stunAnimDelay);
            isAttacking = false;
            animator.SetBool("isStunPlayer", false);
            StartCoroutine(player.GetComponent<ShunraldHangInTheAir>().ReleasesTheWitch());
            yield return new WaitForSeconds(stunCooldown);
            canCrStunPLayer = true;
        }


        private IEnumerator HealthRegen(){
            LookAtPlayer();
            agent.SetDestination(transform.position);
            animator.SetBool("isChasing", false);
            canCrHealthRegen = false;
            animator.SetBool("isHealthRegen",true);
            isAttacking = true;
            if (!healthRegenParticle.isPlaying)
            {
                healthRegenParticle.Play();
            }
            while (true)
            {
                RegenerateHealth();
                if (isRegenDone || isRegenInterrupt)
                {
                    if (healthRegenParticle.isPlaying)
                    {
                        healthRegenParticle.Stop();
                    }
                    animator.SetBool("isHealthRegen", false);
                    isRegenDone = false;
                    isRegenInterrupt = false;
                    break;
                }
                yield return null;
            }
            //yield return new WaitForSeconds(healthAnimDelay);
            isAttacking = false;
            yield return new WaitForSeconds(healthCooldown);
            canCrHealthRegen = true;
        }

        private void RegenerateHealth()
        {
            if (currentHealth >= maxHealth)
            {
                currentHealth = 100;
                isRegenDone = true;
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
            var healthPercentage = currentHealth / maxHealth;
            regenSpeed = Mathf.Lerp(regenSpeedMin, regenSpeedMax, healthPercentage);
        }

        public void TakeDamage(int damage)
        {
            isRegenInterrupt = true;
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

        public void TakeDamage(Vector3 exploLocation, float damage)
        {
            isRegenInterrupt = true;
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        public float Health()
        {
            return currentHealth;
        }

        public void SetEnemyStats(float health, float damage)
        {
            currentHealth = health;
        }
    }
}




