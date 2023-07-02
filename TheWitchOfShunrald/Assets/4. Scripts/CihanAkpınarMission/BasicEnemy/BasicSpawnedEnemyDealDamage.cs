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
            if (other.gameObject.CompareTag("Shunrald"))
            {
                other.gameObject.transform.parent.gameObject.GetComponent<PlayerStats>().TakeDamage(basicDamage); 
                //Audio
                AudioManager.Instance.PlaySfx(AudioManager.Instance.basicSpawnedEnemyHitAudio);
            }
            else if (other.gameObject.layer==13)
            {
                other.gameObject.GetComponent<MoveableObjectScript>().MoveableObjectTakeDamage(basicMoveableObjectDamage);  
            }
        }
    } 
}

