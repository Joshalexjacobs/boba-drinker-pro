using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TitleController : MonoBehaviour
{

    [SerializeField]
    private UIDocument _uiDocument;

    private Button _playButton;

    private Button _creditsButton;

    private VisualElement _background;

    private void Awake()
    {

        _playButton = _uiDocument.rootVisualElement.Q<Button>("PlayButton");

        _creditsButton = _uiDocument.rootVisualElement.Q<Button>("CreditsButton");

        _background = _uiDocument.rootVisualElement.Q<VisualElement>("Background");

        _playButton.RegisterCallback<ClickEvent>(e =>
        {
            SceneManager.LoadScene("SampleScene");
        });

        _creditsButton.RegisterCallback<ClickEvent>(e =>
        {
            SceneManager.LoadScene("Credits");
        });

    }

    private void Update()
    {

        // _background.style.backgroundPositionY = 2;

    }

}
