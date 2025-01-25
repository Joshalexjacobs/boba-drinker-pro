using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class CreditsController : MonoBehaviour
{

    [SerializeField]
    private UIDocument _uiDocument;

    private Button _backButton;

    private void Awake()
    {

        _backButton = _uiDocument.rootVisualElement.Q<Button>("BackButton");

        _backButton.RegisterCallback<ClickEvent>(e =>
        {
            SceneManager.LoadScene("Title");
        });

    }

}
