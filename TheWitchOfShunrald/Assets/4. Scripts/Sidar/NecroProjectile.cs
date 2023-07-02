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

        [SerializeField] private float destroyDelay = 3f;
        private Transform player;
        private bool isSingle = false;
        private float destroyTimer;

        public void Shoot(Vector3 direction)
        {
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
                Vector3 direction = (player.position - transform.position).normalized;
                transform.Translate(direction * speed * Time.deltaTime);
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

                Destroy(gameObject);

                Instantiate(splashEffect, collision.contacts[0].point, Quaternion.identity);

                Collider[] colliders = Physics.OverlapSphere(collision.contacts[0].point, splashRadius);
                foreach (Collider collider in colliders)
                {
                    if (collider.gameObject.tag == "Shunrald")
                    {
                        collider.GetComponent<PlayerStats>().TakeDamage(playerDamage);
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
