using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
	public void LoadScene(string sceneName)
	{
		StartCoroutine(ILoadScene(sceneName));
	}public void LoadSceneDelay(string sceneName)
	{
		StopAllCoroutines();
		StartCoroutine(ILoadScene(sceneName));
	}
	IEnumerator ILoadScene(string sceneName)
	{
		yield return new WaitForSeconds(0.5f);
		SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

	}
}
