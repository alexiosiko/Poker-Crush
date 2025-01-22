using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
	public TMP_Text timeText;   // UI Text component to display time
    private float elapsedTime = 0f; // Time elapsed since the start
    private int score = 0;          // Player's score

    void Update()
    {
        // Increment the elapsed time
        elapsedTime += Time.deltaTime;

        // Update the time display
        UpdateTimeDisplay();
    }

    void UpdateTimeDisplay()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void AddScore(int points)
    {
        score += points;
    }
}
