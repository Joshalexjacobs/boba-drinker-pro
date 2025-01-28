using CandyCoded;
using UnityEngine;

public class StrawController : MonoBehaviour
{

    private Camera _mainCamera;

    private int? _currentFingerId;

    private bool _isDragging;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (gameObject.GetInputDown(_mainCamera, ref _currentFingerId, out RaycastHit _))
        {
            _isDragging = true;
        }
        else if (gameObject.GetInputUp(_mainCamera, ref _currentFingerId, out RaycastHit _))
        {
            _isDragging = false;
        }

        if (!_isDragging)
        {
            return;
        }

        var inputPosition = InputManager.GetInputPosition(_currentFingerId);

        if (!inputPosition.HasValue)
        {
            return;
        }

        var position = _mainCamera.ScreenToWorldPoint(inputPosition.Value);

        gameObject.transform.position = new Vector3(position.x, position.y, 0f);
    }

}
