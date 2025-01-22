using Unity.VisualScripting.Antlr3.Runtime.Tree;
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
		AudioSource s = secondSource ? source2 : source1;
		if (s.enabled == false)
			return;

		s.Stop();
		s.clip = c;
		s.Play();
	}
	public void Play(string[] names, bool secondSource = false)
	{
		AudioClip c = GetClip(names[Random.Range(0, names.Length)]);
		if (!c)
			return;

   	 	AudioSource s = secondSource ? source2 : source1;
		if (s.enabled == false)
			return;

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
		Singleton = this;
	}
	public void OnToggleSound()
	{
		if (source1.enabled == true)
		{
			source1.enabled = false;
			source2.enabled = false;
		}
		else
		{
			source1.enabled = true;
			source2.enabled = true;
		}
	}
	public static Sound Singleton;
}
