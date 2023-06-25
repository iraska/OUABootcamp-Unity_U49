using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum State
    {
        Menu,
        Playing,
        Paused,
        Upgrade,
        Dialog,
        Lose,
        Win
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

    private GameObject player;
    public GameObject Player { get { return player; } }
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
        GameState = State.Menu;
    }
    public void GameStart()
    {
        UIManager.instance.GamePanel();
        player = GameObject.Find("Player");
        gameState = State.Playing;
    }
    public void Win()
    {
        GameState = State.Win;
        UIManager.instance.WinPanel();
    }
    public void Lose()
    {
        GameState = State.Lose;
        UIManager.instance.LosePanel();
    }
    public void Upgrade(int balance)
    {
        GameState = State.Upgrade;
        UIManager.instance.UpgradePanel();
        PlayerPrefs.SetInt("balance", PlayerPrefs.GetInt("balance") + balance);
    }
    public void Paused()
    {
        GameState = State.Paused;
        UIManager.instance.PausePanel();
    }
    public void MenuClicked()
    {
        GameState = State.Menu;
        SceneManager.LoadScene("Menu");
    }
    public void ResumeClicked()
    {
        GameState = State.Playing;
    }
    public void Quit()
    {
        Application.Quit();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameState == State.Playing)
        {
            Paused();
        }
    }
}
