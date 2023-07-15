using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class ArenaBuilder : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    EnemyData[] enemiesData;
    PlayerData playerData;
    GameData gameData;
    private int currentEnemyCount;
    string url = "https://drive.google.com/uc?export=download&id=1B7UF9Z59rCqSApyCDDDXjIAcIDAIx76r";

    [System.Serializable]
    public struct Data
    {
        public GameData gameData;
        public EnemyData[] enemyData;
        public PlayerData playerData;
    }
    [System.Serializable]
    public struct GameData
    {
        public string name;
        public int[] alignment;
    }
    [System.Serializable]
    public struct EnemyData
    {
        public int id;
        public float position_x;
        public float position_y;
        public float position_z;
        public float health;
        public float damage;
    }
    [System.Serializable]
    public struct PlayerData
    {
        public float position_x;
        public float position_y;
        public float position_z;
        public float health;
        public float damage;
        public float mana;
    }
    
    void Start()
    {
        GameManager.instance.IsArena = true;
        StartCoroutine(GetData());
        GameManager.instance.enemyDestroyed += OnEnemyDestroyed;
    }
    IEnumerator GetData()
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error");
        }
        else
        {
            Data data = JsonUtility.FromJson<Data>(request.downloadHandler.text);
            enemiesData = data.enemyData;
            playerData = data.playerData;
            gameData = data.gameData;
            GameManager.instance.GameState = GameManager.State.Paused;
            LoadPlayer();
            LoadEnemies();
        }
    }
    int alignmentIndex=0;
    int enemyIndex=0;
    private void LoadEnemies()
    {
        if(alignmentIndex == gameData.alignment.Length)
        {
            GameManager.instance.ArenaWin(gameData.name);
            return;
        }

        for (int j = 0; j < gameData.alignment[alignmentIndex]; j++)
        {
            Enemy enemy = Instantiate(enemies[enemiesData[enemyIndex].id], new Vector3(enemiesData[enemyIndex].position_x, enemiesData[enemyIndex].position_y, enemiesData[enemyIndex].position_z), Quaternion.identity).GetComponentInChildren<Enemy>();
            enemy.SetEnemyStats(enemiesData[enemyIndex].health, enemiesData[enemyIndex].damage);
            ++enemyIndex;
        }
        currentEnemyCount = gameData.alignment[alignmentIndex];
        ++alignmentIndex;
    }
    private void OnEnemyDestroyed()
    {
       if(--currentEnemyCount == 0)
       {
           LoadEnemies();
       }
    }
    private void LoadPlayer()
    {
        GameManager.instance.Player.transform.position = new Vector3(playerData.position_x, playerData.position_y, playerData.position_z);
        GameManager.instance.Player.GetComponent<PlayerStats>().SetPlayerStats(playerData.health, playerData.damage, playerData.mana);
    }
    private void OnDestroy()
    {
        GameManager.instance.enemyDestroyed -= OnEnemyDestroyed;
    }

}