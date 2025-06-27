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
		source.Play();
	}
	public void PlayPressedClip()
	{
		print("Highligt");
		source.clip = pressedClip;
		source.Play();
	}
	void Start() => PlayButtonHighlight(); // Debug check

}
