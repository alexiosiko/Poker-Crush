using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static float xGap = 1f;
    public static float yGap = 1.39f;
    [SerializeField] GameObject[] cards;
    [SerializeField] GameObject[] jokers;
    [SerializeField] GameObject[] bombs;
	readonly public static int rows = 5;
	readonly public static int cols = 5;
    void Start()
    {
		Singleton = this;
        StartCoroutine(CreateBoard());
    }
	// void Update()
	// {
	// 	if (Input.GetKeyDown(KeyCode.C))
	// 		StartCoroutine(CheckAndClearPokerHands());
	// 	if (Input.GetKeyDown(KeyCode.F))
	// 		StartCoroutine(FallAtOnce());
	// 	if (Input.GetKeyDown(KeyCode.V))
	// 		StartCoroutine(CreateAndFall());
		
	// }
	bool loopClearCreateFall = false;
	bool isClearCreateFallLoopRunning = false;
	public void StartClearCreateFallLoop(float delay) => StartCoroutine(ClearCreateFallLoop(delay));
	public IEnumerator ClearCreateFallLoop(float delay)
	{
		if (isClearCreateFallLoopRunning)
			yield break; // Prevent starting a new instance if one is already running.
		isClearCreateFallLoopRunning = true; // Mark as running.
		Controller.busy = true;
		multiplier = 1;
		yield return new WaitForSeconds(delay + Static.Buffer);
		print("start loop");
		do {
			print("Loop");
			loopClearCreateFall = false;
			yield return CheckAndClearPokerHands();
			yield return StartCoroutine(Fall());
			yield return StartCoroutine(CreateAndFall());
		} while (loopClearCreateFall);
		print("done loop");

		Controller.busy = false;
		isClearCreateFallLoopRunning = false; // Mark as not running.
	}
	
	IEnumerator Fall()
	{
		bool droppedSomething = false;
		for (int x = 0; x < cols; x++)
		{
			for (int y = 0; y < rows - 1; y++)
			{
				
				if (GetCardAtIndex(x, y))
					continue;

				for (int aboveY = y + 1; aboveY < rows; aboveY++)
				{

					Transform topCard = GetCardAtIndex(x, aboveY);
					if (!topCard)
						continue;
					
					// Disable colider so the next card can fall on its prev position
					topCard.GetComponent<BoxCollider2D>().enabled = false;

					topCard.DOMove(IndexToPos(x, y), Static.TweenDuration);
					Sound.Singleton.Play("fall");
					yield return new WaitForSeconds(0.1f);
					droppedSomething = true;
					
					loopClearCreateFall = true;
					break;

				}
			}
		}
		yield return EnableAllCardColliders();
		if (droppedSomething)
			yield return new WaitForSeconds(Static.TweenDuration + Static.Buffer);

	}
	IEnumerator EnableAllCardColliders()
	{
		foreach (Transform t in transform)
			t.GetComponent<BoxCollider2D>().enabled = true;
		
		yield return null;
	}
	IEnumerator CreateAndFall()
	{
		bool didSomething = false;
		for (int y = 0; y < rows; y++)
		{
			for (int x = 0; x < cols; x++)
			{
				Transform current = GetCardAtIndex(x, y);
				if (!current) {
					didSomething = true;
					GameObject cardTransform = Instantiate(GetRandomCard(), IndexToPos(x, y + rows + 1), Quaternion.identity, transform);
					cardTransform.GetComponent<BoxCollider2D>().enabled = false;
					cardTransform.transform.DOMove(IndexToPos(x, y), Static.TweenDuration);

					yield return new WaitForSeconds(0.1f);
					Sound.Singleton.Play("fall");

					loopClearCreateFall = true;
				}
			}
		}
		yield return EnableAllCardColliders();
		if (didSomething)
			yield return new WaitForSeconds(Static.TweenDuration + Static.Buffer + 0.1f);
	}
	GameObject GetRandomCard()
	{
		// Define rarity weights for each card type
		float jokerRarity = 0.01f;
		float bombRarity = 0.01f; 

		// Generate a random value between 0 and 1
		float randomValue = Random.Range(0f, 1f);

		// Determine card type based on rarity
		if (randomValue < jokerRarity)
			return jokers[Random.Range(0, jokers.Length)];
		else if (randomValue < jokerRarity + bombRarity)
			return bombs[Random.Range(0, jokers.Length)];

		// Return a random card from the normal deck
		return cards[Random.Range(0, cards.Length)];
	}
	int multiplier = 1;	
	public IEnumerator CheckAndClearPokerHands()
	{
		foreach (var logicFunction in Logic.logicFunctions)
		{
			HashSet<Transform> cardsToClear = new();
			for (int x = 0; x < cols - 1; x++)
			{
				for (int y = 0; y < rows; y++) 
				{
					Transform card = GetCardAtIndex(x, y);
					if (!card)
						continue;
					if (logicFunction.function(card, Vector2.right, cardsToClear))
					{
						Game.Singleton.AddScore(logicFunction.score * multiplier);

						
						Effects.Singleton.TextEffect(logicFunction.name, new (card.position.x + logicFunction.centerOffset, card.position.y));

					}
				}
			}
			foreach (Transform c in cardsToClear)
				c.GetComponent<Card>().Break();

			if (cardsToClear.Count > 0) { 
				loopClearCreateFall = true;
				Sound.Singleton.Play(cardsToClear.Count < 5 ? "smallbreak" : "largebreak");
				yield return new WaitForSeconds(Static.BreakDuration + Static.Buffer);
			}
		}
		if (multiplier > 2)
			Effects.Singleton.TextEffect($"{multiplier}X Multi!", new (cols / 2 * xGap, rows / 2 * yGap));
		// if (hasWaitedTime == false)
		// 	yield return new WaitForSeconds(Game.TweenDuration);
		multiplier++;
	}
    IEnumerator CreateBoard()
    {
		Controller.busy = true;
        for (int x = 0; x < cols; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                // Calculate position for each card
                Vector2 position = IndexToPos(x, y);

                // Instantiate the card and set parent
                GameObject card = Instantiate(GetRandomCard(), new Vector2(-2, -2), Quaternion.identity, transform);
				card.transform.DOMove(position, Static.TweenDuration);
				Sound.Singleton.Play("deal");
				yield return new WaitForSeconds(0.1f);
            }
        }
		yield return new WaitForSeconds(Static.Buffer);
		Controller.busy = false;
    }
	Vector2 IndexToPos(int x, int y) => new(x * xGap, y * yGap);
	public static Board Singleton;
	public static Transform GetCardAtMouse()
	{
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 10, LayerMask.GetMask("Card"));
		return hit.collider ? hit.collider.transform : null;
	}
	public static Transform GetCardAtIndex(int x, int y)
	{
		Vector2 pos = new ((float)x * xGap, (float)y * yGap);
		RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 100, LayerMask.GetMask("Card"));
		return hit.collider ? hit.collider.transform : null;
	}
	public static Transform GetCardAt(Vector2 pos)
	{
		RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 100, LayerMask.GetMask("Card"));
		return hit.collider ? hit.collider.transform : null;
	}

}
