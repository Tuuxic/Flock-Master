using GameEvents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    private int max_health = 1000;
    private int health = 1000;

    public HealthBarBehaviour healthBar;

    private void Start()
    {
        UpdateHealthBar();

        GameEventManager.AddListener<PlayerDamageEvent>(OnPlayerDamage);
    }

    void OnPlayerDamage(PlayerDamageEvent e)
    {
        DamagePlayer(e.damage);
    }

    public void DamagePlayer(int damage)
    {
        if (damage < health) {
            health = health - damage;
        } else {
            health = 0;
        }

        if (health <= 0) {
            GameEventManager.Raise(new GameOverEvent());
        }

        UpdateHealthBar();
    }

    public bool HealPlayer(int value)
    {
        if (max_health <= health) return false;
        health += value;
        if (health > max_health) health = max_health;
        UpdateHealthBar();
        return true;
    }

    private void UpdateHealthBar()
    {
        if (healthBar == null) return;
        healthBar.SetMaxHealth(max_health);
        healthBar.SetHealth(health);
    }
}
