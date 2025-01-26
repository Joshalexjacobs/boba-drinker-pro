using UnityEngine;
using UnityEngine.UIElements;

public class BackgroundAnimationController : MonoBehaviour
{

    public const float backgroundMovementSpeed = 20f;

    public static Vector2 backgroundPositionOffset = Vector2.zero;

    [SerializeField]
    private UIDocument _uiDocument;

    private VisualElement _background;

    private void Awake()
    {
        _background = _uiDocument.rootVisualElement.Q<VisualElement>("Background");
    }

    private void Update()
    {
        backgroundPositionOffset += Vector2.one * backgroundMovementSpeed * Time.deltaTime;

        _background.SetBackgroundPosition(backgroundPositionOffset);
    }

}
