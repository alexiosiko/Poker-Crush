using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ButtonEffects : MonoBehaviour
{
	[SerializeField] AudioClip highlightedClip;
	[SerializeField] AudioClip pressedClip;

	AudioSource source;
	void Awake() => source = GetComponent<AudioSource>();
	public void PlayButtonHighlight()
	{
		print("Highligt");
		source.clip = highlightedClip;
		source.PlayOneShot(highlightedClip);
	}
   	public void PlayPressedClip()
	{
		print("pressed");
		source.clip = pressedClip;
		source.Play();
	}
	public void TestButton()
	{
		Debug.Log("Teest");
	}
}
