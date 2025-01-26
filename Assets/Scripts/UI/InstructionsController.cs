using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class InstructionsController : MonoBehaviour
{

    [SerializeField]
    private UIDocument _uiDocument;

    private Button _letsGoButton;

    private void Awake()
    {
        _letsGoButton = _uiDocument.rootVisualElement.Q<Button>("LetsGoButton");

        _letsGoButton.RegisterCallback<ClickEvent>(e =>
        {
            SceneManager.LoadScene("SampleScene");
        });
    }

}
