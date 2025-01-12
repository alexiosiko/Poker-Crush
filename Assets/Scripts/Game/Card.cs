using System.Collections;
using DG.Tweening;
using UnityEngine;

public enum Suit {
	clubs,
	hearts,
	spades,
	diamonds,
	joker,
	bomb,
}
public class Card : MonoBehaviour
{
	public short number;
	public Suit suit;
	public virtual void Break()
	{
		if (isBreaking)
			return;
		isBreaking = true;
		StartCoroutine(BreakIEnumerator());
	} 
	[SerializeField] protected bool isBreaking = false;
	protected IEnumerator BreakIEnumerator()
	{
		transform.DOShakeRotation(0.5f, 20);
		transform.DOScale(0, Game.TweenDuration);
		yield return new WaitForSeconds(Game.TweenDuration);
		Destroy(gameObject);
	}
}


