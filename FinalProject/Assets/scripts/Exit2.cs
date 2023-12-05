using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackToMainMenu2 : MonoBehaviour
{
    public Button backButton;

    private void Start()
    {
        // Add a click event listener to the "Back" button.
        backButton.onClick.AddListener(LoadMainMenu);
    }

    private void Update()
    {
        // Check if the "Esc" key is pressed.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadMainMenu();
        }
    }

    private void LoadMainMenu()
    {
        // Load the "MainMenu" scene.
        SceneManager.LoadScene("Start Menu");
    }
}
