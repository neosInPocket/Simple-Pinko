using System;
using UnityEngine;

[Serializable]
public class Preferences
{
	public int coins = 10;
	public bool tutorial = true;

	public int level = 1;
	public bool music = true;
	public bool effects = true;
	public int timeUpgrades = 0;
	public int energyUpgrades = 1;
}
