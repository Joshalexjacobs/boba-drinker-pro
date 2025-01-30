using CandyCoded;
using UnityEngine;

public class StrawController : MonoBehaviour
{

    private const float _strawMovementSpeed = 15f;

    private Camera _mainCamera;

    private int? _currentFingerId;

    private bool _isDragging;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (InputManager.GetInputDown(ref _currentFingerId))
        {
            _isDragging = true;
        }
        else if (InputManager.GetInputUp(ref _currentFingerId))
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

        var newPosition = new Vector3(position.x, position.y, 0f);

        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, newPosition,
            _strawMovementSpeed * Time.deltaTime);
    }

}
