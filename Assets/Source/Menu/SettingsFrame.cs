using UnityEngine;
using UnityEngine.UI;

public class SettingsFrame : MonoBehaviour
{
	[SerializeField] private Image music;
	[SerializeField] private Image sfx;
	private AudioManager audioManager;

	private void Start()
	{
		audioManager = GameObject.FindFirstObjectByType<AudioManager>();
		bool musicEnabled = DataPreferences.Preferences.music;
		bool sfxEnabled = DataPreferences.Preferences.effects;

		music.enabled = musicEnabled;
	}

	public void ToggleMusic()
	{
		bool enabled = audioManager.Toggle();

		music.enabled = enabled;
	}

	public void ToggleSFX()
	{
		sfx.enabled = !sfx.enabled;
		DataPreferences.Preferences.effects = sfx.enabled;

		DataPreferences.SavePreferences();
	}
}
