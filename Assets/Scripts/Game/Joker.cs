using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Joker : WildCard
{
    public override async Task Break()
    {
		if (isBreaking)
			return;
		isBreaking = true;
        HashSet<Task> tasks = new();

        // Get horizontal cards
        for (int x = 0; x < Board.cols; x++)
        {
            Transform card = Board.GetCardAt(new(x * Board.xGap, transform.position.y));
            if (card)
                tasks.Add(card.GetComponent<Card>().Break());
        }

        // Get vertical cards
        for (int y = 0; y < Board.rows; y++)
        {
            Transform card = Board.GetCardAt(new(transform.position.x, y * Board.yGap));
            if (card)
                tasks.Add(card.GetComponent<Card>().Break());
        }
		tasks.Add(base.Break());



        Effects.Singleton.TextEffect("Joker!", transform.position);
		Sound.Singleton.Play("joker");
		Score.Singleton.RemoveScore(20);
		
		await Task.WhenAll(tasks);
    }
}
