using UnityEngine;

public class MainCircle : MonoBehaviour
{
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private PlayerBallController playerBallController;
	[SerializeField] private SpriteRenderer layer;
	[SerializeField] private RectTransform allPanel;

	private void Start()
	{
		var screenSize = ScreenSize.GetSize();
		spriteRenderer.size = new Vector2(2 * screenSize.x, 2 * screenSize.x);
		layer.transform.localScale = layer.transform.localScale * 2 * screenSize.x;
		transform.position = new Vector2(0, -screenSize.y + screenSize.x);
		playerBallController.Initialize(transform.position, new Vector2(0, transform.position.y + 0.3606271777003484f * spriteRenderer.size.y));

		allPanel.anchorMin = new Vector2(allPanel.anchorMin.x, spriteRenderer.size.x / (2 * screenSize.y));
	}
}

public static class ScreenSize
{
	public static Vector2 GetSize()
	{
		return ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
	}

	public static Vector3 ScreenToWorldPoint(Vector2 screenPosition)
	{
		var objective = Camera.main.ScreenPointToRay(screenPosition);

		var direction = objective.direction;
		var origin = objective.origin;

		Vector3 normal = new Vector3(0, 0, 1);
		Vector3 point = new Vector3(0, 0, 0);

		float product = Vector3.Dot(direction, normal);

		float magnitude = Vector3.Dot(point - origin, normal) / product;

		Vector3 result = origin + magnitude * direction;
		return result;
	}
}
