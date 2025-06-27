using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
	public void LoadScene(string sceneName)
	{
		aud
		SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
	}
}
