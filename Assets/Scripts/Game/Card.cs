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
 	[SerializeField] Sprite face;
 	[SerializeField] Sprite back;
	public async Task Flip(int delayMilliseconds)
	{
		await Task.Delay(delayMilliseconds);
		if (renderer.sprite == back)
		{
			await transform.DOScaleX(0, 0.1f).AsyncWaitForCompletion();
			renderer.sprite = face;
			await transform.DOScaleX(1, 0.1f).AsyncWaitForCompletion();
		}
	}
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
	new SpriteRenderer renderer;
	void Start()
	{
		// _ = Flip(500);
	}
	void Awake()
	{
		renderer = GetComponent<SpriteRenderer>();
		// renderer.sprite = back;
	}
}


