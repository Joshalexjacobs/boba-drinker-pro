using UnityEngine;
using UnityEngine.UIElements;

public class TitleController : MonoBehaviour
{

    [SerializeField]
    private UIDocument _uiDocument;

    [SerializeField]
    private Sprite _buttonDefaultSprite;

    [SerializeField]
    private Sprite _buttonDownSprite;

    private Button _playButton;

    private Button _creditsButton;

    private void OnEnable()
    {
        _playButton = _uiDocument.rootVisualElement.Q<Button>("PlayButton");
        _creditsButton = _uiDocument.rootVisualElement.Q<Button>("CreditsButton");

        _playButton.RegisterBackgroundToggleImageEvents(_buttonDefaultSprite, _buttonDownSprite);
        _creditsButton.RegisterBackgroundToggleImageEvents(_buttonDefaultSprite, _buttonDownSprite);

        _playButton.RegisterCallback<ClickEvent>(PlayButtonClickHandler);
        _creditsButton.RegisterCallback<ClickEvent>(CreditsButtonClickHandler);
    }

    private void PlayButtonClickHandler(ClickEvent e)
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
        _playButton.UnregisterBackgroundToggleImageEvents();
        _creditsButton.UnregisterBackgroundToggleImageEvents();

        _playButton.UnregisterCallback<ClickEvent>(PlayButtonClickHandler);
        _playButton.UnregisterCallback<ClickEvent>(CreditsButtonClickHandler);
    }

}
