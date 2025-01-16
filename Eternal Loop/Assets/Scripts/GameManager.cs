using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject gameOverContent;
    public GameObject startGameContent;
    public GameObject player;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI levelText; // New TextMeshProUGUI reference to show the level

    public int lives = 3; // Total number of lives the player starts with
    public TextMeshProUGUI livesText; //Reference to display remaining lives

    private bool isGameOver = true;
    private int score = 0;
    public int scoreToNextLevel = 5;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            // Load the lives from PlayerPrefs (if available)
            lives = PlayerPrefs.GetInt("Lives", 3); // Default to 3 if no saved value
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Initialize the lives text
        UpdateLivesText();
        UpdateLevelText(); // Update the level text at the start
    }

    private void Update()
    {
        if (score >= scoreToNextLevel)
        {
            ProceedToNextLevel();
        }
    }

    public void OnHideGameStartUIAnimationEnd()
    {
        // Logic to start the game after the start UI hides
        if (startGameContent.activeSelf)
        {
            startGameContent.SetActive(false); // Hide StartGame UI
        }

        StartGame(); // Begin the game
    }

    public void OnHideGameOverUIAnimationEnd()
    {
        // Logic to restart or reset the game
        if (gameOverContent.activeSelf)
        {
            gameOverContent.SetActive(false); // Hide GameOver text and PlayAgain button
        }

        StartGame(); // Restart the game
    }

    public void StartGame()
    {
        score = 0;
        lives = 3; // Reset lives to 3 only when starting a new game
        scoreText.text = "Score: " + 0;
        UpdateLivesText(); // Update the lives text UI
        isGameOver = false;
        player.SetActive(true);
        PlayerPrefs.SetInt("Lives", lives); // Save the lives at the start of the game
        UpdateLevelText(); // Ensure the level is displayed at the start
    }

    public void GameOver()
    {
        SoundManager.instance.PlayDestroySound();
        gameOverContent.SetActive(true);
        UIAnimationsManager.instance.ShowGameOverUIControls();
        isGameOver = true;
        player.SetActive(false);
        PlayerPrefs.SetInt("Lives", lives); // Save lives when the game ends
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }

    public void AddScore()
    {
        SoundManager.instance.PlayShootSound();
        score++;
        scoreText.text = "Score: " + score.ToString();
    }

    public void LoseLife()
    {
        lives--; // Decrease lives
        UpdateLivesText(); // Update the lives UI

        // Check if the player has run out of lives
        if (lives <= 0)
        {
            GameOver();
        }
        else
        {
            PlayerPrefs.SetInt("Lives", lives); // Save remaining lives
        }
    }

    private void UpdateLivesText()
    {
        livesText.text = "Lives: " + lives; // Update the lives display
    }

    private void UpdateLevelText()
    {
        if (levelText == null)
        {
            Debug.LogError("Level Text is not assigned in the Inspector.");
            return;
        }

        string level = SceneManager.GetActiveScene().name;
        if (level == "Level1")
        {
            levelText.text = "Level 1";
        }
        else if (level == "Level2")
        {
            levelText.text = "Level 2";
        }
    }

    public void ProceedToNextLevel()
    {
        if (score >= scoreToNextLevel && SceneManager.GetActiveScene().name == "Level2")
        {
            // Load the EndGame scene after finishing level 2
            SceneManager.LoadScene("EndGame");
        }
        else
        {
            // Save lives before transitioning
            PlayerPrefs.SetInt("Lives", lives);
            // Proceed to next level, but don't reset lives
            SceneManager.LoadScene("Level2");
        }
    }
}
