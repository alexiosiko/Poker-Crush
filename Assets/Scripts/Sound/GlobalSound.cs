using UnityEngine;

public class GlobalSound : MonoBehaviour
{
    [SerializeField] AudioClip[] clips; // Static field for global access
    [SerializeField] AudioSource source;
	public void Play(string name, bool secondSource = false)
	{
		AudioClip c = GetClip(name);
		if (!c)
			return;


		source.Stop();
		source.clip = c;
		source.Play();
	}
	public void Play(string[] names, bool secondSource = false)
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
	public static GlobalSound Singleton;
	void Awake()
	{
		if (Singleton != null && Singleton != this)
		{
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad(gameObject);
		Singleton = this;
	}
}
