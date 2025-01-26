using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
	public void ResetBoard()
	{
		StopAllCoroutines();
		foreach (Transform t in transform)
			Destroy(t.gameObject);
        CreateBoard();
		
	}
    void Start()
    {
		Singleton = this;
        CreateBoard();
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
	public async Task ClearCreateFallLoop()
    {
        if (isClearCreateFallLoopRunning)
            return; // Prevent starting a new instance if one is already running.

        isClearCreateFallLoopRunning = true; // Mark as running.
        multiplier = 1;


        do
        {
            loopClearCreateFall = false;

            // Await CheckAndClearPokerHands, Fall, and CreateAndFall sequentially.
            await ClearPokerHands();
            await Fall();
            await CreateAndFall();

        } while (loopClearCreateFall);

        isClearCreateFallLoopRunning = false; // Mark as not running.
    }

    private async Task Fall()
    {

		List<Task> tasks = new();
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

                    // Disable collider so the next card can fall on its prev position
                    topCard.GetComponent<BoxCollider2D>().enabled = false;
                    tasks.Add(topCard.DOMove(IndexToPos(x, y), Static.TweenDuration).SetUpdate(true).AsyncWaitForCompletion());
					await Task.Delay(Static.Delay);
                    Sound.Singleton.Play("fall");

                    loopClearCreateFall = true;
                    break;
                }
            }
        }
		await Task.WhenAll(tasks);
        await EnableAllCardColliders();
    }

    private async Task EnableAllCardColliders()
    {
        foreach (Transform t in transform)
            t.GetComponent<BoxCollider2D>().enabled = true;

        await Task.Yield(); // Yield control back to the main thread.
    }

    private async Task CreateAndFall()
    {

		List<Task> tasks = new();
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                Transform current = GetCardAtIndex(x, y);
                if (!current)
                {
                    GameObject cardTransform = Instantiate(GetRandomCard(), IndexToPos(x, y + rows + 1), Quaternion.identity, transform);
                    cardTransform.GetComponent<BoxCollider2D>().enabled = false;
                    tasks.Add(cardTransform.transform.DOMove(IndexToPos(x, y), Static.TweenDuration * 2).SetUpdate(true).AsyncWaitForCompletion());

                    Sound.Singleton.Play("fall");

                    loopClearCreateFall = true;
					await Task.Delay(Static.Delay);
                }
            }
        }
		await Task.WhenAll(tasks);
        await EnableAllCardColliders();
    }

    private async Task ClearPokerHands()
    {
        bool clearedSomething = false;

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

                        Effects.Singleton.TextEffect(logicFunction.name, new(card.position.x + logicFunction.centerOffset, card.position.y));
                    }
                }
            }

            List<Task> breakTasks = new();
            foreach (Transform c in cardsToClear)
                breakTasks.Add(c.GetComponent<Card>().Break());

            if (cardsToClear.Count > 0)
            {
                clearedSomething = true;
                loopClearCreateFall = true;
                Sound.Singleton.Play(cardsToClear.Count < 5 ? "smallbreak" : "largebreak");
            }
            // Wait for all break tasks to complete.
            await Task.WhenAll(breakTasks);

        }

        if (clearedSomething && multiplier > 1)
            Effects.Singleton.TextEffect($"{multiplier}X Multi!", new(cols / 2 * xGap, rows / 2 * yGap));

        multiplier++;
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
	
    async void CreateBoard()
    {
		Controller.busy = true;
		await PlaceCards();
		Controller.busy = false;
    } 
	async Task PlaceCards()
	{
		List<Task> tasks = new();
        for (int x = 0; x < cols; x++)
        {
            for (int y = 0; y < rows; y++)
            {

                // Calculate position for each card
                Vector2 position = IndexToPos(x, y);

                // Instantiate the card and set parent
                GameObject card = Instantiate(GetRandomCard(), new Vector2(-2, -2), Quaternion.identity, transform);
				tasks.Add(card.transform.DOMove(position, Static.TweenDuration).AsyncWaitForCompletion());
				Sound.Singleton.Play("deal");
				await Task.Delay(100);
            }
        }
		await Task.WhenAll(tasks);
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
