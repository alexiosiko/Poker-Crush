using TMPro;
using UnityEngine;
public class TimeManager : MonoBehaviour
{
	public TMP_Text timeText;
    private float elapsedTime = 0f;
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
	public static TimeManager Singleton;
	void Awake() => Singleton = this;
}
