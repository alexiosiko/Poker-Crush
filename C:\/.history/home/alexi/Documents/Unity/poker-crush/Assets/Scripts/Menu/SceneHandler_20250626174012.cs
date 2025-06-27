using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
	public void LoadScene(string sceneName)
	{
		AudioClip cl;
		AudioSource source;
		source.PlayScheduled
		SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
	}
}
