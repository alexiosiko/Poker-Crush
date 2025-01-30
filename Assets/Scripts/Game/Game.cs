using System.Collections;
using TMPro;
using UnityEngine;

public class Game : MonoBehaviour
{
	readonly int winScore = 1000;
	[SerializeField] GameObject loseObject;
	[SerializeField] GameObject winObject;
	[SerializeField] TMP_Text timeText;

	
	void Awake() => Singleton = this;
	public static Game Singleton;
	public void CheckWinCondition(TMP_Text score)
	{
		if (int.Parse(score.text) > winScore)
			Win();
		else if (int.Parse(score.text) < 0)
			Lose();
	}
	void Win()
	{
		winObject.SetActive(true);
		loseObject.SetActive(false);
		timeText.text = TimeManager.Singleton.timeText.text;
	}
	void Lose()
	{
		winObject.SetActive(false);
		loseObject.SetActive(true);
	}
	void Close()
	{
		winObject.SetActive(false);
		loseObject.SetActive(false);
	}
	void Start() => Application.targetFrameRate = (int)Screen.currentResolution.refreshRateRatio.value;
	// public void ResetGame()
	// {
	// 	StopAllCoroutines();
	// 	Close(); 
	// 	score.text = "200";
	// 	Board.Singleton.ResetBoard();
	// }
}
