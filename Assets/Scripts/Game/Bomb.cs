using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Bomb : WildCard
{
	public override async Task Break()
	{
		if (isBreaking)
			return;
		isBreaking = true;


		List<Task> tasks = new();
		for (int x = -1; x <= 1; x++)
		{
			for (int y = -1; y <= 1; y++)
			{
				if (x == 0 && y == 0)
					continue;
				
				Transform card = Board.GetCardAt(new(transform.position.x + x * Board.xGap, transform.position.y + y * Board.yGap));
				if (card)
					tasks.Add(card.GetComponent<Card>().Break());
			}
		}
		Sound.Singleton.Play("explosion", true);
        Effects.Singleton.TextEffect("Bomb!", transform.position);

		tasks.Add(base.Break());
		await Task.WhenAll(tasks);
			
		Game.Singleton.RemoveScore(20);

	}
}
