using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CihanAkpınar
{
   public class BasicEnemySpawner : MonoBehaviour
   {
       [System.Serializable]
       

       //Dalga dizisi altına dizi ekliyebilmek için yeni class kullandım
       public class WaveContent
       {
           [SerializeField][NonReorderable] GameObject[] basicEnemySpawner;
   
           public GameObject[] GetEnemySpawnList()
           {
               return basicEnemySpawner;
           }
           
       }
   
       [SerializeField][NonReorderable] WaveContent[] waves;
       int currentWave = 0;
       [SerializeField] private float basicEnemyLookRadiusNew;
       [SerializeField] float spawnRange = 10;
       [SerializeField] float spawnSecond=3f;
       public List<GameObject> currentEnemy;
       
       void Start()
       {
           SpawnWave();
       }
       
       void Update()
       {
           if(currentEnemy.Count == 0)
           {
                if (currentWave < waves.Length) 
                {
                    SpawnWave();
                }
                else
                {
                    Destroy(gameObject);
                }
               
           }
           
       }
   
       void SpawnWave()
       {
           StartCoroutine(SpawmDelay());

       }
   
       Vector3 FindSpawnLoc()
       {
           Vector3 spawnPos;
           Vector3 raycastPos;

           float xLoc = Random.Range(-spawnRange, spawnRange) + transform.position.x;
           float zLoc = Random.Range(-spawnRange, spawnRange) + transform.position.z;
           float yLoc = transform.position.y;

           spawnPos = new Vector3(xLoc, yLoc, zLoc);

           raycastPos = spawnPos + (Vector3.up * 20f);

           RaycastHit hit;
           if (Physics.Raycast(raycastPos, Vector3.down, out hit, 30))
           {
               if (hit.collider.gameObject.layer!=10)
               {
                   return FindSpawnLoc();
               }
           }

           return spawnPos;
       }

       private IEnumerator SpawmDelay()
       {
            if (currentWave >= waves.Length)
            {
                yield break;
            }
           
           for (int i = 0; i < waves[currentWave].GetEnemySpawnList().Length; i++)
           {
               
               
               GameObject newspawn = Instantiate(waves[currentWave].GetEnemySpawnList()[i], FindSpawnLoc(), Quaternion.identity);
               if (newspawn.GetComponent<BasicSpawnedEnemyAi>() != null)
                {
                    newspawn.GetComponent<BasicSpawnedEnemyAi>().lookSpawnedEnemyRadius = basicEnemyLookRadiusNew;
                    newspawn.GetComponent<BasicSpawnedEnemyAi>().spawnerOfThisEnemy = this.gameObject;
                }
               else if (newspawn.GetComponent<WizardEnemy>() != null)
                {
                    newspawn.GetComponent<WizardEnemy>().spawnerOfThisEnemy = this.gameObject;
                }
               
               currentEnemy.Add(newspawn);

               yield return new WaitForSeconds(spawnSecond);
           }

            if (currentWave < waves.Length)
            {
                currentWave++;
            }
            Debug.Log(currentWave);
           
       }
   } 
}
