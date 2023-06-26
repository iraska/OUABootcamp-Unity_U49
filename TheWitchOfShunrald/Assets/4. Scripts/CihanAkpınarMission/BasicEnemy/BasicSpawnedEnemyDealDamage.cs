using System;
using System.Collections;
using System.Collections.Generic;
using CihanAkpınar;
using UnityEngine;

namespace CihanAkpınar
{
    public class BasicSpawnedEnemyDealDamage : MonoBehaviour
    {
        [SerializeField] int basicDamage;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject==GameManager.instance.Player)
            {
                other.gameObject.GetComponent<PlayerStats>().TakeDamage(basicDamage);  
            }
        }
    } 
}

