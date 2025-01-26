using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TitleController : MonoBehaviour
{

    [SerializeField]
    private UIDocument _uiDocument;

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
            SceneManager.LoadScene("Credits");
        });
    }

}
