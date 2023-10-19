using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public Button newGameButton;

    private void Start()
    {
        // Add a click event listener to the "New Game" button.
        newGameButton.onClick.AddListener(LoadGameScene);
    }

    private void LoadGameScene()
    {
        // Load the "game" scene when the button is clicked.
        SceneManager.LoadScene("game");
    }
}
