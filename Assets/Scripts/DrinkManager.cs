using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class DrinkManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _drinkPrefab;

    [SerializeField]
    private UIDocument _countdownDialogDocument;

    [SerializeField]
    private Transform _spawnPoint;

    [SerializeField]
    private Transform _restPoint;

    [SerializeField]
    private Transform _despawnPoint;

    [SerializeField]
    private StrawController _strawController;

    private float _drinkSpeed = 5;

    private float _drinkSpeedStep = 3;

    private readonly List<GameObject> _spawnedDrinks = new();

    private Coroutine _animateDrinkCoroutine;

    private Coroutine _spawnBobaCoroutine;

    private Coroutine _animateStrawCoroutine;

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
            var spawnedDrink = Instantiate(_drinkPrefab, _spawnPoint.position, Quaternion.identity);

            _spawnedDrinks.Add(spawnedDrink);

            spawnedDrink.transform.localScale = gameObject.transform.localScale;

            _countdownDialogDocument.gameObject.SetActive(true);

            var countdownLabel = _countdownDialogDocument.rootVisualElement.Q<Label>("Countdown");

            countdownLabel.style.fontSize = 60;
            countdownLabel.text = "Get Ready!";

            yield return CandyCoded.Animate.MoveTo(spawnedDrink.gameObject, _restPoint.position, 1, Space.World);

            _animateStrawCoroutine = StartCoroutine(_strawController.MoveStrawBack());

            countdownLabel.style.fontSize = 120;
            countdownLabel.text = "3";

            spawnedDrink.TryGetComponent(out DrinkController spawnedDrinkController);

            _spawnBobaCoroutine = StartCoroutine(spawnedDrinkController.SpawnBoba());

            yield return new WaitForSeconds(1);

            countdownLabel.style.fontSize = 120;
            countdownLabel.text = "2";

            yield return new WaitForSeconds(1);

            countdownLabel.style.fontSize = 120;
            countdownLabel.text = "1";

            yield return new WaitForSeconds(1);

            _countdownDialogDocument.gameObject.SetActive(false);

            yield return spawnedDrinkController.Drink(_drinkSpeed);

            if (spawnedDrinkController.bobaRemaining > 0)
            {
                GameManager.SwitchState(GameState.GameOver);
            }

            yield return _strawController.MoveStrawOutOfDrink();

            yield return CandyCoded.Animate.MoveTo(spawnedDrink.gameObject, _despawnPoint.position, 1,
                Space.World);

            _spawnedDrinks.Remove(spawnedDrink);

            Destroy(spawnedDrink);

            _drinkSpeed += _drinkSpeedStep;

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

        if (_animateDrinkCoroutine != null)
        {
            StopCoroutine(_animateDrinkCoroutine);

            _animateDrinkCoroutine = null;
        }

        if (_spawnBobaCoroutine != null)
        {
            StopCoroutine(_spawnBobaCoroutine);

            _spawnBobaCoroutine = null;
        }

        if (_animateStrawCoroutine != null)
        {
            StopCoroutine(_animateStrawCoroutine);

            _animateStrawCoroutine = null;
        }

        foreach (var spawnedDrink in _spawnedDrinks)
        {
            Destroy(spawnedDrink);
        }
    }

}
