using System.Collections;
using System.Collections.Generic;
using TutorialSystem;
using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using static UnityEngine.ParticleSystem;

public class GameManager : MonoBehaviour
{
    public event Action enemyDestroyed = delegate { };
    public enum State
    {
        Menu,
        Playing,
        Paused,
        Upgrade,
        Dialog,
        Lose,
        Win,
        Tutorial
    }
    private State gameState;
    public State GameState 
    { 
        get { return gameState; } 
        set 
        {
            gameState = value;
        } 
    }
    private bool isArena;
    public bool IsArena { get { return isArena; } set { isArena = value; } }

    private bool canGoTheNextLevel;
    public bool CanGoTheNextLevel { get { return canGoTheNextLevel; } set { canGoTheNextLevel = value; } }

    private GameObject player;
    public GameObject Player { get { return player; } set { player = value; } }
    [SerializeField] private int targetFrameRate = 60;
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFrameRate;
        GameState = State.Menu;
    }

    public void GameStart()
    {
        Time.timeScale = 1;
        UIManager.instance.GamePanel();
        gameState = State.Playing;
    }
    public void Win()
    {
        if (GameState == State.Playing)
        {
            GameState = State.Win;
            UIManager.instance.WinPanel();
        }
    }
    public void ArenaWin(string name)
    {
        if (GameState == State.Playing)
        {
            GameState = State.Win;
            if (PlayerPrefs.GetString("arena" + (PlayerPrefs.GetInt("arena", 0) -1 ).ToString(), "null") != name)
            {
                PlayerPrefs.SetString("arena" + PlayerPrefs.GetInt("arena").ToString(), name);
                PlayerPrefs.SetInt("arena", PlayerPrefs.GetInt("arena", 0) + 1);
            }
            UIManager.instance.ArenaWinPanel(name);
            IsArena = false;
        }
    }
    public void Lose()
    {
        if(GameState == State.Playing)
        {
            GameState = State.Lose;
            StartCoroutine(UIManager.instance.LosePanel());
            IsArena = false;
        }
    }
    public void Upgrade(int balance)
    {
        GameState = State.Upgrade;
        PlayerPrefs.SetInt("balance", PlayerPrefs.GetInt("balance") + balance);
        UIManager.instance.UpgradePanel();
    }
    public void Paused()
    {
        GameState = State.Paused;
        UIManager.instance.PausePanel();
        Time.timeScale = 0;
    }
    public void MenuClicked()
    {
        GameState = State.Menu;
        SceneManager.LoadScene(0);
        LevelManager.instance.Start();
        Time.timeScale = 1;
    }
    public void ResumeClicked()
    {
        GameState = State.Playing;
        Time.timeScale = 1;
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void EnemyDestoyEvent()
    {
        enemyDestroyed.Invoke();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameState == State.Playing)
        {
            Paused();
        }
    }
}
