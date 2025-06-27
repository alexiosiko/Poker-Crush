using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
	public void LoadScene(string sceneName, int number)
	{
		SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
	}
}
