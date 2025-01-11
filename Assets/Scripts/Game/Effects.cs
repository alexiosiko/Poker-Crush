using UnityEngine;
using DG.Tweening;
using System.Collections;
using TMPro;
using Unity.Mathematics;

public class Effects : MonoBehaviour
{
	[SerializeField] GameObject textPrefab;
	[SerializeField] GameObject textDestructivePrefab;
	public void TextEffect(string text, Vector2 pos, float delay = 0f) => StartCoroutine(TextEffectCoroutine(text, pos, delay));
	IEnumerator TextEffectCoroutine(string text, Vector2 pos, float delay)
	{
		yield return new WaitForSeconds(delay);
		GameObject obj = Instantiate(textPrefab, new Vector3(pos.x, pos.y, -1), Quaternion.identity);
		obj.GetComponentInChildren<TMP_Text>().text = text;
		obj.transform.DOScale(1, 1f);
		obj.transform.DOShakeRotation(1, 30);
		yield return new WaitForSeconds(0.5f);
		obj.transform.GetComponentInChildren<TMP_Text>().DOFade(0, 0.5f);
		obj.transform.DOMove(Game.Singleton.score.transform.position, 0.5f);
		yield return new WaitForSeconds(0.5f);
		obj.transform.DOKill();
		Destroy(obj);
	}
	public void TextEffectDestructive(string text, Vector2 pos)
	{
		GameObject textObj = Instantiate(textDestructivePrefab, new Vector3(pos.x, pos.y, -1), quaternion.identity);
		textObj.transform.GetComponent<TMP_Text>().DOFade(0, 1f);
		Destroy(textObj, 1f);
	}
	void Awake() => Singleton = this;
	public static Effects Singleton;
}
