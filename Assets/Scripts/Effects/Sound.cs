using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class Sound : MonoBehaviour
{
	[SerializeField] AudioClip[] clips;
	AudioSource source;
	public void Play(string name)
	{
		AudioClip c = GetClip(name);
		if (!c)
			return;
		
		source.Stop();
		source.clip = c;
		source.Play();
	}
	public void Play(string[] names)
	{
		AudioClip c = GetClip(names[Random.Range(0, names.Length)]);
		if (!c)
			return;
		
		source.Stop();
		source.clip = c;
		source.Play();
	}
 	AudioClip GetClip(string name)
	{
		foreach (AudioClip a in clips)
			if (a.name == name)
				return a;
		Debug.LogError($"Could not find audio name: {name}");
		return null;
	}
	void Awake()
	{
		source = GetComponent<AudioSource>();
		Singleton = this;
	}
	public static Sound Singleton;
}
