using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Game : MonoBehaviour
{
	[SerializeField] public TMP_Text score;
	// [SerializeField] TMP_Text moves;
	[SerializeField] public static float TweenDuration = 0.25f;
	public List<Transform> highlighted;
	// public void AddMoves(int s) => moves.text = (int.Parse(moves.text) + s).ToString();
	public void AddScore(int score, float delay = 1f) => StartCoroutine(AddScoreIenumerator(score, delay));
	IEnumerator AddScoreIenumerator(int score, float delay)
	{
		yield return new WaitForSeconds(delay);
		yield return IncrementScore(score);
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
}
