using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Windows;

public class TutorialClickDialog : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text dialogText;
    [SerializeField] private Image faceImage;

    [Header("Faces")]
    [SerializeField] private Sprite defaultFace;
    [SerializeField] private Sprite lastFace;

    [Header("Tutorial steps")]
    [TextArea(2, 6)]
    [SerializeField] private string[] steps;

    [Header("Typewriter")]
    [SerializeField] private float charsPerSecond = 35f;

    [Header("Disable during tutorial")]
    [SerializeField] private MonoBehaviour playerShoot;

    private int index = 0;

    private string currentText;
    private int charIndex = 0;
    private float charTimer = 0f;

    private bool isTyping = false;
    private bool textCompleted = false;

    public InputSystem_Actions input;


    private void Start()
    {
        if (steps == null || steps.Length == 0)
        {
            panel.SetActive(false);
            return;
        }

        panel.SetActive(true);

        if (playerShoot) playerShoot.enabled = false;

        index = 0;
        StartStep(index);

        input = new InputSystem_Actions();
        input.Enable();
    }

    private void Update()
    {
        if (!panel.activeSelf) return;

        HandleTypewriter();
        HandleClick();
    }

    private void HandleTypewriter()
    {
        if (!isTyping) return;

        charTimer += Time.deltaTime;
        float interval = 1f / Mathf.Max(1f, charsPerSecond);

        while (charTimer >= interval && isTyping)
        {
            charTimer -= interval;

            charIndex++;
            if (charIndex >= currentText.Length)
            {
                CompleteText();
                break;
            }

            dialogText.text = currentText.Substring(0, charIndex);
        }
    }

    private void HandleClick()
    {
        if (!input.Player.Shoot.WasPressedThisFrame()) return;

        if (isTyping)
        {
            CompleteText();
        }

        else if (textCompleted)
        {
            NextStep();
        }
    }

    private void StartStep(int i)
    {
        currentText = steps[i];
        dialogText.text = "";
        charIndex = 0;
        charTimer = 0f;

        isTyping = true;
        textCompleted = false;

        SetFaceForIndex(i);
    }

    private void CompleteText()
    {
        dialogText.text = currentText;
        isTyping = false;
        textCompleted = true;
    }

    private void NextStep()
    {
        index++;

        if (index >= steps.Length)
        {
            EndTutorial();
            return;
        }

        StartStep(index);
    }

    private void SetFaceForIndex(int i)
    {
        if (!faceImage) return;

        faceImage.sprite = (i == steps.Length - 1)
            ? lastFace
            : defaultFace;
    }

    private void EndTutorial()
    {
        panel.SetActive(false);

        if (playerShoot) playerShoot.enabled = true;
    }

    private void OnDestroy()
    {
        input.Player.Disable();
        input.UI.Disable();
    }
}
