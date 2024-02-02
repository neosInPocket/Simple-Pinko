using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class RotatingLayer : MonoBehaviour
{
	[SerializeField] private Vector2 rotationSpeeds;
	[SerializeField] private Vector2 changeDirectionDelay;
	[SerializeField] private Rigidbody2D rigid;
	[SerializeField] private GameObject hitEffectPrefab;

	public bool Enabled
	{
		get => isEnabled;
		set
		{
			isEnabled = value;
			if (!value)
			{
				if (gameObject != null)
				{
					StopAllCoroutines();
				}
			}
			else
			{
				var rotationMultiplier = Random.Range(0, 2) == 2 ? 1 : -1;
				rigid.angularVelocity = rotationMultiplier * Random.Range(rotationSpeeds.x, rotationSpeeds.y);
			}

			changingDirection = false;
		}
	}
	private bool changingDirection;
	private bool isEnabled;

	private void Start()
	{

	}

	private void Update()
	{
		if (!isEnabled) return;
		if (changingDirection) return;

		changingDirection = true;
		StartCoroutine(ChangeRotationDirection());
	}

	public void ShowEffect(Vector2 position)
	{
		StartCoroutine(Effect(position));
	}

	private IEnumerator Effect(Vector2 position)
	{
		var effect = Instantiate(hitEffectPrefab, position, Quaternion.identity, null);
		yield return new WaitForSeconds(1f);
		Destroy(effect.gameObject);
	}

	private IEnumerator ChangeRotationDirection()
	{
		yield return new WaitForSeconds(Random.Range(changeDirectionDelay.x, changeDirectionDelay.y));

		var rotationMultiplier = Random.Range(0, 2) == 1 ? 1 : -1;
		rigid.angularVelocity = rotationMultiplier * Random.Range(rotationSpeeds.x, rotationSpeeds.y);
		changingDirection = false;
	}
}
