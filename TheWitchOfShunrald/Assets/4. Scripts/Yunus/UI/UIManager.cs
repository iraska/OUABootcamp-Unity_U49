using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private GameObject gamePanel, pausePanel, upgradePanel, dialogPanel, winPanel, losePanel, infoPanel, arenaWinPanel, gameLoadingPanel, bossHealthBarPanel;
    [SerializeField] private GameObject[] weaponImages;
    [SerializeField] private Image healthBar, healthBar1, manaBar, easyButtonImage, normalButtonImage, hardButtonImage, lowButtonImage, highButtonImage, mediumButtonImage, weaponUICircle, weaponUILine, fireSkillImage, dashSkillImage;
    [SerializeField] private Slider bossHealthBar;
    [SerializeField] private Sprite selectedSprite, unselectedSprite;
    [SerializeField] private Text infoText;
    [SerializeField] private DialogSystem dialogSystem;
    [SerializeField] private TextMeshProUGUI badgeText, versionText, titleText;
    [SerializeField] private ArenaRewardPanel arenaRewardPanel;
    [SerializeField] private RectTransform badgeTransform;
    private GameObject currentGamePanel, currentMenuPanel;

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
        versionText.text = 'v' + Application.version;
        currentGamePanel = gamePanel;
        if (!PlayerPrefs.HasKey("difficulty"))
        {
            PlayerPrefs.SetInt("difficulty", 2);
        }
        switch (PlayerPrefs.GetInt("difficulty"))
        {
            case 1:
                easyButtonImage.sprite = selectedSprite;
                easyButtonImage.gameObject.GetComponent<Button>().interactable = false;
                break;
            case 2:
                normalButtonImage.sprite = selectedSprite;
                normalButtonImage.gameObject.GetComponent<Button>().interactable = false;
                break;
            case 3:
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
        TitleColorAnim();
    }
    public void GamePanel()
    {
        if(GameManager.instance.isArena)
            StartCoroutine(GameLoadingPanel());

        currentGamePanel.SetActive(false);
        bossHealthBarPanel.SetActive(false);
        gamePanel.SetActive(true);
        StartCoroutine(FireCoolDown(0));
        StartCoroutine(DashCoolDown(0));
        currentGamePanel = gamePanel;
    }
    public void UpgradePanel()
    {
        currentGamePanel.SetActive(false);
        upgradePanel.SetActive(true);
        currentGamePanel = upgradePanel;
    }
    public void DialogPanel(DialogSystem.DialogStruct[] dialog)
    {
        currentGamePanel.SetActive(false);
        dialogPanel.SetActive(true);
        currentGamePanel = dialogPanel;
        dialogSystem.DialogText(dialog);
        GameManager.instance.GameState = GameManager.State.Dialog;
    }
    public void PausePanel()
    {
        currentGamePanel.SetActive(false);
        pausePanel.SetActive(true);
        currentGamePanel = pausePanel;
    }
    public void WinPanel()
    {
        currentGamePanel.SetActive(false);
        winPanel.SetActive(true);
        currentGamePanel = winPanel;
    }
    public void ArenaWinPanel(string name)
    {
        badgeTransform.localScale = Vector3.zero;
        currentGamePanel.SetActive(false);
        arenaWinPanel.SetActive(true);
        badgeTransform.DOScale(2.5f, 1.5f).OnComplete(() =>
        {
            badgeTransform.DOScale(1, 3f).OnComplete(() =>
            {
                badgeTransform.DOShakeRotation(2, 5).SetLoops(3, LoopType.Restart);
            });
        });
        currentGamePanel = arenaWinPanel;
        badgeText.text = name;
        StartCoroutine(arenaRewardPanel.Start());
    }
    public IEnumerator LosePanel()
    {
        yield return new WaitForSeconds(5);

        currentGamePanel.SetActive(false);
        losePanel.SetActive(true);
        currentGamePanel = losePanel;
    }

    public void HealthBar(float currentHealth, float maxHealth)
    {
        healthBar.fillAmount = currentHealth / (float)maxHealth;
        healthBar1.DOFillAmount(currentHealth / (float)maxHealth, 1f).SetEase(Ease.Linear);
    }
    public void ManaBar(float currentMana, float maxMana)
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

    public void WeaponImage(int index)
    {
        for(int i = 0; i < weaponImages.Length; i++)
        {
            weaponImages[i].SetActive(false);
        }
        weaponImages[index].SetActive(true);
    }
    Tween dashTween;
    public IEnumerator DashCoolDown(float duration)
    {
        dashTween.Kill();
        dashSkillImage.fillAmount = 1;
        dashSkillImage.transform.parent.localScale = Vector3.one;
        dashSkillImage.DOFillAmount(0, duration).SetEase(Ease.Linear);
        yield return new WaitForSeconds(duration);
        dashTween = dashSkillImage.transform.parent.DOScale(1.1f, 1f).SetEase(Ease.Linear);
        dashTween.SetLoops(-1, LoopType.Yoyo);
    }
    Tween fireTween;
    public IEnumerator FireCoolDown(float duration)
    {
        fireTween.Kill();
        fireSkillImage.fillAmount = 1;
        fireSkillImage.transform.parent.localScale = Vector3.one;
        fireSkillImage.DOFillAmount(0, duration).SetEase(Ease.Linear);
        yield return new WaitForSeconds(duration);
        fireTween = fireSkillImage.transform.parent.DOScale(1.1f, 1f).SetEase(Ease.Linear);
        fireTween.SetLoops(-1, LoopType.Yoyo);
    }

    public Image WeaponUICircle
    {
        get { return weaponUICircle; }
    }

    public Image WeaponUILine
    {
        get { return weaponUILine; }
    }
    private IEnumerator GameLoadingPanel()
    {
        gameLoadingPanel.transform.GetChild(0).GetComponent<Image>().fillAmount = 0;
        gameLoadingPanel.SetActive(true);
        gameLoadingPanel.transform.GetChild(0).GetComponent<Image>().DOFillAmount(1, 5f);
        yield return new WaitForSeconds(5);
        gameLoadingPanel.SetActive(false);
        GameManager.instance.GameState = GameManager.State.Playing;
    }

    public void BossHealthBarPanel()
    {
        bossHealthBar.value = 1;
        bossHealthBarPanel.SetActive(true);
    }

    public void BossHealthBarDeactivate()
    {
        bossHealthBarPanel.SetActive(false);
    }

    public void BossHealthBar(float maxHealth, float currentHealth)
    {
        bossHealthBar.value = currentHealth / maxHealth;
    }
    private void TitleColorAnim()
    {
        Color titleTopLeftColor = Color.white;
        float value = 1;
        DOTween.To(() => value, x => value = x, 0, 3)
            .OnUpdate(() => {
                titleTopLeftColor = new Color(1, value, value);
                titleText.colorGradient = new VertexGradient(titleTopLeftColor, titleText.colorGradient.topRight, titleText.colorGradient.bottomLeft, titleText.colorGradient.bottomRight);
            }).SetLoops(-1, LoopType.Yoyo);
    }

    public void OpenPanelAnim(Transform openPanel)
    {
        if(currentMenuPanel != null)
        {
            currentMenuPanel.transform.DOScale(0, 0.5f).OnComplete(() =>
            {
                currentMenuPanel.gameObject.SetActive(false);
                openPanel.gameObject.SetActive(true);
                currentMenuPanel = openPanel.gameObject;
                openPanel.localScale = Vector3.zero;
                openPanel.DOScale(1.2f, 0.5f).OnComplete(() =>
                {
                    openPanel.DOScale(1.0f, 0.2f);
                });
            });
        }
        else
        {
            openPanel.gameObject.SetActive(true);
            currentMenuPanel = openPanel.gameObject;
            openPanel.localScale = Vector3.zero;
            openPanel.DOScale(1.2f, 0.5f).OnComplete(() =>
            {
                openPanel.DOScale(1.0f, 0.2f);
            });
        }
    }
    public void ClosePanelAnim()
    {
        currentMenuPanel.transform.DOScale(0, 0.5f).OnComplete(() =>
        {
            currentMenuPanel.gameObject.SetActive(false);
            currentMenuPanel = null;
        });
    }

    //------------------- SETT�NGS--------------------------

    // D�FF�CULTY
    public void Easy()
    {
        easyButtonImage.sprite = selectedSprite;
        easyButtonImage.gameObject.GetComponent<Button>().interactable = false;
        if(PlayerPrefs.GetInt("difficulty") == 2)
        {
            normalButtonImage.sprite = unselectedSprite;
            normalButtonImage.gameObject.GetComponent<Button>().interactable = true;
        }
        else
        {
            hardButtonImage.sprite = unselectedSprite;
            hardButtonImage.gameObject.GetComponent<Button>().interactable = true;
        }
        PlayerPrefs.SetInt("difficulty",1);
    }
    public void Normal()
    {
        normalButtonImage.sprite = selectedSprite;
        normalButtonImage.gameObject.GetComponent<Button>().interactable = false;
        if (PlayerPrefs.GetInt("difficulty") == 1)
        {
            easyButtonImage.sprite = unselectedSprite;
            easyButtonImage.gameObject.GetComponent<Button>().interactable = true;
        }
        else
        {
            hardButtonImage.sprite = unselectedSprite;
            hardButtonImage.gameObject.GetComponent<Button>().interactable = true;
        }
        PlayerPrefs.SetInt("difficulty", 2);
    }
    public void Hard()
    {
        hardButtonImage.sprite = selectedSprite;
        hardButtonImage.gameObject.GetComponent<Button>().interactable = false;
        if (PlayerPrefs.GetInt("difficulty") == 2)
        {
            normalButtonImage.sprite = unselectedSprite;
            normalButtonImage.gameObject.GetComponent<Button>().interactable = true;
        }
        else
        {
            easyButtonImage.sprite = unselectedSprite;
            easyButtonImage.gameObject.GetComponent<Button>().interactable = true;
        }
        PlayerPrefs.SetInt("difficulty", 3);
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
