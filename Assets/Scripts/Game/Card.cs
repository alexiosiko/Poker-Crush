using System.Collections;
using DG.Tweening;
using NUnit.Framework;
using UnityEngine;

public enum Suit {
	clubs,
	hearts,
	spades,
	diamonds,
	joker
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
		StartCoroutine(BreakIenumeartor());
	} 
	protected bool isBreaking = false;
	IEnumerator BreakIenumeartor()
	{
		transform.DOShakeRotation(0.2f, 20);
		yield return new WaitForSeconds(Game.TweenDuration);
		transform.DOScale(0, Game.TweenDuration);
		yield return new WaitForSeconds(Game.TweenDuration);
		Destroy(gameObject);
	}
}


