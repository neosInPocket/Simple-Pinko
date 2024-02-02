using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class PlayerBallController : MonoBehaviour
{
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private Transform ball;
	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private float startVelocity;
	[SerializeField] private GameObject missEffectPrefab;
	[SerializeField] private AudioSource audioSource;
	private float currentVelocity;
	private Action<int> onDamage;
	private Action onTargetHit;
	private int energyLeft;

	public bool Enabled
	{
		get => isEnabled;
		set
		{
			isEnabled = value;
			if (value)
			{
				rb.angularVelocity = startVelocity;
				currentVelocity = startVelocity;
				Touch.onFingerDown += OnFingerDown;
			}
			else
			{
				rb.angularVelocity = 0;
				currentVelocity = 0;
				Touch.onFingerDown -= OnFingerDown;
			}
		}
	}

	private bool isEnabled;

	private void Awake()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
	}

	private void Start()
	{
		energyLeft = DataPreferences.Preferences.energyUpgrades;
	}

	public void Subscribe(Action<int> damageAction, Action targetHitAction)
	{
		onDamage = damageAction;
		onTargetHit = targetHitAction;
	}

	private void OnFingerDown(Finger finger)
	{
		RaycastHit2D raycast = Physics2D.Raycast(ball.transform.position, Vector3.forward);
		if (raycast.collider != null)
		{
			if (raycast.collider.TryGetComponent<RotatingLayer>(out RotatingLayer target))
			{
				audioSource.Stop();
				audioSource.Play();
				target.ShowEffect(ball.transform.position);
				onTargetHit();
			}
		}
		else
		{
			energyLeft--;

			if (energyLeft <= 0)
			{
				energyLeft = 0;
				Lose();
			}
			else
			{
				StopAllCoroutines();
				missEffectPrefab.gameObject.SetActive(false);
				StartCoroutine(TakeDamage(ball.transform.position));
			}

			onDamage(energyLeft);
		}

		rb.angularVelocity *= -1;
	}

	private IEnumerator TakeDamage(Vector2 position)
	{
		missEffectPrefab.SetActive(true);
		yield return new WaitForSeconds(1f);
		missEffectPrefab.SetActive(false);
	}

	private void Lose()
	{
		spriteRenderer.enabled = false;
		StartCoroutine(TakeDamage(ball.transform.position));
		Enabled = false;
	}

	public void Initialize(Vector2 position, Vector2 ballPos)
	{
		transform.position = position;
		ball.transform.position = ballPos;
	}

	private void OnDestroy()
	{
		Touch.onFingerDown -= OnFingerDown;
	}
}
