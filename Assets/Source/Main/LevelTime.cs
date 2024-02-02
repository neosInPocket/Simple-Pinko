using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelTime : MonoBehaviour
{
	[SerializeField] private Image fill;
	[SerializeField] private TMP_Text timer;
	private float elapsed;
	private float goal;
	private bool started = false;
	private Action EndAction;

	public void StartCountDown(float time, Action endAction)
	{
		EndAction = endAction;
		elapsed = time;
		goal = time;
		started = true;
	}

	private void Update()
	{
		if (!started) return;
		if (elapsed <= 0)
		{
			started = false;
			EndAction();
			return;
		}

		fill.fillAmount = elapsed / goal;
		timer.text = ((int)elapsed).ToString();
		elapsed -= Time.deltaTime;
	}

	public void FillTimer(float time)
	{
		timer.text = ((int)(time)).ToString();
		fill.fillAmount = 1f;
	}

	public void Stop()
	{
		started = false;
	}
}
