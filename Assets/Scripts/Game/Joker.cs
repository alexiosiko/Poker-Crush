using System.Collections.Generic;
using UnityEngine;

public class Joker : Card
{
    private static HashSet<Transform> cardsBeingProcessed = new();

    public override void Break()
    {
        // Prevent processing the same card multiple times
        if (cardsBeingProcessed.Contains(transform))
            return;

        cardsBeingProcessed.Add(transform);

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
		print("here");
        Board.Singleton.StartClearCreateFallLoop();
		print("here done");

        // Remove this card from the processing set after its effects are complete
        cardsBeingProcessed.Remove(transform);

        base.Break();
    }
}
