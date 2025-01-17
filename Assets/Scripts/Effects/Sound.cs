using UnityEngine;
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioSource))]
public class Sound : MonoBehaviour
{
    [SerializeField] AudioClip[] clips; // Static field for global access
	[SerializeField] AudioSource source1;
	[SerializeField] AudioSource source2;
	public void Play(string name, bool secondSource = false)
	{
		AudioClip c = GetClip(name);
		if (!c)
			return;


		AudioSource s;
		if (secondSource)
			s = source2;
		else
			s = source1;
		s.Stop();
		s.clip = c;
		s.Play();
	}
	public void Play(string[] names, bool secondSource = false)
	{
		AudioClip c = GetClip(names[Random.Range(0, names.Length)]);
		if (!c)
			return;
		
		AudioSource s;
		if (secondSource)
			s = source2;
		else
			s = source1;
		s.Stop();
		s.clip = c;
		s.Play();
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
		source1 = GetComponent<AudioSource>();
		Singleton = this;

	}
	public static Sound Singleton;
}
