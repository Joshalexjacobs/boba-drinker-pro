using UnityEngine;
using UnityEngine.UIElements;

public class InstructionsController : MonoBehaviour
{

    [SerializeField]
    private UIDocument _uiDocument;

    [SerializeField]
    private Sprite _buttonDefaultSprite;

    [SerializeField]
    private Sprite _buttonDownSprite;

    private Button _letsGoButton;

    private void OnEnable()
    {
        _letsGoButton = _uiDocument.rootVisualElement.Q<Button>("LetsGoButton");

        _letsGoButton.RegisterBackgroundToggleImageEvents(_buttonDefaultSprite, _buttonDownSprite);

        _letsGoButton.RegisterCallback<ClickEvent>(LetsGoButtonHandler);
    }

    private void LetsGoButtonHandler(ClickEvent e)
    {
        AudioManager.instance.Play(AudioManager.AudioClips.BubblePop);
    }

    private void OnDisable()
    {
        _letsGoButton.UnregisterBackgroundToggleImageEvents();

        _letsGoButton.UnregisterCallback<ClickEvent>(LetsGoButtonHandler);
    }

}
