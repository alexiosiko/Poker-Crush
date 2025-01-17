using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
	public void OnHome()
	{
		SceneManager.LoadScene("Main Menu");
	}
}
