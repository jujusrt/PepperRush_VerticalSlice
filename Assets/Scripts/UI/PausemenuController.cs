using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenuPanel;

    public InputSystem_Actions input;

    private bool isOpen = false;


    void Start()
    {
        input = new InputSystem_Actions();
        input.Enable();

        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(false);
        }
    }

    void Update()
    {
        if (input.Player.Menu.WasPressedThisFrame())
        {
            if (isOpen)
            {
                Time.timeScale = 1f;
                CloseMenu();
            }
            else
            {
                OpenMenu();
                Time.timeScale = 0f;
            }
        }
    }

    public void OpenMenu()
    {
        isOpen = true;

        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(true);
        }
    }

    public void CloseMenu()
    {
        isOpen = false;

        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        if (input != null)
        {
            input.Player.Disable();
            input.UI.Disable();
        }
    }
}