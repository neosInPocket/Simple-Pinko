using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeInfoWindow : MonoBehaviour
{
	[SerializeField] private TMP_Text description;
	[SerializeField] private GameObject timeContainer;
	[SerializeField] private GameObject energyContainer;
	[SerializeField] private TMP_Text cost;
	[SerializeField] private int timeCost;
	[SerializeField] private int energyCost;
	[SerializeField] private string timeDescription;
	[SerializeField] private string energyDescription;
	[SerializeField] private List<Image> timePoints;
	[SerializeField] private List<Image> energyPoints;
	[SerializeField] private Button button;
	[SerializeField] private TMP_Text buyStatus;
	[SerializeField] private MenuUpdater menuUpdater;
	private bool isTime;

	public void OpenTime()
	{
		gameObject.SetActive(true);
		isTime = true;
		timeContainer.SetActive(true);
		energyContainer.SetActive(false);

		cost.text = timeCost.ToString();
		description.text = timeDescription;
		RefreshPoints(timePoints, DataPreferences.Preferences.timeUpgrades);
		RefreshButton(timeCost, DataPreferences.Preferences.timeUpgrades);
	}

	public void OpenEnergy()
	{
		gameObject.SetActive(true);
		isTime = false;
		timeContainer.SetActive(false);
		energyContainer.SetActive(true);

		cost.text = energyCost.ToString();
		description.text = energyDescription;
		RefreshPoints(energyPoints, DataPreferences.Preferences.energyUpgrades);
		RefreshButton(energyCost, DataPreferences.Preferences.energyUpgrades);

	}

	public void Purchase()
	{
		if (isTime)
		{
			DataPreferences.Preferences.coins -= timeCost;
			DataPreferences.Preferences.timeUpgrades++;
			DataPreferences.SavePreferences();
			RefreshPoints(timePoints, DataPreferences.Preferences.timeUpgrades);
			RefreshButton(timeCost, DataPreferences.Preferences.timeUpgrades);
		}
		else
		{
			DataPreferences.Preferences.coins -= energyCost;
			DataPreferences.Preferences.energyUpgrades++;
			DataPreferences.SavePreferences();
			RefreshPoints(energyPoints, DataPreferences.Preferences.energyUpgrades);
			RefreshButton(energyCost, DataPreferences.Preferences.energyUpgrades);
		}

		menuUpdater.UpdateMenu();
	}

	private void RefreshButton(int cost, int upgrade)
	{
		bool buttonEnabled = DataPreferences.Preferences.coins >= cost && upgrade < 3;
		bool upgradedToMax = upgrade >= 3;

		if (buttonEnabled)
		{
			buyStatus.gameObject.SetActive(false);
		}
		else
		{
			buyStatus.gameObject.SetActive(true);
		}
		button.interactable = buttonEnabled;

		if (upgradedToMax)
		{
			buyStatus.gameObject.SetActive(false);
		}
	}

	private void RefreshPoints(List<Image> points, int value)
	{
		points.ForEach(x => x.enabled = false);

		for (int i = 0; i < value; i++)
		{
			points[i].enabled = true;
		}
	}
}
