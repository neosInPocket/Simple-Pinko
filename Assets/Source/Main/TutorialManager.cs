using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
	[SerializeField] private TMP_Text characterText;
	private Action End;
	private void Start()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
	}

	public void Entry(Action endAction)
	{
		End = endAction;
		gameObject.SetActive(true);
		Touch.onFingerDown += Play;
		characterText.text = "WELCOME TO SIMPLE PINKO!";
	}

	private void Play(Finger finger)
	{
		Touch.onFingerDown -= Play;
		Touch.onFingerDown += Quote1;
		characterText.text = "TAP THE SCREEN WHEN YOUR BALL MATCHES THE DARK AREA OF THE RING!";
	}

	private void Quote1(Finger finger)
	{
		Touch.onFingerDown -= Quote1;
		Touch.onFingerDown += Quote2;
		characterText.text = "BOTH THE DARK ZONE AND THE BALL ITSELF CAN CHANGE THE DIRECTIONS OF ROTATION CHAOTICALLY, SO BE CAREFUL!";
	}

	private void Quote2(Finger finger)
	{
		Touch.onFingerDown -= Quote2;
		Touch.onFingerDown += Replica3;
		characterText.text = "GET THE REQUIRED NUMBER OF POINTS BEFORE THE TIME RUNS OUT!";
	}

	private void Replica3(Finger finger)
	{
		Touch.onFingerDown -= Replica3;
		Touch.onFingerDown += Rep4;
		characterText.text = "DO YOU REACTION ENOUGH TO PASS THE LEVEL? GOOD LUCK!";
	}

	private void Rep4(Finger finger)
	{
		Touch.onFingerDown -= Rep4;
		End();

		if (gameObject != null)
		{
			gameObject.SetActive(false);
		}
	}
}
