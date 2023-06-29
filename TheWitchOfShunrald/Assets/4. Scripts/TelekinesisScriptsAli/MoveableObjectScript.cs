using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ali
{
    public class MoveableObjectScript : MonoBehaviour
    {
        private bool isObjectPointed;
        [SerializeField] private float obstacleHealth;

        public float ObstacleHealth { get { return obstacleHealth; } set { obstacleHealth = value; } }
        public bool IsObjectPointed
        {
            get { return isObjectPointed; }
            set
            {
                isObjectPointed = value;
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                MoveableObjectTakeDamage(33f);
            }
        }

        public void MoveableObjectTakeDamage(float damage)
        {
            obstacleHealth -= damage;
            if (obstacleHealth < 0) { DestroyObject(); }
        }

        // Destroy object
        public MeshRenderer wholeCrate;
        public BoxCollider boxCollider;
        public GameObject fracturedCrate;

        private void DestroyObject()
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            wholeCrate.enabled = false;
            boxCollider.enabled = false;
            fracturedCrate.SetActive(true);
        }
    }
}

