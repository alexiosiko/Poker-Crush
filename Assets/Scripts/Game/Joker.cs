using System.Collections.Generic;
using UnityEngine;

public class Joker : WildCard
{
    public override void Break()
    {
		if (isBreaking)
			return;
		isBreaking = true;

        HashSet<Transform> cardsToBreak = new();

        // Get horizontal cards
        for (int x = 0; x < Board.cols; x++)
        {
            Transform card = Board.GetCardAt(new(x * Board.xGap, transform.position.y));
            if (card)
                cardsToBreak.Add(card);
        }

        // Get vertical cards
        for (int y = 0; y < Board.rows; y++)
        {
            Transform card = Board.GetCardAt(new(transform.position.x, y * Board.yGap));
            if (card)
                cardsToBreak.Add(card);
        }

        foreach (Transform card in cardsToBreak)
        {
            if (card == this.transform)
                continue;
            card.GetComponent<Card>().Break();
        }

        Effects.Singleton.TextEffect("Joker!", transform.position);
		Sound.Play("joker");
		base.Break();
    }

}
