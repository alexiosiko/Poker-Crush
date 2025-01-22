using System.Collections;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public enum Suit {
	clubs,
	hearts,
	spades,
	diamonds,
	joker,
	bomb,
}
public class Card : MonoBehaviour
{
	public short number;
	public Suit suit;
	protected bool isBreaking = false;

	public virtual async Task Break()
    {
        if (isBreaking)
            return;
        isBreaking = true;

        await BreakAsync(); // Wait for the breaking process to complete.
    }

    private async Task BreakAsync()
    {
        // Shake the rotation	
        await transform.DOShakeRotation(0.5f, 20).AsyncWaitForCompletion();

        // Optional: Add scaling or additional animations if needed
        await transform.DOScale(0, Static.TweenDuration).AsyncWaitForCompletion();

        // Destroy the object after animations
        Destroy(gameObject);
    }
}


