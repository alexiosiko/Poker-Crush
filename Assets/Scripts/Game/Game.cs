using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Game : MonoBehaviour
{
	[SerializeField] public TMP_Text score;
	int winScore = 1000;
	[SerializeField] GameObject menuObject;
	[SerializeField] GameObject winObject;
	public static float TweenDuration = 0.25f;
	public static float TweenBuffer = 0.05f;
	public List<Transform> highlighted;
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
		print("remove");
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
		if (int.Parse(this.score.text) > winScore)
			Win();
		else if (int.Parse(this.score.text) < 0)
			Lose();
	}
	void Win()
	{
		winObject.SetActive(true);
		menuObject.SetActive(false);
	}
	void Lose()
	{
		winObject.SetActive(false);
		menuObject.SetActive(true);
	}
	void Close()
	{
		winObject.SetActive(false);
		menuObject.SetActive(false);
	}
}
