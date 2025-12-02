using UnityEngine;
using TMPro;

public class EnemyCounter : MonoBehaviour
{
    public static EnemyCounter instance;

    public int totalEnemies = 8;
    private int killedEnemies = 0;

    public TextMeshProUGUI counterText;
    public SlidingDoor door;

    void Awake()
    {
        instance = this;
        UpdateUI();
    }

    public void EnemyKilled()
    {
        killedEnemies++;
        UpdateUI();

        if (killedEnemies >= totalEnemies)
        {
            door.OpenDoor();
        }
    }

    void UpdateUI()
    {
        if (counterText != null)
            counterText.text = $"{killedEnemies}/{totalEnemies}";
    }
}