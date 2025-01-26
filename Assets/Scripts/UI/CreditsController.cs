using UnityEngine;
using UnityEngine.SceneManagement;
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

    private void Awake()
    {

        _backButton = _uiDocument.rootVisualElement.Q<Button>("BackButton");

        _backButton.RegisterCallback<ClickEvent>(e =>
        {
            AudioManager.instance.Play(AudioManager.AudioClips.BubblePop);
            SceneManager.LoadScene("Title");
        });

    }

}
