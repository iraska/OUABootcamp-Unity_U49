using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private int health, damage, mana;
    [SerializeField] private int startingHealth, startingDamage, startingMana;
    [SerializeField] private int healthMaxValue, damageMaxValue, manaMaxValue;

    public int Health { get { return health; } }
    public int Damage { get { return damage; } }
    public int Mana { get { return mana; } }

    public int HealthMaxValue { get { return healthMaxValue;} }
    public int DamageMaxValue { get { return damageMaxValue; } }
    public int ManaMaxValue { get { return manaMaxValue; } }

    public int StartingHealth { get { return startingHealth; } }
    public int StartingDamage { get { return startingDamage; } }
    public int StartingMana { get { return startingMana; } }

    private void Start()
    {
        if(PlayerPrefs.GetInt("lastGame", 0) > 0)
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
        }
    }
    public void Upgrade(int health, int damage, int mana)
    {
        this.health += startingHealth / 10 * health;
        this.damage += startingDamage / 10 * damage;
        this.mana += startingMana / 10 * mana;
        PlayerPrefs.SetInt("health", this.health);
        PlayerPrefs.SetInt("mana", this.mana);
        PlayerPrefs.SetInt("damage", this.damage);
    }
}
