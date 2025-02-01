using System;
using System.Collections;
using CandyCoded;
using ScottDoxey;
using UnityEngine;

public class StrawController : MonoBehaviour
{

    [SerializeField]
    private RopeController _ropeController;

    [SerializeField]
    private Transform _strawBottomTransform;

    private const float _strawMovementSpeed = 15f;

    private Camera _mainCamera;

    private int? _currentFingerId;

    private bool _isDraggable = true;

    private bool _isDragging;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    public IEnumerator MoveStrawOutOfDrink()
    {
        _isDraggable = false;
        _isDragging = false;

        yield return Animate.MoveTo(gameObject, new Vector3(0, 16, 0), 1f, Space.World);
    }

    public IEnumerator MoveStrawBack()
    {
        ResetRope();

        _ropeController.SetupRope();

        yield return new WaitForSeconds(1f);

        yield return Animate.MoveTo(gameObject, new Vector3(0, 8.35f, 0), 1f, Space.World);

        _isDraggable = true;
    }

    private void Update()
    {
        if (!_isDraggable)
        {
            return;
        }

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

    private void OnEnable()
    {
        ResetRope();
    }

    private void OnDisable()
    {
        ResetRope();
    }

    private void ResetRope()
    {
        _ropeController.TeardownRope();

        _strawBottomTransform.localPosition = new Vector3(0, -6f, 0);

        _isDragging = false;
    }

}
