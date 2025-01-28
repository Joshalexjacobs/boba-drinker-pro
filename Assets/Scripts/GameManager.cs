using System;
using UnityEngine;
using UnityEngine.UIElements;

public enum GameState
{

    Title,

    Instructions,

    Credits

}

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;

    [SerializeField]
    private UIDocument _titleDocument;

    [SerializeField]
    private UIDocument _instructionsDocument;

    [SerializeField]
    private UIDocument _creditsDocument;

    private GameState _currentState = GameState.Title;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        SwitchState(_currentState);
    }

    public static void SwitchState(GameState state)
    {

        switch (state)
        {

            case GameState.Title:
                _instance.SwitchToTitleState();

                break;
            case GameState.Instructions:
                _instance.SwitchToInstructionsState();

                break;
            case GameState.Credits:
                _instance.SwitchToCreditsState();

                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }

    }

    private void SwitchToTitleState()
    {
        _titleDocument.gameObject.SetActive(true);

        _instructionsDocument.gameObject.SetActive(false);
        _creditsDocument.gameObject.SetActive(false);
    }

    private void SwitchToInstructionsState()
    {
        _instructionsDocument.gameObject.SetActive(true);

        _titleDocument.gameObject.SetActive(false);
        _creditsDocument.gameObject.SetActive(false);
    }

    private void SwitchToCreditsState()
    {
        _creditsDocument.gameObject.SetActive(true);

        _titleDocument.gameObject.SetActive(false);
        _instructionsDocument.gameObject.SetActive(false);
    }

}
