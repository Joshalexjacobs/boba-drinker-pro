using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _drinkPrefab;

    [SerializeField]
    private Transform _spawnPoint;

    [SerializeField]
    private Transform _restPoint;

    [SerializeField]
    private Transform _despawnPoint;

    private readonly List<GameObject> _spawnedDrinks = new();

    private Coroutine _gameLoopCoroutine;

    private void OnEnable()
    {
        if (_gameLoopCoroutine != null)
        {
            StopCoroutine(_gameLoopCoroutine);

            _gameLoopCoroutine = null;
        }

        _gameLoopCoroutine = StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop()
    {
        while (gameObject.activeSelf)
        {
            var spawnedGameObject = Instantiate(_drinkPrefab, _spawnPoint.position, Quaternion.identity);

            spawnedGameObject.TryGetComponent(out DrinkController spawnedDrinkController);

            spawnedGameObject.transform.localScale = gameObject.transform.localScale;

            _spawnedDrinks.Add(spawnedGameObject);

            yield return CandyCoded.Animate.MoveTo(spawnedGameObject.gameObject, _restPoint.position, 1, Space.World);

            // yield return new WaitForSeconds(1f);

            spawnedDrinkController.StartingDrinking();

            yield return spawnedDrinkController.Drink();

            yield return CandyCoded.Animate.MoveTo(spawnedGameObject.gameObject, _despawnPoint.position, 1,
                Space.World);

            _spawnedDrinks.Remove(spawnedGameObject);

            Destroy(spawnedGameObject);

            // GameManager.SwitchState(GameState.GameOver);

            yield return null;
        }
    }

    private void OnDisable()
    {
        if (_gameLoopCoroutine != null)
        {
            StopCoroutine(_gameLoopCoroutine);

            _gameLoopCoroutine = null;
        }

        foreach (var spawnedDrink in _spawnedDrinks)
        {
            Destroy(spawnedDrink);
        }
    }

}
