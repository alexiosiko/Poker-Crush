using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
	public void LoadScene(string sceneName)
	{
		AudioClip clip
		SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
	}
}
