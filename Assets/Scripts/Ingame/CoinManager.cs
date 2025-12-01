using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    public TextMeshProUGUI coinText;
    private int coins = 0;

    private void Awake()
    {
        instance = this;
    }

    public void AddCoin()
    {
        coins++;
        UpdateUI();
    }

    void UpdateUI()
    {
        coinText.text = coins.ToString("00");
    }
}
