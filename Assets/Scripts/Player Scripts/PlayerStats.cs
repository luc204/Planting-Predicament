using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public int coins = 0;
    public TextMeshProUGUI coinText;

    void Start()
    {
        UpdateCoinText();
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateCoinText();
    }

    public void UpdateCoinText()
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + coins.ToString();
        }
    }
}

