using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
	public void LoadScene(string sceneName)
	{
		SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
	}
	IEnumerator LoadScene(string sceneName)
	{
		yield return new WaitForSeconds(0.5f);
		SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

	}
}
