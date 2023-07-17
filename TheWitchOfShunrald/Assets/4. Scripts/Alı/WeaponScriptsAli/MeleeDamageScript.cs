using CihanAkpınar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDamageScript : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private bool isStaff;
    [SerializeField] private float throwBackForce;
    [SerializeField] private Rigidbody rb;
    private float playerDamage;

    private void Start()
    {
        playerDamage = GameManager.instance.Player.GetComponent<PlayerStats>().Damage;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if ((enemyLayer.value & (1 << collision.gameObject.layer)) != 0)
        {
            if (isStaff)
            {
                //collision.gameObject.GetComponent<Enemy>().TakeDamage(rb.velocity * throwBackForce, playerDamage / 8 * rb.velocity.magnitude / 20);
            }
            else
            {
                collision.gameObject.GetComponent<Enemy>().TakeDamage(rb.velocity * throwBackForce, playerDamage / 5 * rb.velocity.magnitude / 20);
            }
            
        }
    }
}
