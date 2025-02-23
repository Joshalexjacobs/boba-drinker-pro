using System;
using UnityEngine;

public class TeaTimer : MonoBehaviour
{

    public enum TeaTimerState
    {

        Idle,
        Slurping,
        Paused,
        GameOver,

    }

    public TeaTimerState currentState = TeaTimerState.Idle;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private BoxCollider _boxCollider;

    private StrawController _strawController;

    private GameManager _gameManager;

    private float _teaSpriteHeight = 0f;

    private float _percentageSlurped = 0f;

    [SerializeField]
    private float _slurpSpeed = 0.25f;

    private void Start()
    {
        _strawController = StrawController.LocateStrawController();

        _gameManager = GameManager.LocateGameManager();

        _slurpSpeed *= _gameManager.drinksCleared == 0 ? 1 : _gameManager.drinksCleared;

        _teaSpriteHeight = _spriteRenderer.size.y;
    }

    public void PauseTeaTimer()
    {
        currentState = TeaTimerState.Paused;
    }

    private void Update()
    {
        if (currentState == TeaTimerState.Slurping)
        {
            _percentageSlurped += Time.deltaTime * _slurpSpeed;

            if (_percentageSlurped >= 0.9f)
            {
                _gameManager.GameOver();

                currentState = TeaTimerState.GameOver;

                _strawController.StopParticles();
            }

            UpdateTeaSpriteHeight();
        }

        _boxCollider.size = new Vector3(_spriteRenderer.size.x, _spriteRenderer.size.y, 0.2f);

        _boxCollider.center = new Vector3(0f, _boxCollider.size.y / 2, 0f);
    }

    private void UpdateTeaSpriteHeight()
    {
        _spriteRenderer.size = new Vector2(_spriteRenderer.size.x, _teaSpriteHeight - (_teaSpriteHeight * _percentageSlurped));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (currentState == TeaTimerState.Paused || currentState == TeaTimerState.Idle)
        {
            if (other.CompareTag(BobaSlurper.Tag))
            {
                currentState = TeaTimerState.Slurping;

                _strawController.StartParticles();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (currentState == TeaTimerState.Slurping)
        {
            if (other.CompareTag(BobaSlurper.Tag))
            {
                currentState = TeaTimerState.Paused;

                _strawController.StopParticles();
            }
        }
    }

}
