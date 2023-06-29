using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CihanAkpınar
{
    public class BasicSpawnedEnemyHealthBar : MonoBehaviour
    {
        public GameObject basicEnemy; // Karakterin referansı
        private float startingHealth;
        [SerializeField] private GameObject pivot; 

        void Start()
        {
            startingHealth = basicEnemy.GetComponent<BasicSpawnedEnemyAi>().basicEnemyHealth;

        }

        void Update()
        { 
            changHealthValue();
            // Karakterin rotasyonunu düzelt
            transform.rotation = Quaternion.Euler(0f, -135f, 0f);

        }

        void changHealthValue()
        {
            // Karakterin can değerini al
            float healthPercentage = basicEnemy.GetComponent<BasicSpawnedEnemyAi>().basicEnemyHealth;
            // Can göstergesinin boyutunu güncelle
            pivot.transform.localScale = new Vector3(healthPercentage / startingHealth,pivot.transform.localScale.y,1);
            
            
        }
        
    }
 
}
