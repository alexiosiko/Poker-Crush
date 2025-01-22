using UnityEngine;
using DG.Tweening;
using System.Collections;
using TMPro;

public class Effects : MonoBehaviour
{
	[SerializeField] GameObject textPrefab;
	public void TextEffect(string text, Vector2 pos, float delay = 0f) => StartCoroutine(TextEffectCoroutine(text, pos, delay));
	IEnumerator TextEffectCoroutine(string text, Vector2 pos, float delay)
	{
		yield return new WaitForSeconds(delay);
		GameObject obj = Instantiate(textPrefab);
		obj.transform.position = new (obj.transform.position.x, pos.y, obj.transform.position.z);
		TMP_Text textObj = obj.GetComponentInChildren<TMP_Text>();
		textObj.text = text;
		obj.transform.DOScale(1, 1f).SetUpdate(true);
		obj.transform.DOShakeRotation(1, 30).SetUpdate(true);
		yield return new WaitForSeconds(1f);
		textObj.DOFade(0, 0.5f);
		// obj.transform.DOMove(Game.Singleton.score.transform.position, 0.5f).SetUpdate(true);
		yield return new WaitForSeconds(1f);
		Destroy(obj);
	}

	void Awake() => Singleton = this;
	public static Effects Singleton;
}
