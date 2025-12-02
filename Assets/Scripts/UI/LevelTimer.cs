using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelTimer : MonoBehaviour
{
    public float startTime = 45f;
    float currentTime;

    public bool levelFinished = false;

    public Image timerBar;

    public GameObject victoryPanel;
    public GameObject defeatPanel;

    public Image[] starImages;
    public Sprite starOn;
    public Sprite starOff;

    public TextMeshProUGUI coinsCollectedText;
    private int maxCoins = 10;


    private void Start()
    {
        currentTime = startTime;

        if (victoryPanel != null)
        {
            victoryPanel.SetActive(false);
        }
        if (defeatPanel != null)
        {
            defeatPanel.SetActive(false);
        }
    }

    private void Update()
    {
        if (levelFinished)
            return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            Lose();
        }

        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        if (timerBar != null)
        {
            float t = currentTime / startTime;
            timerBar.fillAmount = Mathf.Clamp01(t);
        }
    }

    public void PlayerReachedGoal()
    {
        if (levelFinished)
        {
            return;
        }

        levelFinished = true;

        int stars = 0;

        if (CoinManager.instance.Coins >= 8f)
        {
            stars = 3;
        }
        else if (CoinManager.instance.Coins >= 5f)
        {
            stars = 2;
        }
        else if (CoinManager.instance.Coins >= 3f)
        {
            stars = 1;
        }
        else
        {
            stars = 0;
        }

        ShowVictory(stars);
    }


    void Lose()
    {
        if (levelFinished) return;

        levelFinished = true;
        ShowDefeat();
    }

    void ShowVictory(int stars)
    {
        Time.timeScale = 0f;
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
        }

        if (coinsCollectedText != null && CoinManager.instance != null)
        {
            coinsCollectedText.text = "You collected \n"
                           + CoinManager.instance.Coins + " of " + maxCoins + " coins!";

        }

        if (starImages != null && starImages.Length > 0)
        {
            for (int i = 0; i < starImages.Length; i++)
            {
                if (starImages[i] == null)
                {
                    continue;
                }

                if (i < stars)
                {
                    starImages[i].sprite = starOn;
                }   
                else
                {
                    starImages[i].sprite = starOff;
                } 
            }
        }
    }

    void ShowDefeat()
    {
        Time.timeScale = 0f;

        if (defeatPanel != null)
        {
            defeatPanel.SetActive(true);
        }
    }
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }



}
