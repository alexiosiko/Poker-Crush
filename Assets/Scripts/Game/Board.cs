using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static float xGap = 1f;
    public static float yGap = 1.39f;
    [SerializeField] GameObject[] cards;
	readonly public static int rows = 4;
	readonly public static int cols = 5;
    void Start()
    {
		Singleton = this;
        StartCoroutine(CreateBoard());
    }
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.C))
			StartCoroutine(CheckAndClearPokerHands());
		if (Input.GetKeyDown(KeyCode.F))
			StartCoroutine(Fall());
		if (Input.GetKeyDown(KeyCode.V))
			StartCoroutine(CreateAndFall());
		
	}
	bool loopClearCreateFall = false;
	bool isClearCreateFallLoopRunning = false;
	public void StartClearCreateFallLoop() => StartCoroutine(ClearCreateFallLoop());
	public IEnumerator ClearCreateFallLoop()
	{
		if (isClearCreateFallLoopRunning)
			yield break; // Prevent s	tarting a new instance if one is already running.
		Controller.busy = true;
		isClearCreateFallLoopRunning = true; // Mark as running.
		multiplier = 1f;
		yield return new WaitForSeconds(Game.TweenDuration + 0.01f);
		do {
			loopClearCreateFall = false;
			yield return CheckAndClearPokerHands();
			yield return new WaitForSeconds(0.01f);
			yield return StartCoroutine(Fall());
			yield return new WaitForSeconds(0.01f);
			yield return StartCoroutine(CreateAndFall());
			yield return new WaitForSeconds(0.01f);
		} while (loopClearCreateFall);

		Controller.busy = false;
		isClearCreateFallLoopRunning = false; // Mark as not running.
	}
	
	IEnumerator Fall()
	{
		for (int y = 1; y < rows; y++)
		{
			bool droppedInARow = false;
			for (int x = 0; x < cols; x++)
			{
				Transform current = GetCardAtIndex(new(x, y));
				if (!current)
					continue;
				
				for (int dy = y - 1; dy >= 0; dy--)
				{
					Transform below = GetCardAtIndex(new(x, dy));
					if (!below) {
						loopClearCreateFall = true;
						droppedInARow = true;
						current.DOMove(IndexToPos(x, dy), Game.TweenDuration);

					}
					else
						break;
				}
			}
			if (droppedInARow) {
				Sound.Singleton.Play("carddrop");
				yield return new WaitForSeconds(Game.TweenDuration);
			}
		}
	}
	IEnumerator CreateAndFall()
	{
		for (int y = 0; y < rows; y++)
		{
			bool hasCardToDropInRow = false;
			for (int x = 0; x < cols; x++)
			{
				Transform current = GetCardAtIndex(new(x, y));
				if (!current) {
					hasCardToDropInRow = true;
					GameObject cardTransform = Instantiate(cards[Random.Range(0, cards.Length)], IndexToPos(x, y + rows + 1), Quaternion.identity, transform);
					cardTransform.transform.DOMove(IndexToPos(x, y), Game.TweenDuration);
				}
			}
			if (hasCardToDropInRow)
			{
				Sound.Singleton.Play("carddrop");
				yield return new WaitForSeconds(Game.TweenDuration);
			}
		}
	}
	float multiplier = 1f;	
	public IEnumerator CheckAndClearPokerHands()
	{
		foreach (var logicFunction in Logic.logicFunctions)
		{
			HashSet<Transform> cardsToClear = new();
			for (int x = 0; x < cols - 1; x++)
			{
				for (int y = 0; y < rows; y++) 
				{
					Transform card = GetCardAtIndex(new(x, y));
					if (!card)
						continue;
					if (logicFunction.function(card, Vector2.right, cardsToClear))
					{
						Game.Singleton.AddScore(logicFunction.score);

						
						Effects.Singleton.TextEffect(logicFunction.name, new (card.position.x + logicFunction.centerOffset, card.position.y));
						if (multiplier > 1)
						{
							Effects.Singleton.TextEffect($"{multiplier}X Multiplier!", new (cols / 2 * xGap, rows / 2 * yGap), Game.TweenDuration * 2.5f);
							Game.Singleton.AddScore(logicFunction.score, Game.TweenDuration * 2.5f);
						}
					}
				}
			}
			foreach (Transform c in cardsToClear)
				c.GetComponent<Card>().Break();

			if (cardsToClear.Count > 0) { 
				if (multiplier > 1)
					Effects.Singleton.TextEffect($"{multiplier}X Multiplier!", new (cols / 2 * xGap, rows / 2 * yGap), Game.TweenDuration * 2.5f);
				Sound.Singleton.Play(cardsToClear.Count < 5 ? "smallbreak" : "largebreak");
				yield return new WaitForSeconds(Game.TweenDuration * 2);
			}
		}
		// if (hasWaitedTime == false)
		// 	yield return new WaitForSeconds(Game.TweenDuration);
		multiplier++;
	}
    IEnumerator CreateBoard()
    {
        for (int x = 0; x < cols; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                // Calculate position for each card
                Vector2 position = IndexToPos(x, y);

                // Instantiate the card and set parent
                GameObject card = Instantiate(cards[Random.Range(0, cards.Length)], new Vector2(-2, -2), Quaternion.identity, transform);
				card.transform.DOMove(position, Game.TweenDuration);
				yield return new WaitForSeconds(0.05f);
            }
        }
    }
	Vector2 IndexToPos(int x, int y) => new(x * xGap, y * yGap);
	public static Board Singleton;
	public static Transform GetCardAtMouse()
	{
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 10, LayerMask.GetMask("Card"));
		return hit.collider ? hit.collider.transform : null;
	}
	public static Transform GetCardAtIndex(Vector2 pos)
	{
		pos.x *= xGap;
		pos.y *= yGap;
		RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 100, LayerMask.GetMask("Card"));
		return hit.collider ? hit.collider.transform : null;
	}
	public static Transform GetCardAt(Vector2 pos)
	{
		RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 100, LayerMask.GetMask("Card"));
		return hit.collider ? hit.collider.transform : null;
	}

}
