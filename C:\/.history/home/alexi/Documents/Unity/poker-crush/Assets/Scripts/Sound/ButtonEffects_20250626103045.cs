using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ButtonEffects : MonoBehaviour
{
	[SerializeField] AudioClip highlightedClip;
	
	AudioSource source;
	void Awake() => source = GetComponent<AudioSource>();
	public void PlayButtonHighlight()
	{

	}
}
