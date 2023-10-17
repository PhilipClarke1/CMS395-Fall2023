using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartTutorial : MonoBehaviour
{
    public Button startTutorialButton;

    private void Start()
    {
        // Add a click event listener to the "Start Tutorial" button.
        startTutorialButton.onClick.AddListener(StartTutorial2);
    }

    private void StartTutorial2()
    {
        // Load the "tutorial" scene when the button is clicked.
        SceneManager.LoadScene("help");
    }
}
