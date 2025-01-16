using UnityEngine;
using UnityEngine.SceneManagement; // For scene management
using System.Collections; // Add this namespace to resolve the IEnumerator error

public class EndGameManager : MonoBehaviour
{
    public GameObject endGameUI; // Reference to the end game UI (panel or screen)
    public GameObject retryQuitUI; // UI for Retry and Quit options

    void Start()
    {
        // Ensure the EndGame screen/UI is visible
        endGameUI.SetActive(true);

        // Show Retry/Quit UI for player input
        retryQuitUI.SetActive(true);

        // Optional: Remove auto-restart after 5 seconds
        // StartCoroutine(WaitAndRestart());
    }

    public void RetryGame()
    {
        // Restart the game by loading the first level
        SceneManager.LoadScene("Level1"); // Adjust to your starting level or scene name
    }

    public void QuitGame()
    {
        // Quit the application
        Application.Quit();
    }

    IEnumerator WaitAndRestart()
    {
        // Wait for 5 seconds
        yield return new WaitForSeconds(5f);

        // Automatically restart the game after 5 seconds (optional)
        SceneManager.LoadScene("Level1");
    }
}
