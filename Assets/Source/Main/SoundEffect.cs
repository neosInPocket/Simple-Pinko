using UnityEngine;

public class SoundEffect : MonoBehaviour
{
	[SerializeField] private AudioSource audioSource;

	private void Start()
	{
		if (DataPreferences.Preferences.effects)
		{
			audioSource.enabled = true;
		}
		else
		{
			audioSource.enabled = false;
		}
	}
}
