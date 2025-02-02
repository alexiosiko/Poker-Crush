using UnityEngine;
using DG.Tweening;
using System.Collections;
using TMPro;

public class Effects : MonoBehaviour
{
	[SerializeField] GameObject textPrefab;
	public void TextEffect(string text, Vector2 pos, float delay = 0f, int score = -1) => StartCoroutine(TextEffectCoroutine(text, pos, delay, score));
	IEnumerator TextEffectCoroutine(string text, Vector2 pos, float delay, int score)
	{
		yield return new WaitForSeconds(delay);
		GameObject obj = Instantiate(textPrefab, transform);
		obj.transform.localScale = Vector3.zero;
		Vector2 viewPosition = Camera.main.WorldToScreenPoint(pos);
		obj.transform.position = new (viewPosition.x, viewPosition.y, obj.transform.position.z);
		TMP_Text textObj = obj.GetComponentInChildren<TMP_Text>();
		textObj.text = text;
		obj.transform.DOScale(1, 0.2f).SetUpdate(true);
		obj.transform.DOShakeRotation(1, 30).SetUpdate(true);
		yield return new WaitForSeconds(1f);
		if (score != -1)
			yield return PointsEffects(obj, score);
		else
		{
			textObj.DOFade(0, 0.5f);
			yield return new WaitForSeconds(0.5f);
		}
		Destroy(obj);
	}
	IEnumerator PointsEffects(GameObject obj, int score)
	{
		obj.transform.DOShakeScale(0.2f, 1);
		obj.transform.DOScaleY(0, 0.1f);
		yield return new WaitForSeconds(0.1f);
		obj.GetComponentInChildren<TMP_Text>().text = score.ToString();
		obj.transform.DOScaleY(1, 0.1f);
		yield return new WaitForSeconds(0.5f);
		obj.transform.DOMove(Score.Singleton.GetComponent<RectTransform>().position, 0.4f).SetUpdate(true);
		obj.transform.GetComponent<TMP_Text>().DOFade(0, 0.4f);
		yield return new WaitForSeconds(0.2f);
		Score.Singleton.AddScore(score, 0);
		yield return new WaitForSeconds(0.2f);
		
	}
	void Awake() => Singleton = this;
	public static Effects Singleton;
}
