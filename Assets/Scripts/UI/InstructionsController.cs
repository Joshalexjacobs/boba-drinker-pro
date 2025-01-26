using UnityEngine;
using UnityEngine.SceneManagement;
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

    private void Awake()
    {
        _letsGoButton = _uiDocument.rootVisualElement.Q<Button>("LetsGoButton");

        _letsGoButton.RegisterCallback<ClickEvent>(e =>
        {
            AudioManager.instance.Play(AudioManager.AudioClips.BubblePop);
            SceneManager.LoadScene("Straw Test");
        });
    }

}
