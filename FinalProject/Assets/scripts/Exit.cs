using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackToMainMenu : MonoBehaviour
{
    public Button backButton;

    private void Start()
    {
        // Add a click event listener to the "Back" button.
        backButton.onClick.AddListener(LoadMainMenu);
    }

    private void LoadMainMenu()
    {
        // Load the "MainMenu" scene when the button is clicked.
        SceneManager.LoadScene("Start Menu");
    }
}

