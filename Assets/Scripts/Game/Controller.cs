using System;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] public static bool busy = false;
	Transform selectedCard;
    void Update()
    {
		if (Input.GetMouseButtonDown(0))
			Click();
		if (!busy && Input.GetMouseButtonUp(0))
			Release();

		// if (Input.GetMouseButtonUp(0)) {
		// 	busy = true;
		// 	StartCoroutine(Release());

		// }
		// if (busy)
		// 	return;
        // if (Input.GetMouseButton(0))
		// 	Drag();
    }
	async void Release()
	{
		if (!selectedCard)
			return;

		Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		float dx = mousePos.x - selectedCard.position.x;
		float dy = mousePos.y - selectedCard.position.y;
		if (Math.Abs(dx) < 0.5f && Math.Abs(dy) < 0.5f)
			return;

		Transform swapCard;
		if (Math.Abs(dx) > Math.Abs(dy))
		{
			if (dx > 0)
				swapCard = Board.GetCardAt(new Vector2(selectedCard.position.x + Board.xGap, selectedCard.position.y));
			else
				swapCard = Board.GetCardAt(new Vector2(selectedCard.position.x - Board.xGap, selectedCard.position.y));
		}
		else
		{
			if (dy > 0)
				swapCard = Board.GetCardAt(new Vector2(selectedCard.position.x, selectedCard.position.y + Board.yGap));
			else
				swapCard = Board.GetCardAt(new Vector2(selectedCard.position.x, selectedCard.position.y - Board.yGap));
		}

		if (!swapCard)
			return;

		busy = true;

		Vector3 selectedCardPos = selectedCard.position;
		Vector3 swapCardPos = swapCard.position;

		// Play sound, start loop, and update score after animations complete
		Game.Singleton.RemoveScore(20);
		Sound.Singleton.Play(new string[] { "move1", "move2", "move3" });
		
		// Start both animations and wait for them to complete
		Task card1 = selectedCard.DOMove(swapCardPos, Static.TweenDuration).SetUpdate(true).AsyncWaitForCompletion();
		Task card2 = swapCard.DOMove(selectedCardPos, Static.TweenDuration).SetUpdate(true).AsyncWaitForCompletion();
		await Task.WhenAll(card1, card2);
		selectedCard = null;


		await Board.Singleton.ClearCreateFallLoop();
		busy = false;
	}


	async void Click()
	{
		Transform cardTransform = Board.GetCardAtMouse();
		if (!cardTransform)
			return;
		selectedCard = cardTransform;
		if (busy)
			return;
		busy = true;
		Card c = cardTransform.GetComponent<WildCard>();
		if (c)
			await c.Break();
		await Board.Singleton.ClearCreateFallLoop();
		
		busy = false;
	}
}
