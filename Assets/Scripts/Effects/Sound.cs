using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class Sound : MonoBehaviour
{
	[SerializeField] AudioClip[] instanceClips; // Shown in the Inspector
    static AudioClip[] clips; // Static field for global access
	static AudioSource source;
	public static void Play(string name)
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
 	static AudioClip GetClip(string name)
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
        clips = instanceClips; // Assign the non-static field to the static field

	}
	public static Sound Singleton;
}
