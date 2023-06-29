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
               SpawnWave();
           }
           
       }
   
       void SpawnWave()
       {
           StartCoroutine(SpawmDelay());
           if (currentEnemy.Count>0)
           {
              //kalp sesi
              //AudioManager.instance.PlaySound(AudioManager.instance.hearthAudio,true);
           }
           else
           {
               //AudioManager.instance.StopSound();  
           }
           
       }
   
       Vector3 FindSpawnLoc()
       {
           Vector3 SpawnPos;
   
           float xLoc = Random.Range(-spawnRange, spawnRange) + transform.position.x;
           float zLoc = Random.Range(-spawnRange, spawnRange) + transform.position.z;
           float yLoc = transform.position.y;
   
           SpawnPos = new Vector3(xLoc, yLoc, zLoc);
   
           if(Physics.Raycast(SpawnPos, Vector3.down,5))
           {
               return SpawnPos;
           }
           else
           {
               return FindSpawnLoc();
           }
       }

       IEnumerator SpawmDelay()
       {
           for (int i = 0; i < waves[currentWave].GetEnemySpawnList().Length; i++)
           {
               //spawnAudio
               //AudioManager.instance.PlaySound(AudioManager.instance.basicSpawnerSpawmAudio,false);
               GameObject newspawn = Instantiate(waves[currentWave].GetEnemySpawnList()[i], FindSpawnLoc(), Quaternion.identity);
               newspawn.GetComponent<BasicSpawnedEnemyAi>().lookSpawnedEnemyRadius = basicEnemyLookRadiusNew;
               newspawn.GetComponent<BasicSpawnedEnemyAi>().spawnerOfThisEnemy = this.gameObject;
               currentEnemy.Add(newspawn);

               yield return new WaitForSeconds(spawnSecond);
           }

           currentWave++;
       }
   } 
}
