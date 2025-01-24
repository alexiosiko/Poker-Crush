using System.Collections;
using TMPro;
using UnityEngine;

public class Game : MonoBehaviour
{
	[SerializeField] public TMP_Text score;
	readonly int winScore = 10000;
	[SerializeField] GameObject loseObject;
	[SerializeField] GameObject winObject;
	public void AddScore(int score, float delay = 1f) => StartCoroutine(AddScoreIenumerator(score, delay));
	IEnumerator AddScoreIenumerator(int score, float delay)
	{
		yield return new WaitForSeconds(delay);
		yield return IncrementScore(score);
		CheckWinCondition();
	}
	public void RemoveScore(int score) => StartCoroutine(RemoveScoreIEnumerator(score));
	IEnumerator RemoveScoreIEnumerator(int score)
	{
		while (score > 0) {
			this.score.text = (int.Parse(this.score.text) - 1).ToString();
			score--;
			yield return new WaitForSeconds(0.005f);
		}
		CheckWinCondition();
	}
	IEnumerator IncrementScore(int score)
	{
		while (score > 0) {
			this.score.text = (int.Parse(this.score.text) + 1).ToString();
			score--;
			yield return new WaitForSeconds(0.025f);
		}
	}
	void Awake() => Singleton = this;
	public static Game Singleton;
	void CheckWinCondition()
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
