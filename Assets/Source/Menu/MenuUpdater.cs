using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuUpdater : MonoBehaviour
{
	[SerializeField] private TMP_Text caption;

	private void Start()
	{
		UpdateMenu();
	}

	public void UpdateMenu()
	{
		caption.text = DataPreferences.Preferences.coins.ToString();
	}
}
