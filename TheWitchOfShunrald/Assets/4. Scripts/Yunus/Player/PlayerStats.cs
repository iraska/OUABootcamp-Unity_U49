using Shunrald;
using System.Collections;
using System.Collections.Generic;
using CihanAkpınar;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private int health, damage;
    private float mana;

    [SerializeField] private int startingHealth, startingDamage, startingMana;
    [SerializeField] private int healthMaxValue, damageMaxValue, manaMaxValue;
    [SerializeField] private float restoreMana;
    public int Health { get { return health; } }
    public int Damage { get { return damage; } }
    public float Mana { get { return mana; } }

    public int HealthMaxValue { get { return healthMaxValue;} }
    public int DamageMaxValue { get { return damageMaxValue; } }
    public int ManaMaxValue { get { return manaMaxValue; } }

    public int StartingHealth { get { return startingHealth; } }
    public int StartingDamage { get { return startingDamage; } }
    public int StartingMana { get { return startingMana; } }

    private ShunraldController shunraldController;

    private void Awake()
    {
        GameManager.instance.Player = this.gameObject;
    }

    private void Start()
    {
        if(PlayerPrefs.GetInt("lastGame") > 1)
        {
            health = PlayerPrefs.GetInt("health");
            damage = PlayerPrefs.GetInt("damage");
            mana = PlayerPrefs.GetInt("mana");
        }
        else
        {
            health = startingHealth;
            damage = startingDamage;
            mana = startingMana;
            PlayerPrefs.SetInt("health", startingHealth);
            PlayerPrefs.SetInt("mana", startingMana);
            PlayerPrefs.SetInt("damage", startingDamage);
        }
        InvokeRepeating(nameof(RestoreMana), 1, 0.1f);
        UIManager.instance.HealthBar(health, PlayerPrefs.GetInt("health"));
        UIManager.instance.ManaBar(mana, PlayerPrefs.GetInt("mana"));

        shunraldController = GetComponent<ShunraldController>();
    }
    public void Upgrade(int health, int damage, int mana)
    {
        PlayerPrefs.SetInt("health", PlayerPrefs.GetInt("health") + startingHealth / 10 * health);
        PlayerPrefs.SetInt("mana", PlayerPrefs.GetInt("mana") + startingMana / 10 * mana);
        PlayerPrefs.SetInt("damage", PlayerPrefs.GetInt("damage") + startingDamage / 10 * damage);
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            GameManager.instance.Lose();
            shunraldController.Animation.PlayDeathAnim();

            AudioManager.Instance.PlaySfx(AudioManager.Instance.witchDieAudio);
        }
        UIManager.instance.HealthBar(health, PlayerPrefs.GetInt("health"));
    }

    public bool SpendMana(float mana)
    {
        this.mana -= mana;
        UIManager.instance.ManaBar(this.mana, PlayerPrefs.GetInt("mana"));
        if (this.mana <= 0)
        {
            CancelInvoke(nameof(RestoreMana));
            InvokeRepeating(nameof(RestoreMana), 2, 0.1f);
            return false;
        }
        return true;
    }
    public void RestoreMana()
    {
        if (this.mana < startingMana)
        {
            mana += restoreMana;
            UIManager.instance.ManaBar(this.mana, PlayerPrefs.GetInt("mana"));
        }
    }
    public void ManaPot(float mana)
    {
        this.mana += mana;
        if (this.mana > PlayerPrefs.GetInt("mana"))
        {
            this.mana = PlayerPrefs.GetInt("mana");
        }
        UIManager.instance.ManaBar(this.mana, PlayerPrefs.GetInt("mana"));
    }
    public void HealthPot(int health)
    {
        this.health += health;
        if (this.mana > PlayerPrefs.GetInt("health"))
        {
            this.mana = PlayerPrefs.GetInt("health");
        }
        UIManager.instance.HealthBar(this.health, PlayerPrefs.GetInt("health"));
    }
}
