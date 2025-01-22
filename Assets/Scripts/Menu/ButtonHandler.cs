using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
	public void LoadMainMenu()
	{
		SceneManager.LoadScene("Main Menu");
	}
	
	public void LoadGame()
	{
		SceneManager.LoadScene("Game");
	}
}
