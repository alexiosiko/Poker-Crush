using System.Collections;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
	[SerializeField] AudioClip[] clips;
	[SerializeField] AudioSource source;
	[SerializeField] public TMP_Text scoreText;
	public void AddScore(int score, float delay = 1f) => StartCoroutine(AddScoreIenumerator(score, delay));
	IEnumerator AddScoreIenumerator(int score, float delay)
	{
		yield return new WaitForSeconds(delay);
		yield return IncrementScore(score);
		Game.Singleton.CheckWinCondition(this.scoreText);
	}
	public void RemoveScore(int score) => StartCoroutine(RemoveScoreIEnumerator(score));
	IEnumerator RemoveScoreIEnumerator(int score)
	{
		while (score > 0) {
			this.scoreText.text = (int.Parse(this.scoreText.text) - 1).ToString();
			score--;
			source.clip = clips[Random.Range(0, clips.Length)];
			source.Play();
			yield return new WaitForSeconds(0.001f);
		}
		Game.Singleton.CheckWinCondition(this.scoreText);
	}
	IEnumerator IncrementScore(int score)
	{
		while (score > 0) {
			this.scoreText.text = (int.Parse(this.scoreText.text) + 1).ToString();
			score--;
			source.clip = clips[Random.Range(0, clips.Length)];
			source.Play();
			yield return new WaitForSeconds(0.025f);
		}
	}
	public static Score Singleton;
	void Awake() => Singleton = this;
}
