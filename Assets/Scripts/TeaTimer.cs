using System;
using UnityEngine;

public class TeaTimer : MonoBehaviour
{

    public enum TeaTimerState
    {

        Idle,
        Started,
        GameOver,
        Paused,

    }

    public TeaTimerState currentState = TeaTimerState.Idle;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private float _maxTime = 20f;

    private float _startTime = 0f;

    private float _teaSpriteHeight = 0f;

    private TimeSpan _timeSpan;

    private void Start()
    {
        _teaSpriteHeight = _spriteRenderer.size.y;
    }

    public void StartTeaTimer()
    {
        _startTime = Time.timeSinceLevelLoad;

        currentState = TeaTimerState.Started;
    }

    public void PauseTeaTimer()
    {
        currentState = TeaTimerState.Paused;
    }

    private void Update()
    {
        if (currentState == TeaTimerState.Started)
        {
            _timeSpan = TimeSpan.FromSeconds(Time.timeSinceLevelLoad - _startTime);

            if (_timeSpan.Seconds >= _maxTime)
            {
                // out of time
                currentState = TeaTimerState.GameOver;
            }
            else
            {
                UpdateTeaSpriteHeight();
            }
        }
    }

    private void UpdateTeaSpriteHeight()
    {
        var timeElapsedPercentage = (float)_timeSpan.TotalSeconds / _maxTime;

        _spriteRenderer.size = new Vector2(_spriteRenderer.size.x, _teaSpriteHeight - (_teaSpriteHeight * timeElapsedPercentage));
    }

}
