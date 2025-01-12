using System;
using System.Collections.Generic;
using UnityEngine;

public static class Logic
{
	public static List<(Func<Transform, Vector2, HashSet<Transform>, bool> function, int score, string name, float centerOffset)> logicFunctions = new()
		{
			(GetRoyalFlush, 250, "Royal Flush", 2.5f * Board.xGap),
			(GetStraightFlush, 200, "Straight Flush", 2.5f * Board.xGap),
			(GetFourOfAKind, 150, "Four of a Kind", 2.5f * Board.xGap),
			(GetFullHouse, 120, "Full House", 2.5f * Board.xGap),
			(GetFlush, 100, "Flush", 2.5f * Board.xGap),
			(GetStraight, 80, "Straight", 2.5f * Board.xGap),
			(GetThreeOfAKind, 50, "Three of a Kind", 1.5f * Board.xGap),
			(GetTwoPair, 30, "Two Pair", 2.5f * Board.xGap),
			(GetOnePair, 10, "Pair", 0.5f * Board.xGap),
		};	
	public static bool GetOnePair(Transform card, Vector2 direction, HashSet<Transform> cardsToClear)
    {
		Transform second = Board.GetCardAt((Vector2)card.position + new Vector2(direction.x * Board.xGap * 1, direction.y * Board.yGap * 1));
		if (!second)
			return false;
		if (card.GetComponent<Card>().number != second.GetComponent<Card>().number)
			return false;

		cardsToClear.Add(card);
		cardsToClear.Add(second);
		Debug.Log($"Pair: {card.GetComponent<Card>().number} {second.GetComponent<Card>().number}");
		return true;
    }
	public static bool GetTwoPair(Transform card, Vector2 direction, HashSet<Transform> cardsToClear)
	{
		Transform second = Board.GetCardAt((Vector2)card.position + new Vector2(direction.x * Board.xGap * 1, direction.y * Board.yGap * 1));
		if (!second)
			return false;

		Transform third = Board.GetCardAt((Vector2)card.position + new Vector2(direction.x * Board.xGap * 2, direction.y * Board.yGap * 2));
		if (!third)
			return false;

		Transform fourth = Board.GetCardAt((Vector2)card.position + new Vector2(direction.x * Board.xGap * 3, direction.y * Board.yGap * 3));
		if (!fourth)
			return false;

		// Check for two distinct pairs
		int number1 = card.GetComponent<Card>().number;
		int number2 = second.GetComponent<Card>().number;
		int number3 = third.GetComponent<Card>().number;
		int number4 = fourth.GetComponent<Card>().number;

		if (number1 == number2 && number3 == number4 && number1 != number3)
		{
			cardsToClear.Add(card);
			cardsToClear.Add(second);
			cardsToClear.Add(third);
			cardsToClear.Add(fourth);

			Debug.Log($"Two Pair: {number1} and {number3}");
			return true;
		}
		return false;
	}

	
	public static bool GetThreeOfAKind(Transform card, Vector2 direction, HashSet<Transform> cardsToClear)
    {
		Transform second = Board.GetCardAt((Vector2)card.position + new Vector2(direction.x * Board.xGap * 1, direction.y * Board.yGap * 1));
		if (!second)
			return false;
		Transform third = Board.GetCardAt((Vector2)card.position + new Vector2(direction.x * Board.xGap * 2, direction.y * Board.yGap * 2));
		if (!third)
			return false;

		if (card.GetComponent<Card>().number != second.GetComponent<Card>().number)
			return false;
		if (card.GetComponent<Card>().number != third.GetComponent<Card>().number)
			return false;

		cardsToClear.Add(card);
		cardsToClear.Add(second);
		cardsToClear.Add(third);
		Debug.Log($"Three of a kind: {card.GetComponent<Card>().number} {second.GetComponent<Card>().number} {third.GetComponent<Card>().number}");
		return false;
	}
	public static bool GetStraight(Transform card, Vector2 direction, HashSet<Transform> cardsToClear)
	{
		Transform second = Board.GetCardAt((Vector2)card.position + new Vector2(direction.x * Board.xGap * 1, direction.y * Board.yGap * 1));
		if (!second)
			return false;
		Transform third = Board.GetCardAt((Vector2)card.position + new Vector2(direction.x * Board.xGap * 2, direction.y * Board.yGap * 2));
		if (!third)
			return false;
		Transform fourth = Board.GetCardAt((Vector2)card.position + new Vector2(direction.x * Board.xGap * 3, direction.y * Board.yGap * 3));
		if (!fourth)
			return false;
		Transform fifth = Board.GetCardAt((Vector2)card.position + new Vector2(direction.x * Board.xGap * 4, direction.y * Board.yGap * 4));
		if (!fifth)
			return false;

		int[] numbers = new int[5];

		numbers[0] = card.	GetComponent<Card>().number;
		numbers[1] = second.GetComponent<Card>().number;
		numbers[2] = third.	GetComponent<Card>().number;
		numbers[3] = fourth.GetComponent<Card>().number;
		numbers[4] = fifth.	GetComponent<Card>().number;

		Array.Sort(numbers);
	
		 // Check for consecutive differences of 1
		for (int i = 1; i < numbers.Length; i++)
			if (numbers[i] - numbers[i - 1] != 1)
				return false;

		cardsToClear.Add(card);
		cardsToClear.Add(second);
		cardsToClear.Add(third);
		cardsToClear.Add(fourth);
		cardsToClear.Add(fifth);

		Debug.Log($"Straight: {numbers[0]} {numbers[1]} {numbers[2]} {numbers[3]} {numbers[4]}");
		return true;
	}
	public static bool GetFlush(Transform card, Vector2 direction, HashSet<Transform> cardsToClear)
	{
		Transform second = Board.GetCardAt((Vector2)card.position + new Vector2(direction.x * Board.xGap * 1, direction.y * Board.yGap * 1));
		if (!second)
			return false;
		Transform third = Board.GetCardAt((Vector2)card.position + new Vector2(direction.x * Board.xGap * 2, direction.y * Board.yGap * 2));
		if (!third)
			return false;
		Transform fourth = Board.GetCardAt((Vector2)card.position + new Vector2(direction.x * Board.xGap * 3, direction.y * Board.yGap * 3));
		if (!fourth)
			return false;
		Transform fifth = Board.GetCardAt((Vector2)card.position + new Vector2(direction.x * Board.xGap * 4, direction.y * Board.yGap * 4));
		if (!fifth)
			return false;

		Suit suit = card.GetComponent<Card>().suit;
		if (second.GetComponent<Card>().suit != suit)
			return false;
		if (third.GetComponent<Card>().suit != suit)
			return false;
		if (fourth.GetComponent<Card>().suit != suit)
			return false;
		if (fifth.GetComponent<Card>().suit != suit)
			return false;

		cardsToClear.Add(card);
		cardsToClear.Add(second);
		cardsToClear.Add(third);
		cardsToClear.Add(fourth);
		cardsToClear.Add(fifth);

		Debug.Log($"Flush: {card.GetComponent<Card>().number} {second.GetComponent<Card>().number} {third.GetComponent<Card>().number} {fourth.GetComponent<Card>().number} {fifth.GetComponent<Card>().number}");
		return true;
	}
	public static bool GetFourOfAKind(Transform card, Vector2 direction, HashSet<Transform> cardsToClear)
	{
		Transform second = Board.GetCardAt((Vector2)card.position + new Vector2(direction.x * Board.xGap * 1, direction.y * Board.yGap * 1));
		if (!second)
			return false;
		Transform third = Board.GetCardAt((Vector2)card.position + new Vector2(direction.x * Board.xGap * 2, direction.y * Board.yGap * 2));
		if (!third)
			return false;
		Transform fourth = Board.GetCardAt((Vector2)card.position + new Vector2(direction.x * Board.xGap * 3, direction.y * Board.yGap * 3));
		if (!fourth)
			return false;

		int number = card.GetComponent<Card>().number;
		if (second.GetComponent<Card>().number != number || third.GetComponent<Card>().number != number || fourth.GetComponent<Card>().number != number)
			return false;

		cardsToClear.Add(card);
		cardsToClear.Add(second);
		cardsToClear.Add(third);
		cardsToClear.Add(fourth);

		Debug.Log($"Four of a Kind: {number}");
		return true;
	}

