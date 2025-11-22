using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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


    private void Start()
    {
        currentTime = startTime;

        if (victoryPanel != null) victoryPanel.SetActive(false);
        if (defeatPanel != null) defeatPanel.SetActive(false);
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
        if (levelFinished) return;

        levelFinished = true;

        int stars = 0;

        if (currentTime > 30f)
            stars = 3;
        else if (currentTime > 15f)
            stars = 2;
        else if (currentTime > 0f)
            stars = 1;
        else
            stars = 0;

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
            victoryPanel.SetActive(true);

        if (starImages != null && starImages.Length > 0)
        {
            for (int i = 0; i < starImages.Length; i++)
            {
                if (starImages[i] == null) continue;

                if (i < stars)
                    starImages[i].sprite = starOn;
                else
                    starImages[i].sprite = starOff;
            }
        }
    }

    void ShowDefeat()
    {
        Time.timeScale = 0f;

        if (defeatPanel != null)
            defeatPanel.SetActive(true);
    }
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }



}
