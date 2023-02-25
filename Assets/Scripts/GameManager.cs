using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Score Elements")]
    public int score;
    public int highscore;
    public Text scoreText;
    public Text highscoreText;

    [Header("Game Over")]
    public GameObject gameOverPanel;
    public GameObject startGamePanel;
    public Text gameOverPanelScoreText;
    public Text gameOverPanelHighScoreText;


    [Header("Sounds")]
    public AudioClip[] sliceSounds;
    public AudioClip bombHitSound;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        Time.timeScale = 0;
        GetHighscore();
    }

    private void GetHighscore()
    {
        highscore = PlayerPrefs.GetInt("Highscore");
        highscoreText.text = "Best: " + highscore;
    }

    public void IncreaseScore(int points)
    {
        score += points;
        scoreText.text = score.ToString();

        if (score > highscore)
        {
            PlayerPrefs.SetInt("Highscore", score);
            highscoreText.text = score.ToString();
        }
    }

    public void OnBombHit()
    {
        audioSource.PlayOneShot(bombHitSound);
        ShowGameOverPanel();
    }

    public void ShowGameOverPanel()
    {
        Time.timeScale = 0;
        gameOverPanelScoreText.text = "Score: " + score;
        gameOverPanelHighScoreText.text = "Best: " + PlayerPrefs.GetInt("Highscore");
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        score = 0;
        scoreText.text = score.ToString();
        gameOverPanel.SetActive(false);

        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Interactable"))
        {
            Destroy(g);
        }

        FindObjectOfType<TimerManager>().ResetTimer();
        Time.timeScale = 1;
    }

    public void StartGame()
    {
        startGamePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayRandomSliceSound()
    {
        AudioClip randomSound = sliceSounds[Random.Range(0, sliceSounds.Length)];
        audioSource.PlayOneShot(randomSound);
    }
}
