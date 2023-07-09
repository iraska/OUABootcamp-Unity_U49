using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sidar
{
    public class NecroProjectile : MonoBehaviour
    {
        [SerializeField] private float speed = 3f;
        [SerializeField] private int playerDamage = 10;
        [SerializeField] private float objectDamage = 20f;
        [SerializeField] private float splashRadius = 5f;
        [SerializeField] private GameObject splashEffect;
		[SerializeField] private ParticleSystem blood, hit;
        [SerializeField] private float destroyDelay = 3f;
        private Transform player;
        private bool isSingle = false;
        private float destroyTimer;

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
            if(collision.gameObject.tag != "Enemy"){
                destroyTimer = 0f;
				blood.Stop();
                Destroy(gameObject);
	
                Instantiate(splashEffect, collision.contacts[0].point, Quaternion.identity);

                Collider[] colliders = Physics.OverlapSphere(collision.contacts[0].point, splashRadius);
                foreach (Collider collider in colliders)
                {
                    if (collider.gameObject.tag == "Shunrald")
                    {
                        player.GetComponent<PlayerStats>().TakeDamage(playerDamage);
                    }
                    /*else if (collider.gameObject.tag == "CanBeMoved")
                    {
                        collider.GetComponent<MoveableObjectScript>().MoveableObjectTakeDamage(objectDamage);
                    }*/
                }
            }
        }
    }
}
