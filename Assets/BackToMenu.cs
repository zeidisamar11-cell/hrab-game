using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    private void Update()
    {
        if (Keyboard.current == null) return;

        if (Keyboard.current.escapeKey.wasPressedThisFrame || Keyboard.current.mKey.wasPressedThisFrame)
        {
            Debug.Log("Retour au menu");
            GoToMenu();
        }
    }

    private void GoToMenu() => SceneManager.LoadScene("MainMenu");
}