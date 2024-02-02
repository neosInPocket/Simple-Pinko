using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
	[SerializeField] private PlayerBallController player;
	[SerializeField] private RotatingLayer rotatingLayer;
	[SerializeField] private List<Image> energy;
	[SerializeField] private Image fill;
	[SerializeField] private int addScore;
	[SerializeField] private LevelTime timer;
	[SerializeField] private TutorialManager tutorialManagment;
	[SerializeField] private CountinueFrame countinueFrame;
	[SerializeField] private ResultAction resultShow;
	[SerializeField] private TMP_Text levelHolder;
	[SerializeField] private TMP_Text fillText;
	private float time => 2 * Mathf.Log(DataPreferences.Preferences.level + 1) + 10 + DataPreferences.Preferences.timeUpgrades;
	private int goalScore => (int)(2 * Mathf.Log(DataPreferences.Preferences.level + 1) + 11);
	private int reward => (int)(3 * Mathf.Log(DataPreferences.Preferences.level + 1) + 3 + DataPreferences.Preferences.level);
	private int currentScore;

	private void Start()
	{
		currentScore = 0;
		levelHolder.text = "LEVEL " + DataPreferences.Preferences.level.ToString();

		player.Subscribe(OnPlayerTakeDamage, OnTargetHit);
		RefreshEnergyPoints(DataPreferences.Preferences.energyUpgrades);

		fill.fillAmount = (float)currentScore / (float)goalScore;
		fillText.text = $"{currentScore}/{goalScore}";
		timer.FillTimer(time);

		if (DataPreferences.Preferences.tutorial)
		{
			DataPreferences.Preferences.tutorial = false;
			DataPreferences.SavePreferences();

			tutorialManagment.Entry(TutorialEnd);
		}
		else
		{
			TutorialEnd();
		}
	}

	private void TutorialEnd()
	{
		countinueFrame.StartShow(OnTap);
	}

	private void OnTap()
	{
		player.Enabled = true;
		rotatingLayer.Enabled = true;
		timer.StartCountDown(time, OnTimerEnd);
	}

	private void OnTargetHit()
	{
		currentScore += addScore;

		if (currentScore >= goalScore)
		{
			currentScore = goalScore;
			DisableAll();

			resultShow.ShowResult(false, true, reward);
			DataPreferences.Preferences.coins += reward;
			DataPreferences.Preferences.level++;
			DataPreferences.SavePreferences();
		}

		fill.fillAmount = (float)currentScore / (float)goalScore;
		fillText.text = $"{currentScore}/{goalScore}";
	}

	private void OnTimerEnd()
	{
		DisableAll();
		resultShow.ShowResult(true, false, 0);

	}

	private void OnPlayerTakeDamage(int lifesLeft)
	{
		if (lifesLeft == 0)
		{
			resultShow.ShowResult(false, false, 0);
			DisableAll();
		}

		RefreshEnergyPoints(lifesLeft);
	}

	private void RefreshEnergyPoints(int lifes)
	{
		energy.ForEach(x => x.enabled = false);
		for (int i = 0; i < lifes; i++)
		{
			energy[i].enabled = true;
		}
	}

	private void DisableAll()
	{
		player.Enabled = false;
		rotatingLayer.Enabled = false;
		timer.Stop();
	}

	public void Menu()
	{
		SceneManager.LoadScene("MainMenu");
	}

	public void Retry()
	{
		SceneManager.LoadScene("GameMain");
	}
}
