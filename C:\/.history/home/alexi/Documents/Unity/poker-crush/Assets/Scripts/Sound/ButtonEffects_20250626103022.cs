using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ButtonEffects : MonoBehaviour
{
	AudioSource source;
	void Awale() => source = GetComponent<AudioSource>();
	public void PlayButtonHighlight()
	{

	}
}
