using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public GameObject gamePanel, pausePanel, upgradePanel, dialogPanel, winPanel, losePanel;
    public Image healthBar, manaBar, upgrade1Bar, upgrade2Bar, upgrade3Bar, dialogImage, weaponImage;
    public TextMeshProUGUI dialogNameText, upgradeBalanceText;
    public Text infoText, dialogText;

    public void MenuClicked()
    {
        SceneManager.LoadScene("Menu");
    }
    public void ResumeClicked()
    {
        Time.timeScale = 1;
    }
    public void RestartClicked()
    { 

    }
    public void TryAgainClicked()
    {

    }
    public void QuitClicked()
    {
        Application.Quit();
    }
    public void UpgradeContinueClicked()
    {

    }
    public void Upgrade1Increase()
    {

    }
    public void Upgrade2Increase()
    {

    }
    public void Upgrade3Increase()
    {

    }
    public void Upgrade1Decrease()
    {

    }
    public void Upgrade2Decrease()
    {

    }
    public void Upgrade3Decrease()
    {

    }
    public void DialogNext()
    {
        UIManager.instance.DialogText();
    }
}
