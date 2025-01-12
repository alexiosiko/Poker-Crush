using System;
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
	void Release()
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
		selectedCard.DOMove(swapCardPos, Game.TweenDuration);
		selectedCard = null;
		swapCard.DOMove(selectedCardPos, Game.TweenDuration);
		Sound.Singleton.Play(new string[] {"move", "move1"});

		Board.Singleton.StartClearCreateFallLoop();
		Game.Singleton.RemoveScore(20);
	}

	void Click()
	{
		Transform cardTransform = Board.GetCardAtMouse();
		if (!cardTransform)
			return;
		selectedCard = cardTransform;
		if (busy)
			return;
		Card c = cardTransform.GetComponent<Card>();
		if (c.suit == Suit.joker) {
			busy = true;
			c.Break();
		}

	}
	// IEnumerator Release()
	// {
	// 	List<Transform> highlighted = Game.Singleton.highlighted;
	// 	bool score = false;
	// 	if (highlighted.Count >= 3)
	// 		score = true;

	// 	foreach (Transform t in highlighted)
	// 	{
	// 		t.GetComponent<SpriteRenderer>().color = Color.white;
	// 		if (score) 
	// 			t.GetComponent<Card>().Break();
	// 	}
	// 	highlighted.Clear();

	// 	yield return new WaitForSeconds(0.2f);
	// 	yield return StartCoroutine(Board.Singleton.FallAndCreate());
	// 	busy = false;
	// }
	// void Drag()
	// {
	// 	RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 100, LayerMask.GetMask("Card"));
	// 	if (!hit.collider)
	// 		return;
			
	// 	Transform cardTransform = hit.collider.transform;
	// 	List<Transform> highlighted = Game.Singleton.highlighted;

	// 	if (highlighted.Contains(cardTransform))
	// 		return;
	// 	if (!CanSelectCard(cardTransform, highlighted))
	// 		return;

	// 	highlighted.Add(cardTransform);
	// 	cardTransform.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);

	// }

	// bool CanSelectCard(Transform cardTransform, List<Transform> highlighted)
	// {
	// 	if (highlighted.Count == 0)
	// 		return true;

	// 	if (!isAdjacent(cardTransform, highlighted))
	// 		return false;

	// 	Card cardOther = cardTransform.GetComponent<Card>();
	// 	Card cardRecent = highlighted[highlighted.Count - 1].GetComponent<Card>();

	// 	if (Math.Abs(cardOther.cardValue - cardRecent.cardValue) == 1 ||
	// 		Math.Abs(cardOther.cardValue - cardRecent.cardValue) == 12 ||
	// 		cardOther.cardValue - cardRecent.cardValue == 0
	// 	) 
	// 		return true;

	// 	if (cardOther.suit == cardRecent.suit)
	// 		return true;
	// 	return false;
	// }
	// bool isAdjacent(Transform cardTransform, List<Transform> highlighted)
	// {
	// 	const float epsilon = 0.1f; // Small tolerance value for floating-point comparison

	// 	if (highlighted.Count == 0)
	// 		return true;

	// 	Transform prevCardTransform = highlighted[highlighted.Count - 1];
	// 	float dx = prevCardTransform.position.x - cardTransform.position.x;
	// 	float dy = prevCardTransform.position.y - cardTransform.position.y;


	// 	// Use tolerance for comparisons
	// 	if (Math.Abs(dx) - Board.xGap < epsilon && Math.Abs(dy) < epsilon 
	// 		|| Math.Abs(dy) - Board.yGap < epsilon && Math.Abs(dx) < epsilon)
	// 		return true;

	// 	return false;
	// }
}
