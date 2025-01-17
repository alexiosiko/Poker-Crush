using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseMenu : MonoBehaviour
{
	public void OnHome()
	{
		SceneManager.LoadScene("Main Menu");
	}
}
