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
    [SerializeField] private GameObject gamePanel, pausePanel, upgradePanel, dialogPanel, winPanel, losePanel;
    [SerializeField] private Image healthBar, manaBar, weaponImage;
    [SerializeField] private Text infoText;
    [SerializeField] private DialogSystem dialogSystem;
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
        currentPanel = gamePanel;
    }
    public void GamePanel()
    {
        currentPanel.SetActive(false);
        gamePanel.SetActive(true);
        currentPanel = gamePanel;
    }
    public void UpgradePanel()
    {
        currentPanel.SetActive(false);
        upgradePanel.SetActive(true);
        currentPanel = upgradePanel;
    }
    public void DialogPanel(DialogSystem.DialogStruct[] dialog)
    {
        currentPanel.SetActive(false);
        dialogPanel.SetActive(true);
        currentPanel = dialogPanel;
        dialogSystem.DialogText(dialog);
        GameManager.instance.GameState = GameManager.State.Dialog;
    }
    public void PausePanel()
    {
        currentPanel.SetActive(false);
        pausePanel.SetActive(true);
        currentPanel = pausePanel;
    }
    public void WinPanel()
    {
        currentPanel.SetActive(false);
        winPanel.SetActive(true);
        currentPanel = winPanel;
    }
    public void LosePanel()
    {
        currentPanel.SetActive(false);
        losePanel.SetActive(true);
        currentPanel = losePanel;
    }
    public void HealthBar(int currentHealth, int maxHealth)
    {
        healthBar.DOFillAmount(currentHealth / (float)maxHealth, 1f).SetEase(Ease.Linear);
    }
    public void ManaBar(int currentMana, int maxMana)
    {
        healthBar.DOFillAmount(currentMana / (float)maxMana, 1f).SetEase(Ease.Linear);
    }
    public void InfoText(string text)
    {
        infoText.text = "";
        infoText.DOText(text, text.Length / 100);
    }

    public void WeaponImage(Sprite weapon)
    {
        weaponImage.sprite = weapon;
    }

}
