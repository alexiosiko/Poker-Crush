using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
	public void LoadScene(string sceneName)
	{
		SceneManager.load(sceneName, LoadSceneMode.Single);
	}
}
