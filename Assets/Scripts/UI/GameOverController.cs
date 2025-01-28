using UnityEngine;
using UnityEngine.UIElements;

public class GameOverController : MonoBehaviour
{

    [SerializeField]
    private UIDocument _uiDocument;

    [SerializeField]
    private Sprite _buttonDefaultSprite;

    [SerializeField]
    private Sprite _buttonDownSprite;

    private Button _retryButton;

    private Button _creditsButton;

    private void OnEnable()
    {
        _retryButton = _uiDocument.rootVisualElement.Q<Button>("RetryButton");
        _creditsButton = _uiDocument.rootVisualElement.Q<Button>("CreditsButton");

        _retryButton.RegisterBackgroundToggleImageEvents(_buttonDefaultSprite, _buttonDownSprite);
        _creditsButton.RegisterBackgroundToggleImageEvents(_buttonDefaultSprite, _buttonDownSprite);

        _retryButton.RegisterCallback<ClickEvent>(RetryButtonHandler);
        _creditsButton.RegisterCallback<ClickEvent>(CreditsButtonClickHandler);
    }

    private void RetryButtonHandler(ClickEvent e)
    {
        AudioManager.instance.Play(AudioManager.AudioClips.BubblePop);

        GameManager.SwitchState(GameState.Instructions);
    }

    private void CreditsButtonClickHandler(ClickEvent e)
    {
        AudioManager.instance.Play(AudioManager.AudioClips.BubblePop);

        GameManager.SwitchState(GameState.Credits);
    }

    private void OnDisable()
    {
        _retryButton.UnregisterBackgroundToggleImageEvents();
        _creditsButton.UnregisterBackgroundToggleImageEvents();

        _retryButton.UnregisterCallback<ClickEvent>(RetryButtonHandler);
        _creditsButton.UnregisterCallback<ClickEvent>(CreditsButtonClickHandler);
    }

}
