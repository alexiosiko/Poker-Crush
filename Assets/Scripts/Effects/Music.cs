using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Music : MonoBehaviour
{
	[SerializeField] AudioClip[] clips;
	void Start() => Play();
	void Play()
	{
		source.clip = clips[Random.Range(0, clips.Length)];
		source.Play();
		Invoke(nameof(Play), source.clip.length);
	}
	AudioSource source;
	void Awake()
	{
		DontDestroyOnLoad(gameObject);
		source = GetComponent<AudioSource>();
	}
}
