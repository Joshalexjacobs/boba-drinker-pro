using UnityEngine;
using UnityEngine.UIElements;

public class BackgroundAnimationController : MonoBehaviour
{

    [SerializeField]
    private float _backgroundMovementSpeed = 20f;

    [SerializeField]
    private Vector2 _backgroundMovementDirection = new(1f, -1f);

    private static Vector2 _backgroundPositionOffset = Vector2.zero;

    [SerializeField]
    private UIDocument _uiDocument;

    private VisualElement _background;

    private void OnEnable()
    {
        _background = _uiDocument.rootVisualElement.Q<VisualElement>("Background");
    }

    private void Update()
    {
        _backgroundPositionOffset += _backgroundMovementDirection * _backgroundMovementSpeed * Time.deltaTime;

        _background.SetBackgroundPosition(_backgroundPositionOffset);
    }

}
