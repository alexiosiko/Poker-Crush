using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Music : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip[] clips;
    void Start() => Play();
    void Play()
    {
        source.clip = clips[Random.Range(0, clips.Length)];
        source.Play();
        Invoke(nameof(Play), source.clip.length);
    }
    static Music Singleton;
    void Awake()
    {
        if (Singleton != null && Singleton != this)
        {
            Destroy(gameObject);
            return;
        }

        Singleton = this; // Assign the current instance
        DontDestroyOnLoad(gameObject); // Make sure it persists between scenes
    }
}
