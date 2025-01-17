using System.Collections.Generic;
using UnityEngine;

public class Bomb : WildCard
{
	public override void Break()
	{
		if (isBreaking)
			return;
		isBreaking = true;

		List<Transform> cards = new();
		for (int x = -1; x <= 1; x++)
		{
			for (int y = -1; y <= 1; y++)
			{
				if (x == 0 && y == 0)
					continue;
				
				Transform card = Board.GetCardAt(new(transform.position.x + x * Board.xGap, transform.position.y + y * Board.yGap));
				if (card)
					cards.Add(card);
			}
		}
		foreach (Transform c in cards)
		{
			c.GetComponent<Card>().Break();
		}
		Sound.Singleton.Play("explosion", true);
		base.Break();
	}
}
