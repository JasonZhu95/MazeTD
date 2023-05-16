using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerStats : MonoBehaviour
{
    private int startingHealth = 20;
    private int startingCoins = 100;

    public int CurrentHealth { get; private set; }
    public int CurrentCoins { get; private set; }

    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI coinsText;

    private void Start()
    {
        CurrentHealth = startingHealth;
        CurrentCoins = startingCoins;

        UpdateHealthUI();
        UpdateCoinsUI();
    }

    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;
        if (CurrentHealth <= 0)
        {
            // Game Over Stuff
            Debug.Log("Game Over");
        }

        UpdateHealthUI();
    }

    public void DeductCoins(int amount)
    {
        if (CurrentCoins >= amount)
        {
            CurrentCoins -= amount;
            UpdateCoinsUI();
        }
    }

    public void AddCoins(int amount)
    {
        CurrentCoins += amount;
        UpdateCoinsUI();
    }

    private void UpdateCoinsUI()
    {
        coinsText.text = CurrentCoins.ToString();
    }

    private void UpdateHealthUI()
    {
        healthText.text = CurrentHealth.ToString();
    }
}
