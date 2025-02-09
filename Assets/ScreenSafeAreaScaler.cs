using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSafeAreaScaler : MonoBehaviour
{
    RectTransform _rectTransform;
	Rect _safeArea;
	Vector2 _minAnchor;
	Vector2 _maxAnchor;
	ApplicationRunMode _runMode = ApplicationRunMode.Device;

	private void Start()
	{
		_rectTransform = GetComponent<RectTransform>();
		_runMode = Current;
	}

	public void Update()
	{
		//Don't Resize if testing in Game View
		// if (_runMode != ApplicationRunMode.Editor && _runMode != ApplicationRunMode.Desktop)
		// {
			//Get the Safe Area for the Current Device
			_safeArea = Screen.safeArea;

			//Set the Position & Size
			_minAnchor = _safeArea.position;
			_maxAnchor = _minAnchor + _safeArea.size;

			//Set to the Correct Scale
			_minAnchor.x /= Screen.currentResolution.width;
			_minAnchor.y /= Screen.currentResolution.height;
			_maxAnchor.x /= Screen.currentResolution.width;
			_maxAnchor.y /= Screen.currentResolution.height;

			//Apply Safe Area Rect
			_rectTransform.anchorMin = _minAnchor;
			_rectTransform.anchorMax = _maxAnchor;
		// }
	}

	#region Check Application Run Mode to prevent weird UI when using the Game View
	enum ApplicationRunMode
	{
		Desktop,
		Device,
		Editor,
		Simulator
	}
	static ApplicationRunMode Current
	{
		get
		{
#if UNITY_EDITOR
			return UnityEngine.Device.Application.isEditor && !UnityEngine.Device.Application.isMobilePlatform
				? ApplicationRunMode.Editor
				: ApplicationRunMode.Simulator;
#elif UNITY_ANDROID || UNITY_IOS
			return ApplicationRunMode.Device;
#else
			return ApplicationRunMode.Desktop;
#endif
		}
	}
	#endregion
}

