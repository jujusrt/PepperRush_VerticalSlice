using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public class Cheats : MonoBehaviour
{
    public InputSystem_Actions input;

    private void Start()
    {
        input = new InputSystem_Actions();
        input.Enable();
    }
    void Update()
    {
        if (input.Player.Scene1.WasPressedThisFrame())
        {
            SceneManager.LoadScene("Level1");
        }

        if (input.Player.Scene2.WasPressedThisFrame())
        {
            SceneManager.LoadScene("Level2");
        }
        if (input.Player.Scene3.WasPressedThisFrame())
        {
            SceneManager.LoadScene("Level3");
        }
        if (input.Player.MenuSce.WasPressedThisFrame())
        {
            SceneManager.LoadScene("MenuScene");
        }
    }

    private void OnDestroy()
    {
        input.Player.Disable();
        input.UI.Disable();
    }
}