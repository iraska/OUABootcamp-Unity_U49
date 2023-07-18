using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ali;
using CihanAkpınar;

namespace Sidar
{
    public class NecroProjectile : MonoBehaviour
    {
        private float speed;
        private float playerDamage;
        private float objectDamage;
        [SerializeField] private float splashRadius = 0.5f;
        [SerializeField] private GameObject splashEffect;
		[SerializeField] private ParticleSystem blood, hit;
        [SerializeField] private float destroyDelay = 6f;
        private Transform player;
        private bool isSingle = false;
        private float destroyTimer;

        public float PlayerDamage
        {
            get => playerDamage;
            set => playerDamage = value;
        }
        public float ObjectDamage
        {
            get => objectDamage;
            set => objectDamage = value;
        }

        public float Speed
        {
            get => speed;
            set => speed = value;
        }

        public void Shoot(Vector3 direction)
        {
			
			blood.Play();
            GetComponent<Rigidbody>().velocity = direction * speed;
        }

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Shunrald").transform;
            destroyTimer = 0f;
        }
        

        private void Update()
        {
            destroyTimer += Time.deltaTime;
            if (destroyTimer >= destroyDelay)
            {
                Destroy(gameObject);
            }

            if (isSingle)
            {
                Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z);
                Vector3 direction = (targetPosition - transform.position).normalized;
                transform.position += direction * (speed * Time.deltaTime);

            }
        }


        public void SetIsSingle(bool param)
        {
            isSingle = param;
        }
        

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.layer != 11){
                destroyTimer = 0f;
				blood.Stop();
                Destroy(gameObject);
	
                Instantiate(splashEffect, collision.contacts[0].point, Quaternion.identity);

                Collider[] colliders = Physics.OverlapSphere(collision.contacts[0].point, splashRadius);
                foreach (Collider collider in colliders)
                {
                    if (collider.gameObject.CompareTag("Shunrald"))
                    {
                        AudioManager.Instance.PlaySfx(AudioManager.Instance.splash, player.transform.position);
                        player.GetComponent<PlayerStats>().TakeDamage(playerDamage);
                    }
                    else if (collider.gameObject.layer == 13)
                    {
                        collider.GetComponent<MoveableObjectScript>().MoveableObjectTakeDamage(objectDamage);
                    }
                }
            }
        }
    }
}
