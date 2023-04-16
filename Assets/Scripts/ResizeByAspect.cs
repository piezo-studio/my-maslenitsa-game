using UnityEngine;
using static UnityEngine.Screen;

[RequireComponent(typeof(RectTransform))] [ExecuteInEditMode]
public class ResizeByAspect : MonoBehaviour
{
	private Rect _thisRect;
	private Rect _screenSafeArea;
	private void Start()
	{
		_thisRect = GetComponent<RectTransform>().rect;
		_screenSafeArea = safeArea;

		Resize();
	}

	private void Update()
	{
		if (safeArea == _screenSafeArea) return;
		_screenSafeArea = safeArea;
		Resize();
	}

	private void Resize()
	{
		// Scale to max of either width or height of safeArea
		var scaleFromRef = _screenSafeArea.width >= _screenSafeArea.height
			? _screenSafeArea.height / _thisRect.height
			: _screenSafeArea.width / _thisRect.width;

		// Scale the thing
		transform.localScale = new Vector3(scaleFromRef, scaleFromRef);

		// Plop ourselves in the middle of SafeArea
		transform.localPosition = new Vector2(width / 2f, height / 2f) - _screenSafeArea.center;

		// TODO: Scale down if we're clipping onto UI.
	}
}