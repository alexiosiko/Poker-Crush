using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public void Start()
	{
		SceneManager.LoadScene("Game");
	}
}
