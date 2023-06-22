using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CihanAkpınar
{
   public class BasicEnemySpawner : MonoBehaviour
   {
       [System.Serializable]
   
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
       [SerializeField] float spawnRange = 10;
       public List<GameObject> currentEnemy;
       
       void Start()
       {
           SpawnWave();
       }
       
       void Update()
       {
           if(currentEnemy.Count == 0)
           {
               
               currentWave++;
               SpawnWave();
           }
       }
   
       void SpawnWave()
       {
           
           for(int i = 0; i < waves[currentWave].GetEnemySpawnList().Length; i++)
           {
               GameObject newspawn = Instantiate(waves[currentWave].GetEnemySpawnList()[i], FindSpawnLoc(),Quaternion.identity);
               currentEnemy.Add(newspawn);
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
   } 
}
