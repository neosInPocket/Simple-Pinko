using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	[SerializeField] private AudioSource audioSource;

	private void Awake()
	{
		var possible = GameObject.FindObjectsOfType<AudioManager>();
		var different = possible.FirstOrDefault(x => x.gameObject.scene.name == "DontDestroyOnLoad");

		if (different != null && different != this)
		{
			Destroy(gameObject);
			return;
		}
		else
		{
			DontDestroyOnLoad(gameObject);
		}
	}

	private void Start()
	{
		audioSource.enabled = DataPreferences.Preferences.music;
	}

	public bool Toggle()
	{
		audioSource.enabled = !audioSource.enabled;
		DataPreferences.Preferences.music = audioSource.enabled;
		DataPreferences.SavePreferences();
		return audioSource.enabled;
	}
}
