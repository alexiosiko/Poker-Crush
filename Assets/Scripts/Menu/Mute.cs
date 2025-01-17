using UnityEngine;

public class Mute : MonoBehaviour
{
	[SerializeField] GameObject muteCrossBar;
	public void OnToggle()
	{
		if (muteCrossBar.activeInHierarchy)
			UnMute();
		else
			MuteFunc();
	}
	void UnMute()
	{
		muteCrossBar.SetActive(false);
		GameObject.Find("Music").GetComponent<AudioSource>().volume = musicLevel;
	}
	void MuteFunc()
	{
		muteCrossBar.SetActive(true);
		if (musicLevel == -1f)
			musicLevel = GameObject.Find("Music").GetComponent<AudioSource>().volume;
		GameObject.Find("Music").GetComponent<AudioSource>().volume = 0;
	}
	[SerializeField] float musicLevel = -1f;

}
