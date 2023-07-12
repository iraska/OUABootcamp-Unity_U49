using ali;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardEnemyProjectile : MonoBehaviour
{
    private float damage;
    private PlayerStats playerStats;
    private void Awake()
    {
        damage = GetComponentInParent<WizardEnemy>().Damage;
    }
    private void Start()
    {
        playerStats = GameManager.instance.Player.GetComponent<PlayerStats>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Shunrald"))
        {
            playerStats.TakeDamage(damage);
        }
        if(other.gameObject.layer == 13)
        {
            other.GetComponent<MoveableObjectScript>().MoveableObjectTakeDamage(damage * 3);
        }
    }
}
