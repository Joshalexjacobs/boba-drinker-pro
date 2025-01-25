using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public enum GameState
    {
        Playing,
        GameOver,
        Win,
    }

    public GameState gameState = GameState.Playing;

    [SerializeField]
    private TeaTimer _teaTimer;

    [SerializeField]
    private GameObject _startingPanel;

    [SerializeField]
    private TextMeshProUGUI _countdownText;

    [SerializeField]
    private GameObject _gameOverPanel;

    [SerializeField]
    private GameObject _youWinPanel;

    [SerializeField]
    private List<Boba> _bobas;

    private IEnumerator Start()
    {
        int seconds = 3;

        yield return new WaitForSeconds(1f);

        for (int i = seconds; i > 0f; i--)
        {
            _countdownText.text = $"{i}";

            yield return new WaitForSeconds(1f);
        }

        gameState = GameState.Playing;

        _countdownText.text = "GO!";

        yield return new WaitForSeconds(3f);

        _startingPanel.SetActive(false);
    }

    private void Update()
    {
        if (gameState == GameState.Playing)
        {
            var gameover = _bobas.Count((boba) => boba != null) == 0;

            if (gameover)
            {
                YouWin();
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

    public void YouWin()
    {
        if (gameState == GameState.Playing)
        {
            gameState = GameState.Win;

            _youWinPanel.SetActive(true);
        }
    }

}
