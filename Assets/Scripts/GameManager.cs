using System;
using UnityEngine;
using UnityEngine.UIElements;

public enum GameState
{

    Title,

    Instructions,

    GameLoop,

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

    [SerializeField]
    private DrinkManager _drinkManager;

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
            case GameState.GameLoop:
                _instance.SwitchToGameLoopState();

                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }

    }

    private void SwitchToTitleState()
    {
        SwitchToGameObject(_titleDocument.gameObject);
    }

    private void SwitchToInstructionsState()
    {
        SwitchToGameObject(_instructionsDocument.gameObject);
    }

    private void SwitchToGameLoopState()
    {
        SwitchToGameObject(_drinkManager.gameObject);
    }

    private void SwitchToCreditsState()
    {
        SwitchToGameObject(_creditsDocument.gameObject);
    }

    private void SwitchToGameObject(GameObject newActiveGameObject)
    {
        _titleDocument.gameObject.SetActive(newActiveGameObject.Equals(_titleDocument.gameObject));
        _instructionsDocument.gameObject.SetActive(newActiveGameObject.Equals(_instructionsDocument.gameObject));
        _drinkManager.gameObject.SetActive(newActiveGameObject.Equals(_drinkManager.gameObject));
        _creditsDocument.gameObject.SetActive(newActiveGameObject.Equals(_creditsDocument.gameObject));
    }

}
