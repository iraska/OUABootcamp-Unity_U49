using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class UpgradeSystem : MonoBehaviour
{
    [SerializeField] private Image healthBar, manaBar, damageBar;
    [SerializeField] private TextMeshProUGUI balanceText;
    private int health, damage, mana;
    private PlayerStats playerStats;
    private void Start()
    {
        if (!PlayerPrefs.HasKey("balance"))
            PlayerPrefs.SetInt("balance", 0);
    }
    private void OnEnable()
    {
        playerStats = GameManager.instance.Player.GetComponent<PlayerStats>();
        healthBar.fillAmount = playerStats.Health / playerStats.HealthMaxValue;
        damageBar.fillAmount = playerStats.Damage / playerStats.DamageMaxValue;
        manaBar.fillAmount = playerStats.Mana / playerStats.ManaMaxValue;
        balanceText.text = PlayerPrefs.GetInt("balance").ToString();
    }
    public void UpgradeContinueClicked()
    {
        playerStats.Upgrade(health, damage, mana);
        health = 0;
        damage = 0;
        mana = 0;
        LevelManager.instance.LoadNextLevel();
    }
    public void HealthIncrease()
    {
        if(PlayerPrefs.GetInt("balance") > 0 && health < playerStats.HealthMaxValue)
        {
            health++;
            PlayerPrefs.SetInt("balance", PlayerPrefs.GetInt("balance") - 1);
            healthBar.DOFillAmount((PlayerPrefs.GetFloat("health") + playerStats.StartingHealth / 10f * health) / playerStats.HealthMaxValue, 0.5f);
            balanceText.text = PlayerPrefs.GetInt("balance").ToString();
        }
    }
    public void DamageIncrease()
    {
        if (PlayerPrefs.GetInt("balance") > 0 && damage < playerStats.DamageMaxValue)
        {
            damage++;
            PlayerPrefs.SetInt("balance", PlayerPrefs.GetInt("balance") - 1);
            damageBar.DOFillAmount((PlayerPrefs.GetFloat("damage") + playerStats.StartingDamage / 10f * damage) / playerStats.DamageMaxValue, 0.5f);
            balanceText.text = PlayerPrefs.GetInt("balance").ToString();
        }
    }
    public void ManaIncrease()
    {
        if (PlayerPrefs.GetInt("balance") > 0 && mana < playerStats.ManaMaxValue)
        {
            mana++;
            PlayerPrefs.SetInt("balance", PlayerPrefs.GetInt("balance") - 1);
            manaBar.DOFillAmount((PlayerPrefs.GetFloat("mana") + playerStats.StartingMana / 10f * mana) / playerStats.ManaMaxValue, 0.5f);
            balanceText.text = PlayerPrefs.GetInt("balance").ToString();
        }
    }
    public void HealthDecrease()
    {
        if (health > 0)
        {
            health--;
            PlayerPrefs.SetInt("balance", PlayerPrefs.GetInt("balance") + 1);
            healthBar.DOFillAmount((PlayerPrefs.GetFloat("health") + playerStats.StartingHealth / 10f * health) / playerStats.HealthMaxValue, 0.5f);
            balanceText.text = PlayerPrefs.GetInt("balance").ToString();
        }
    }
    public void DamageDecrease()
    {
        if (damage > 0)
        {
            damage--;
            PlayerPrefs.SetInt("balance", PlayerPrefs.GetInt("balance") + 1);
            damageBar.DOFillAmount((PlayerPrefs.GetFloat("damage") + playerStats.StartingDamage / 10f * damage) / playerStats.DamageMaxValue, 0.5f);
            balanceText.text = PlayerPrefs.GetInt("balance").ToString();
        }
    }
    public void ManaDecrease()
    {
        if (mana > 0)
        {
            mana--;
            PlayerPrefs.SetInt("balance", PlayerPrefs.GetInt("balance") + 1);
            manaBar.DOFillAmount((PlayerPrefs.GetFloat("mana") + playerStats.StartingMana / 10f * mana) / playerStats.ManaMaxValue, 0.5f);
            balanceText.text = PlayerPrefs.GetInt("balance").ToString();
        }
    }
}