	public static bool GetFullHouse(Transform card, Vector2 direction, HashSet<Transform> cardsToClear)
	{
		// Get potential three of a kind
		Transform second = Board.GetCardAt((Vector2)card.position + new Vector2(direction.x * Board.xGap * 1, direction.y * Board.yGap * 1));
		if (!second) return false;

		Transform third = Board.GetCardAt((Vector2)card.position + new Vector2(direction.x * Board.xGap * 2, direction.y * Board.yGap * 2));
		if (!third) return false;

		if (card.GetComponent<Card>().number != second.GetComponent<Card>().number ||
			card.GetComponent<Card>().number != third.GetComponent<Card>().number)
			return false;

		// If we found a three of a kind, check for a pair in the remaining direction
		Transform fourth = Board.GetCardAt((Vector2)card.position + new Vector2(direction.x * Board.xGap * 3, direction.y * Board.yGap * 3));
		if (!fourth) return false;

		Transform fifth = Board.GetCardAt((Vector2)card.position + new Vector2(direction.x * Board.xGap * 4, direction.y * Board.yGap * 4));
		if (!fifth) return false;

		if (fourth.GetComponent<Card>().number == fifth.GetComponent<Card>().number)
		{
			// Full house found
			cardsToClear.Add(card);
			cardsToClear.Add(second);
			cardsToClear.Add(third);
			cardsToClear.Add(fourth);
			cardsToClear.Add(fifth);

			Debug.Log($"Full House: {card.GetComponent<Card>().number}");
			return true;
		}

		return false;
	}
	public static bool GetStraightFlush(Transform card, Vector2 direction, HashSet<Transform> cardsToClear)
	{
		Transform second = Board.GetCardAt((Vector2)card.position + new Vector2(direction.x * Board.xGap * 1, direction.y * Board.yGap * 1));
		if (!second)
			return false;
		Transform third = Board.GetCardAt((Vector2)card.position + new Vector2(direction.x * Board.xGap * 2, direction.y * Board.yGap * 2));
		if (!third)
			return false;
		Transform fourth = Board.GetCardAt((Vector2)card.position + new Vector2(direction.x * Board.xGap * 3, direction.y * Board.yGap * 3));
		if (!fourth)
			return false;
		Transform fifth = Board.GetCardAt((Vector2)card.position + new Vector2(direction.x * Board.xGap * 4, direction.y * Board.yGap * 4));
		if (!fifth)
			return false;

		Suit suit = card.GetComponent<Card>().suit;
		if (second.GetComponent<Card>().suit != suit || third.GetComponent<Card>().suit != suit || fourth.GetComponent<Card>().suit != suit || fifth.GetComponent<Card>().suit != suit)
			return false;

		int[] numbers = new int[5];
		numbers[0] = card.GetComponent<Card>().number;
		numbers[1] = second.GetComponent<Card>().number;
		numbers[2] = third.GetComponent<Card>().number;
		numbers[3] = fourth.GetComponent<Card>().number;
		numbers[4] = fifth.GetComponent<Card>().number;

		Array.Sort(numbers);
		for (int i = 1; i < numbers.Length; i++)
			if (numbers[i] - numbers[i - 1] != 1)
				return false;

		cardsToClear.Add(card);
		cardsToClear.Add(second);
		cardsToClear.Add(third);
		cardsToClear.Add(fourth);
		cardsToClear.Add(fifth);

		Debug.Log($"Straight Flush: {numbers[0]} {numbers[1]} {numbers[2]} {numbers[3]} {numbers[4]}");
		return true;
	}
	public static bool GetRoyalFlush(Transform card, Vector2 direction, HashSet<Transform> cardsToClear)
	{
		Transform second = Board.GetCardAt((Vector2)card.position + new Vector2(direction.x * Board.xGap * 1, direction.y * Board.yGap * 1));
		if (!second)
			return false;
		Transform third = Board.GetCardAt((Vector2)card.position + new Vector2(direction.x * Board.xGap * 2, direction.y * Board.yGap * 2));
		if (!third)
			return false;
		Transform fourth = Board.GetCardAt((Vector2)card.position + new Vector2(direction.x * Board.xGap * 3, direction.y * Board.yGap * 3));
		if (!fourth)
			return false;
		Transform fifth = Board.GetCardAt((Vector2)card.position + new Vector2(direction.x * Board.xGap * 4, direction.y * Board.yGap * 4));
		if (!fifth)
			return false;

		Suit suit = card.GetComponent<Card>().suit;
		if (second.GetComponent<Card>().suit != suit || third.GetComponent<Card>().suit != suit || fourth.GetComponent<Card>().suit != suit || fifth.GetComponent<Card>().suit != suit)
			return false;

		int[] numbers = new int[5];
		numbers[0] = card.GetComponent<Card>().number;
		numbers[1] = second.GetComponent<Card>().number;
		numbers[2] = third.GetComponent<Card>().number;
		numbers[3] = fourth.GetComponent<Card>().number;
		numbers[4] = fifth.GetComponent<Card>().number;

		Array.Sort(numbers);
		if (numbers[0] == 10 && numbers[1] == 11 && numbers[2] == 12 && numbers[3] == 13 && numbers[4] == 14)
		{
			cardsToClear.Add(card);
			cardsToClear.Add(second);
			cardsToClear.Add(third);
			cardsToClear.Add(fourth);
			cardsToClear.Add(fifth);

			Debug.Log($"Royal Flush: {numbers[0]} {numbers[1]} {numbers[2]} {numbers[3]} {numbers[4]}");
			return true;
		}
		return false;
	}
}
