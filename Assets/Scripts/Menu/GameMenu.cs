using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
	public void OnHome()
	{
		SceneManager.LoadScene("Main Menu");
	}
}
