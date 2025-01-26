using UnityEngine;
using UnityEngine.SceneManagement;
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

    private void Awake()
    {
        _playButton = _uiDocument.rootVisualElement.Q<Button>("PlayButton");

        _creditsButton = _uiDocument.rootVisualElement.Q<Button>("CreditsButton");

        _playButton.RegisterCallback<ClickEvent>(e =>
        {
            SceneManager.LoadScene("Instructions");
        });

        _creditsButton.RegisterCallback<ClickEvent>(e =>
        {
            AudioManager.instance.Play(AudioManager.AudioClips.BubblePop);
            SceneManager.LoadScene("Credits");
        });
    }

}
