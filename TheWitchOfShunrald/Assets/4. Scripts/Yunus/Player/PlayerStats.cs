using Shunrald;
using System.Collections;
using System.Collections.Generic;
using CihanAkpınar;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private float health, damage, mana;

    [SerializeField] private float startingHealth, startingDamage, startingMana;
    [SerializeField] private float healthMaxValue, damageMaxValue, manaMaxValue;
    [SerializeField] private float restoreMana;
    public float Health { get { return health; } }
    public float Damage { get { return damage; } }
    public float Mana { get { return mana; } }
    public float HealthMaxValue { get { return healthMaxValue;} }
    public float DamageMaxValue { get { return damageMaxValue; } }
    public float ManaMaxValue { get { return manaMaxValue; } }

    public float StartingHealth { get { return startingHealth; } }
    public float StartingDamage { get { return startingDamage; } }
    public float StartingMana { get { return startingMana; } }

    private ShunraldController shunraldController;

    private void Awake()
    {
        GameManager.instance.Player = this.gameObject;
    }

    private void Start()
    {
        if(PlayerPrefs.GetInt("lastGame") > 1)
        {
            health = PlayerPrefs.GetFloat("health");
            damage = PlayerPrefs.GetFloat("damage");
            mana = PlayerPrefs.GetFloat("mana");
        }
        else
        {
            health = startingHealth;
            damage = startingDamage;
            mana = startingMana;
            PlayerPrefs.SetFloat("health", startingHealth);
            PlayerPrefs.SetFloat("mana", startingMana);
            PlayerPrefs.SetFloat("damage", startingDamage);
        }
        InvokeRepeating(nameof(RestoreMana), 1, 0.1f);
        UIManager.instance.HealthBar(health, PlayerPrefs.GetFloat("health"));
        UIManager.instance.ManaBar(mana, PlayerPrefs.GetFloat("mana"));

        shunraldController = GetComponent<ShunraldController>();
    }
    public void Upgrade(int health, int damage, int mana)
    {
        PlayerPrefs.SetFloat("health", PlayerPrefs.GetFloat("health") + startingHealth / 10 * health);
        PlayerPrefs.SetFloat("mana", PlayerPrefs.GetFloat("mana") + startingMana / 10 * mana);
        PlayerPrefs.SetFloat("damage", PlayerPrefs.GetFloat("damage") + startingDamage / 10 * damage);
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            GameManager.instance.Lose();
            shunraldController.Animation.PlayDeathAnim();

            AudioManager.Instance.PlaySfx(AudioManager.Instance.witchDieAudio,transform.position);
        }
        if(!GameManager.instance.IsArena)
            UIManager.instance.HealthBar(health, PlayerPrefs.GetFloat("health"));
        else
            UIManager.instance.HealthBar(health, arenaStartingHealth);
    }

    public bool SpendMana(float mana)
    {
        this.mana -= mana;
        if (!GameManager.instance.IsArena)
            UIManager.instance.ManaBar(this.mana, PlayerPrefs.GetFloat("mana"));
        else
            UIManager.instance.ManaBar(this.mana, arenaStartingMana);
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
        if (!GameManager.instance.IsArena)
        {
            if (this.mana < PlayerPrefs.GetFloat("mana"))
            {
                mana += restoreMana;
                UIManager.instance.ManaBar(this.mana, PlayerPrefs.GetFloat("mana"));
            }
        }
        else
        {
            if (this.mana < arenaStartingMana)
            {
                mana += restoreMana;
                UIManager.instance.ManaBar(this.mana, arenaStartingMana);
            }
        }
    }
    public void ManaPot(float mana)
    {
        this.mana += mana;
        if (this.mana > PlayerPrefs.GetFloat("mana"))
        {
            this.mana = PlayerPrefs.GetFloat("mana");
        }
        if (!GameManager.instance.IsArena)
            UIManager.instance.ManaBar(this.mana, PlayerPrefs.GetFloat("mana"));
        else
            UIManager.instance.ManaBar(this.mana, arenaStartingMana);
    }
    public void HealthPot(float health)
    {
        this.health += health;
        if (this.mana > PlayerPrefs.GetFloat("health"))
        {
            this.mana = PlayerPrefs.GetFloat("health");
        }
        if (!GameManager.instance.IsArena)
            UIManager.instance.HealthBar(this.health, PlayerPrefs.GetFloat("health"));
        else
            UIManager.instance.HealthBar(this.health, arenaStartingHealth);
    }
    float arenaStartingHealth, arenaStartingMana;
    public void SetPlayerStats(float health, float damage, float mana)
    {
        this.damage = damage;
        this.mana = mana;
        this.health = health;
        arenaStartingHealth = health;
        arenaStartingMana = mana;
    }
}
