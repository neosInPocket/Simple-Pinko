using System.Collections;
using UnityEngine;

public class FloatingTarget : MonoBehaviour
{
	[SerializeField] private GameObject hitEffectPrefab;

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
}
