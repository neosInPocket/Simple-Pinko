using System;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class CountinueFrame : MonoBehaviour
{
	[SerializeField] private GameObject mainObject;
	[SerializeField] private Image image;
	[SerializeField] private float alpha;
	private Action OnTap;

	private void Start()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
	}

	public void StartShow(Action onTap)
	{
		OnTap = onTap;

		var clr = image.color;
		clr.a = alpha;
		image.raycastTarget = true;

		if (mainObject != null)
		{
			mainObject.SetActive(true);
		}

		Touch.onFingerDown += OnTapHandler;
	}

	private void OnTapHandler(Finger finger)
	{
		Touch.onFingerDown -= OnTapHandler;
		OnTap();

		var clr = image.color;
		clr.a = 0;
		image.raycastTarget = false;

		mainObject.SetActive(false);
	}

	private void OnDestroy()
	{
		Touch.onFingerDown -= OnTapHandler;
	}
}

