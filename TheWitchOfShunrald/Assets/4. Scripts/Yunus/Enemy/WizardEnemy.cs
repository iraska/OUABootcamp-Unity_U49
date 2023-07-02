using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardEnemy : MonoBehaviour, Enemy
{
    private Animator anim;
    private Rigidbody rb;
    [SerializeField] private float attackRange;
    [SerializeField] private float speed;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float coolDown;
    [SerializeField] private float health;
    [SerializeField] private int damage;
    public int Damage { get { return damage; } }
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private ParticleSystem electric, explosion;
    [SerializeField] private Transform staff;
    private Transform playerTransform;
    private bool isAttackActivate = true;
    public enum EnemyState
    {
        attack,
        walk,
        injured
    }
    private EnemyState enemyState = EnemyState.walk;
    private IEnumerator Start()
    {
        electric.Stop();
        electric.transform.SetParent(null);
        explosion.transform.SetParent(null);
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        playerTransform = GameManager.instance.Player.transform;


        while(true)
        {
            if(Physics.Raycast(new Vector3(transform.position.x, 0.3f, transform.position.z), playerTransform.position - transform.position, out RaycastHit hitInfo, 200f))
            {
                Debug.Log(1);
                if(hitInfo.transform.CompareTag("Shunrald"))
                {
                    Debug.Log(2);
                    if ((transform.position - playerTransform.position).magnitude < attackRange)
                    {
                        if(isAttackActivate)
                        {
                            isAttackActivate = false;
                            Invoke(nameof(AttackActivate), coolDown);
                            transform.LookAt(playerTransform);
                            anim.SetBool("walk", false);
                            anim.SetTrigger("attack");                            
                            Debug.Log(3);
                        }
                    }
                    else
                    {
                        Debug.Log(4);
                        anim.SetBool("walk", true);
                        Vector3 direction = playerTransform.position - transform.position;
                        transform.LookAt(playerTransform);
                        direction.Normalize();
                        transform.position += direction * speed * Time.deltaTime;
                    }
                }
                else
                {
                    Debug.Log(5);
                    Vector3 randomPos;
                    Vector3 direction;
                    while (true)
                    {
                        randomPos = transform.position + 2 * transform.right + transform.forward;
                        direction = randomPos - transform.position;
                        if (Physics.Raycast(randomPos + Vector3.up, Vector3.down, 10f, groundLayer))
                        {
                            Debug.Log(6);
                            break;
                        }
                        else
                        {
                            Debug.Log(7);
                        }
                        yield return null;
                    }
                    anim.SetBool("walk", true);
                    transform.LookAt(randomPos);
                    direction.Normalize();
                    while(true)
                    {
                        if (Physics.Raycast(new Vector3(transform.position.x, 0.3f, transform.position.z), playerTransform.position - transform.position, out RaycastHit hit, 200f))
                        {
                            if (hit.transform.CompareTag("Shunrald"))
                            {
                                Debug.Log(8);
                                break;
                            }
                        }
                        if (Physics.Raycast(new Vector3(transform.position.x, 0.3f, transform.position.z), direction, 0.5f))
                        {
                            Debug.Log(9);
                            break;
                        }
                        else if (direction.magnitude > 0.5f)
                        {
                            Debug.Log(10);
                            transform.position += direction * speed * Time.deltaTime;
                        }
                        else
                        {
                            Debug.Log(11);
                            break;
                        }
                        yield return null;
                    }
                }
            }
            yield return null;
        }
    }
    private void AttackActivate()
    {
        isAttackActivate = true;
    }
    public void Attack()
    {
        StartCoroutine(Attackk());
    }
    public IEnumerator Attackk()
    {
        electric.transform.position = staff.position;
        electric.gameObject.SetActive(true);
        electric.Play();
        Vector3 targetPos = playerTransform.position + Vector3.up * 0.5f;
        while (true)
        {
            Vector3 direction = targetPos - electric.transform.position;
            if (direction.magnitude > 0.5f)
                electric.transform.position += direction * attackSpeed * Time.deltaTime;
            else
            {
                electric.gameObject.SetActive(false);
                explosion.transform.position = targetPos;
                explosion.gameObject.SetActive(true);
                explosion.Play();
                yield return new WaitForSeconds(0.3f);
                explosion.gameObject.SetActive(false);
                break;
            }
            yield return null;
        }
    }
    void Enemy.TakeDamage(Vector3 exploLocation, float damage)
    {
        anim.SetTrigger("injured");
        Vector3 jumpDirection = ((transform.position - exploLocation) + Vector3.up).normalized * 3;
        rb.AddForce(3 * jumpDirection, ForceMode.VelocityChange);
        health -= damage;
        if (health <= 0)
        {
            //DropMath();
            Destroy(electric);
            Destroy(explosion);
            Destroy(gameObject);
        }
    }
}
