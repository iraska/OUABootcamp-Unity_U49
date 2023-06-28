using System;
using System.Collections;
using System.Collections.Generic;
using ali;
using CihanAkpınar;
using UnityEngine;

namespace CihanAkpınar
{
    public class BasicSpawnedEnemyDealDamage : MonoBehaviour
    {
        [SerializeField] int basicDamage;
        [SerializeField] private float basicMoveableObjectDamage;
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.gameObject.name);
            if (other.gameObject.transform.parent.CompareTag("Player"))
            {
                other.gameObject.transform.parent.gameObject.GetComponent<PlayerStats>().TakeDamage(basicDamage);  
            }
            else if (other.gameObject.layer==13)
            {
                other.gameObject.GetComponent<MoveableObjectScript>().MoveableObjectTakeDamage(basicMoveableObjectDamage);  
            }
        }
    } 
}

