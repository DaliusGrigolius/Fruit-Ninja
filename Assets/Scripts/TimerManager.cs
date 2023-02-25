using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public float gameTimeSeconds = 181f;
    public Text timerText;
    private float timeRemaining;

    private void Start()
    {
        timeRemaining = gameTimeSeconds;
    }

    private void Update()
    {
        if (timeRemaining > 0f)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerText();

            if (timeRemaining < 60f)
            {
                timerText.color = Color.red;
            }
        }
        else
        {
            timerText.text = "00:00";
            FindObjectOfType<GameManager>().ShowGameOverPanel();
        }
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void ResetTimer()
    {
        timeRemaining = gameTimeSeconds;
        UpdateTimerText();
    }
}
