using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{

    private static readonly Lazy<GameManager> _instance = new(FindGameManager);

    public static GameManager LocateGameManager()
    {
        if (_instance.IsValueCreated)
            return _instance.Value;

        return FindGameManager();
    }

    private static GameManager FindGameManager()
    {
        return FindObjectOfType<GameManager>();
    }

    public enum GameState
    {

        Starting,

        Playing,

        GameOver,

        Win,

    }

    public GameState gameState = GameState.Starting;

    [SerializeField]
    private DrinkManager _drinkManager;

    [SerializeField]
    private Boba _bobaPrefab;

    [SerializeField]
    private UIDocument _countdownDocument;

    [SerializeField]
    private UIDocument _gameOverDocument;

    [SerializeField]
    private List<Boba> _bobas = new();

    public int drinksCleared = 0;

    private void Awake()
    {
        _gameOverDocument.rootVisualElement.Q<Button>("RetryButton").RegisterCallback<ClickEvent>(e =>
        {
            AudioManager.instance.Play(AudioManager.AudioClips.BubblePop);
            SceneManager.LoadScene("Instructions");
        });
    }

    private async UniTask Start()
    {
        gameState = GameState.Starting;

        _countdownDocument.enabled = true;
        _gameOverDocument.enabled = false;

        await _drinkManager.SpawnDrink();

        int seconds = 3;

        await UniTask.Delay(1000);

        _bobas.Clear();

        _drinkManager.currentDrink.SpawnSpinners(drinksCleared);

        foreach (var currentDrinkBobaSpawnPoint in _drinkManager.currentDrink.bobaSpawnPoints.RandomRange(
                     Mathf.Clamp(2 * drinksCleared, 2, 8)))
        {
            _bobas.Add(Instantiate(_bobaPrefab, currentDrinkBobaSpawnPoint.position, Quaternion.identity));
        }

        var strawController = StrawController.LocateStrawController();

        if (strawController)
        {
            StartCoroutine(strawController.SlideInStraw());
        }

        var countdownText = _countdownDocument.rootVisualElement.Q<Label>("CountdownLabel");

        for (int i = seconds; i > 0f; i--)
        {
            countdownText.text = $"{i}";

            await UniTask.Delay(1000);
        }

        gameState = GameState.Playing;

        countdownText.text = "GO!";

        await UniTask.Delay(1000);

        _countdownDocument.enabled = false;
    }

    private void Update()
    {
        if (gameState == GameState.Playing)
        {
            var gameover = _bobas.Count((boba) => boba != null) == 0;

            if (gameover)
            {
                gameState = GameState.Win;

                UniTask.RunOnThreadPool(YouWin);
            }
        }
    }

    public void GameOver()
    {
        if (gameState == GameState.Playing)
        {
            _gameOverDocument.enabled = true;

            _gameOverDocument.rootVisualElement.Q<Label>().text = $"{drinksCleared}";
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public async UniTask YouWin()
    {
        await UniTask.SwitchToMainThread();

        var strawController = StrawController.LocateStrawController();

        if (strawController)
        {
            strawController.SlideOutStraw();

            await UniTask.Delay(2000);
        }

        await _drinkManager.DespawnDrink();

        drinksCleared++;

        await UniTask.Delay(2000);

        await Start();
    }

}
