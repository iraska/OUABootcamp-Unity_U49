using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class LevelManager : MonoBehaviour
{
    [SerializeField] private Image loadingBar;
    [SerializeField] private TextMeshProUGUI loadingText;
    [SerializeField] private Button continueGameButton;
    [SerializeField] private GameObject loadingCanvas, gameCanvas;
    public static LevelManager instance;
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
    }
    private void Start()
    {
        if (PlayerPrefs.GetInt("lastGame", 1) > 1)
        {
            continueGameButton.interactable = true;
        }
    }
    public IEnumerator LoadingGame(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone)
        {
            loadingBar.fillAmount = operation.progress;
            loadingText.text = "Loading %" + ((int)(loadingBar.fillAmount * 100)).ToString();
            yield return null;
        }
        loadingCanvas.SetActive(false);
        gameCanvas.SetActive(true);
        GameManager.instance.GameStart();
    }
    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            StartCoroutine(LoadingGame(nextSceneIndex));
            PlayerPrefs.SetInt("lastGame", nextSceneIndex);
        }
        else
        {
            GameManager.instance.Win();
        }
    }
    public void RestartLevel()
    {
        StartCoroutine(LoadingGame(SceneManager.GetActiveScene().buildIndex));
    }
    public void NewGame()
    {
        PlayerPrefs.SetInt("lastGame", 1);
        StartCoroutine(LoadingGame(1));
    }
    public void ContinueGame()
    {
        StartCoroutine(LoadingGame(PlayerPrefs.GetInt("lastGame")));
    }
}
