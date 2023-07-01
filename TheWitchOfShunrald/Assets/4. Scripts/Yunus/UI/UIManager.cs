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
    [SerializeField] private GameObject gamePanel, pausePanel, upgradePanel, dialogPanel, winPanel, losePanel, infoPanel;
    [SerializeField] private Image healthBar, healthBar1, manaBar, weaponImage, easyButtonImage, normalButtonImage, hardButtonImage, lowButtonImage, highButtonImage, mediumButtonImage, weaponUICircle, weaponUILine;
    [SerializeField] private Sprite selectedSprite, unselectedSprite;
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
        if(!PlayerPrefs.HasKey("difficulty"))
        {
            PlayerPrefs.SetInt("difficulty", 1);
        }
        switch (PlayerPrefs.GetInt("difficulty"))
        {
            case 0:
                easyButtonImage.sprite = selectedSprite;
                easyButtonImage.gameObject.GetComponent<Button>().interactable = false;
                break;
            case 1:
                normalButtonImage.sprite = selectedSprite;
                normalButtonImage.gameObject.GetComponent<Button>().interactable = false;
                break;
            case 2:
                hardButtonImage.sprite = selectedSprite;
                hardButtonImage.gameObject.GetComponent<Button>().interactable = false;
                break;
        }
        if (!PlayerPrefs.HasKey("quality"))
        {
            PlayerPrefs.SetInt("quality", 2);
            QualitySettings.SetQualityLevel(3);
        }
        switch (PlayerPrefs.GetInt("quality"))
        {
            case 0:
                lowButtonImage.sprite = selectedSprite;
                lowButtonImage.gameObject.GetComponent<Button>().interactable = false;
                QualitySettings.SetQualityLevel(1);
                break;
            case 1:
                mediumButtonImage.sprite = selectedSprite;
                mediumButtonImage.gameObject.GetComponent<Button>().interactable = false;
                QualitySettings.SetQualityLevel(2);
                break;
            case 2:
                highButtonImage.sprite = selectedSprite;
                highButtonImage.gameObject.GetComponent<Button>().interactable = false;
                QualitySettings.SetQualityLevel(3);
                break;
        }
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
        healthBar.fillAmount = currentHealth / (float)maxHealth;
        healthBar1.DOFillAmount(currentHealth / (float)maxHealth, 1f).SetEase(Ease.Linear);
    }
    public void ManaBar(float currentMana, int maxMana)
    {
        manaBar.DOFillAmount(currentMana / (float)maxMana, 1f).SetEase(Ease.Linear);
    }
    public void InfoEnable(string text)
    {
        infoPanel.SetActive(true);
        infoText.text = "";
        infoText.DOText(text, text.Length / 100);
    }
    public void InfoDisable()
    {
        infoPanel.SetActive(false);
    }

    public void WeaponImage(Sprite weapon)
    {
        weaponImage.sprite = weapon;
    }

    public Image WeaponUICircle
    {
        get { return weaponUICircle; }
    }

    public Image WeaponUILine
    {
        get { return weaponUILine; }
    }

    //------------------- SETTÝNGS--------------------------

    // DÝFFÝCULTY
    public void Easy()
    {
        easyButtonImage.sprite = selectedSprite;
        easyButtonImage.gameObject.GetComponent<Button>().interactable = false;
        if(PlayerPrefs.GetInt("difficulty") == 1)
        {
            normalButtonImage.sprite = unselectedSprite;
            normalButtonImage.gameObject.GetComponent<Button>().interactable = true;
        }
        else
        {
            hardButtonImage.sprite = unselectedSprite;
            hardButtonImage.gameObject.GetComponent<Button>().interactable = true;
        }
        PlayerPrefs.SetInt("difficulty",0);
    }
    public void Normal()
    {
        normalButtonImage.sprite = selectedSprite;
        normalButtonImage.gameObject.GetComponent<Button>().interactable = false;
        if (PlayerPrefs.GetInt("difficulty") == 0)
        {
            easyButtonImage.sprite = unselectedSprite;
            easyButtonImage.gameObject.GetComponent<Button>().interactable = true;
        }
        else
        {
            hardButtonImage.sprite = unselectedSprite;
            hardButtonImage.gameObject.GetComponent<Button>().interactable = true;
        }
        PlayerPrefs.SetInt("difficulty", 1);
    }
    public void Hard()
    {
        hardButtonImage.sprite = selectedSprite;
        hardButtonImage.gameObject.GetComponent<Button>().interactable = false;
        if (PlayerPrefs.GetInt("difficulty") == 1)
        {
            normalButtonImage.sprite = unselectedSprite;
            normalButtonImage.gameObject.GetComponent<Button>().interactable = true;
        }
        else
        {
            easyButtonImage.sprite = unselectedSprite;
            easyButtonImage.gameObject.GetComponent<Button>().interactable = true;
        }
        PlayerPrefs.SetInt("difficulty", 2);
    }
    // Quality
    public void Low()
    {
        QualitySettings.SetQualityLevel(1);

        lowButtonImage.sprite = selectedSprite;
        lowButtonImage.gameObject.GetComponent<Button>().interactable = false;
        if (PlayerPrefs.GetInt("quality") == 1)
        {
            mediumButtonImage.sprite = unselectedSprite;
            mediumButtonImage.gameObject.GetComponent<Button>().interactable = true;
        }
        else
        {
            highButtonImage.sprite = unselectedSprite;
            highButtonImage.gameObject.GetComponent<Button>().interactable = true;
        }
        PlayerPrefs.SetInt("quality", 0);
    }
    public void Medium()
    {
        QualitySettings.SetQualityLevel(2);
        mediumButtonImage.sprite = selectedSprite;
        mediumButtonImage.gameObject.GetComponent<Button>().interactable = false;
        if (PlayerPrefs.GetInt("quality") == 0)
        {
            lowButtonImage.sprite = unselectedSprite;
            lowButtonImage.gameObject.GetComponent<Button>().interactable = true;
        }
        else
        {
            highButtonImage.sprite = unselectedSprite;
            highButtonImage.gameObject.GetComponent<Button>().interactable = true;
        }
        PlayerPrefs.SetInt("quality", 1);
    }
    public void High()
    {
        QualitySettings.SetQualityLevel(3);
        highButtonImage.sprite = selectedSprite;
        highButtonImage.gameObject.GetComponent<Button>().interactable = false;
        if (PlayerPrefs.GetInt("quality") == 1)
        {
            mediumButtonImage.sprite = unselectedSprite;
            mediumButtonImage.gameObject.GetComponent<Button>().interactable = true;
        }
        else
        {
            lowButtonImage.sprite = unselectedSprite;
            lowButtonImage.gameObject.GetComponent<Button>().interactable = true;
        }
        PlayerPrefs.SetInt("quality", 2);
    }
}
