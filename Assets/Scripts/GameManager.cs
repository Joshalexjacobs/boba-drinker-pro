using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CandyCoded;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private static readonly Lazy<GameManager> _instance = new (FindGameManager);

    public static GameManager LocateGameManager() {
        if (_instance.IsValueCreated)
            return _instance.Value;

        return FindGameManager();
    }

    private static GameManager FindGameManager() {
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
    private GameObject _startingPanel;

    [SerializeField]
    private TextMeshProUGUI _countdownText;

    [SerializeField]
    private GameObject _gameOverPanel;

    [SerializeField]
    private GameObject _youWinPanel;

    [SerializeField]
    private List<Boba> _bobas = new ();

    public int drinksCleared = 0;

    private async UniTask Start()
    {
        gameState = GameState.Starting;

        await _drinkManager.SpawnDrink();

        int seconds = 3;

        await UniTask.Delay(1000);

        _bobas.Clear();

        foreach (var currentDrinkBobaSpawnPoint in _drinkManager.currentDrink.bobaSpawnPoints.RandomRange(Mathf.Clamp(2 * drinksCleared, 2, 8)))
        {
            _bobas.Add(Instantiate(_bobaPrefab, currentDrinkBobaSpawnPoint.position, Quaternion.identity));
        }

        var strawController = StrawController.LocateStrawController();

        if (strawController)
        {
            StartCoroutine(strawController.SlideInStraw());
        }

        for (int i = seconds; i > 0f; i--)
        {
            _countdownText.text = $"{i}";

            await UniTask.Delay(1000);
        }

        gameState = GameState.Playing;

        _countdownText.text = "GO!";

        await UniTask.Delay(1000);

        _startingPanel.SetActive(false);
    }

    private void Update()
    {
        if (gameState == GameState.Playing)
        {
            var gameover = _bobas.Count((boba) => boba != null) == 0;

            if (gameover)
            {
                StartCoroutine(YouWin());
            }
        }
    }

    public void GameOver()
    {
        if (gameState == GameState.Playing)
        {
            _gameOverPanel.SetActive(true);
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public IEnumerator YouWin()
    {
        if (gameState == GameState.Playing)
        {
            var strawController = StrawController.LocateStrawController();

            if (strawController)
            {
                Debug.Log("helloo??");
                yield return StartCoroutine(strawController.SlideOutStraw());
                Debug.Log("fuck unity");
            }

            _drinkManager.DespawnDrink();

            gameState = GameState.Win;

            _youWinPanel.SetActive(true);

            drinksCleared++;

            yield return new WaitForSeconds(2f);

            gameState = GameState.Starting;

            Start();
        }
    }

}
