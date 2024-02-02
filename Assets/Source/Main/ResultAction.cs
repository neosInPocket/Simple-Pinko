using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultAction : MonoBehaviour
{
	[SerializeField] private TMP_Text rewardText;
	[SerializeField] private TMP_Text resultText;
	[SerializeField] private TMP_Text buttonText;
	[SerializeField] private Animator animator;
	private Action CloseAction;

	public void ShowResult(bool timeOver, bool win, int coinsAdded)
	{
		gameObject.SetActive(true);
		string buttonCaption = default;
		string rewardCaption = default;
		string resultCaption = default;

		if (win)
		{
			buttonCaption = "NEXT LEVEL";
			resultCaption = "YOU WIN!";
		}
		else
		{
			buttonCaption = "RETRY";

			if (timeOver)
			{
				resultCaption = "TIME IS OVER";
			}
			else
			{
				resultCaption = "MISS CLICKED!";
			}
		}

		rewardCaption = "+" + coinsAdded.ToString();

		rewardText.text = rewardCaption;
		resultText.text = resultCaption;
		buttonText.text = buttonCaption;
	}

	public void GetMenu()
	{
		animator.SetTrigger("hide");
		CloseAction = () => SceneManager.LoadScene("MainMenu");
	}

	public void GetGame()
	{
		animator.SetTrigger("hide");
		CloseAction = () => SceneManager.LoadScene("GameMain");
	}

	public void Hide()
	{
		gameObject.SetActive(false);
		CloseAction();
	}
}
