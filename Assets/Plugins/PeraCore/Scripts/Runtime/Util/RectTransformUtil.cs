using UnityEngine;

public static class RectTransformUtil {
	public static void AddAnchorPosition(this RectTransform rect, Vector2 amount) {
		rect.anchoredPosition += amount;
	}

	public static void AddAnchorPositionX(this RectTransform rect, float x) {
		rect.SetAnchorPositionX(rect.anchoredPosition.x + x);
	}

	public static void SetAnchorPositionX(this RectTransform rect, float x) {
		rect.anchoredPosition = new Vector2(x, rect.anchoredPosition.y);
	}

	public static void SetAnchorPositionY(this RectTransform rect, float y) {
		rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, y);
	}
}
