using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private Button continueGameButton;
    [SerializeField] private Image loadingBar;
    [SerializeField] private TextMeshProUGUI loadingText;
    private GameUI gameUI;
    private GameObject currentPanel;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        if (PlayerPrefs.GetInt("lastGame",0) > 0)
        {
            continueGameButton.interactable = true;
        }
    }
    public void NewGame()
    {
        PlayerPrefs.SetInt("lastGame", 0);
        StartCoroutine(LoadingGame());
    }
    public void ContinueGame()
    {
        StartCoroutine(LoadingGame());
    }
    private IEnumerator LoadingGame()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("Game");
        while (!operation.isDone)
        {
            loadingBar.fillAmount = operation.progress;
            loadingText.text = "Loading %" + ((int)(loadingBar.fillAmount * 100)).ToString();
            yield return null;
        }
        gameUI = FindObjectOfType<GameUI>();
        currentPanel = gameUI.gamePanel;
    }
    public void GamePanel()
    {
        currentPanel.SetActive(false);
        gameUI.gamePanel.SetActive(true);
        currentPanel = gameUI.gamePanel;
    }
    public void UpgradePanel()
    {
        currentPanel.SetActive(false);
        gameUI.upgradePanel.SetActive(true);
        currentPanel = gameUI.upgradePanel;
    }
    public void DialogPanel(DialogSystem.DialogStruct[] dialog)
    {
        currentPanel.SetActive(false);
        gameUI.dialogPanel.SetActive(true);
        currentPanel = gameUI.dialogPanel;
        currentDialog = dialog;
        DialogText();
    }
    public void PausePanel()
    {
        currentPanel.SetActive(false);
        gameUI.pausePanel.SetActive(true);
        currentPanel = gameUI.pausePanel;
    }
    public void WinPanel()
    {
        currentPanel.SetActive(false);
        gameUI.winPanel.SetActive(true);
        currentPanel = gameUI.winPanel;
    }
    public void LosePanel()
    {
        currentPanel.SetActive(false);
        gameUI.losePanel.SetActive(true);
        currentPanel = gameUI.losePanel;
    }
    public void HealthBar(int currentHealth, int maxHealth)
    {
        gameUI.healthBar.DOFillAmount(currentHealth / (float)maxHealth, 1f).SetEase(Ease.Linear);
    }
    public void ManaBar(int currentMana, int maxMana)
    {
        gameUI.healthBar.DOFillAmount(currentMana / (float)maxMana, 1f).SetEase(Ease.Linear);
    }
    public void InfoText(string text)
    {
        gameUI.infoText.text = "";
        gameUI.infoText.DOText(text, text.Length / 100);
    }
    private int dialogIndex = 0;
    private DialogSystem.DialogStruct[] currentDialog;
    public void DialogText()
    {
        if(dialogIndex < currentDialog.Length)
        {
            gameUI.dialogText.text = "";
            gameUI.dialogText.DOText(currentDialog[dialogIndex].text, currentDialog[dialogIndex].text.Length / 40f);
            gameUI.dialogNameText.text = currentDialog[dialogIndex].name;
            gameUI.dialogImage.sprite = currentDialog[dialogIndex].icon;
            dialogIndex++;
        }
        else
        {
            dialogIndex = 0;
            GamePanel();
        }
    }
    public void WeaponImage(Sprite weapon)
    {
        gameUI.weaponImage.sprite = weapon;
    }
    public void Quit()
    {
        Application.Quit();
    }
}
