using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
	[SerializeField] GameObject homeObject;
	public void OnToggle()
	{
		if (homeObject.activeInHierarchy)
			Close();
		else
			Open();

	}
	void Open()
	{
		Controller.menuBusy = true;
		homeObject.SetActive(true);
	}
	void Close()
	{
		Controller.menuBusy = false;
		homeObject.SetActive(false);
	}
	public void OnMusicToggle()
	{
		if (GameObject.Find("Music Toggle").GetComponent<Toggle>().isOn)
			GameObject.Find("Music").GetComponent<AudioSource>().volume = 0.2f;
		else
			GameObject.Find("Music").GetComponent<AudioSource>().volume = 0;

	}
}
