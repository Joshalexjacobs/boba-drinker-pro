using UnityEngine;
using UnityEngine.UIElements;

public class CreditsController : MonoBehaviour
{

    [SerializeField]
    private UIDocument _uiDocument;

    [SerializeField]
    private Sprite _buttonDefaultSprite;

    [SerializeField]
    private Sprite _buttonDownSprite;

    private Button _backButton;

    private void OnEnable()
    {
        _backButton = _uiDocument.rootVisualElement.Q<Button>("BackButton");

        _backButton.RegisterBackgroundToggleImageEvents(_buttonDefaultSprite, _buttonDownSprite);

        _backButton.RegisterCallback<ClickEvent>(BackButtonHandler);
    }

    private void BackButtonHandler(ClickEvent _)
    {
        AudioManager.instance.Play(AudioManager.AudioClips.BubblePop);
        GameManager.SwitchState(GameState.Title);
    }

    private void OnDisable()
    {
        _backButton.UnregisterBackgroundToggleImageEvents();

        _backButton.UnregisterCallback<ClickEvent>(BackButtonHandler);
    }

}
