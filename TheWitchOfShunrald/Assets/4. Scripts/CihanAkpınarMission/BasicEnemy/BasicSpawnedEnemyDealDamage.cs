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
        [SerializeField] private float damage;
        [SerializeField] private float basicMoveableObjectDamage;

        public float Damage { set { damage = value; } }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Shunrald"))
            {
                other.gameObject.transform.parent.gameObject.GetComponent<PlayerStats>().TakeDamage(damage); 
                //Audio
                AudioManager.Instance.PlaySfx(AudioManager.Instance.basicSpawnedEnemyHitAudio,transform.position);
            }
            else if (other.gameObject.layer==13)
            {
                other.gameObject.GetComponent<MoveableObjectScript>().MoveableObjectTakeDamage(basicMoveableObjectDamage);  
            }
        }
    } 
}

