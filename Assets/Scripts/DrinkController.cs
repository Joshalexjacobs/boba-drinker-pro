using System.Collections;
using UnityEngine;

public class DrinkController : MonoBehaviour
{

    private const float MAX_VOLUME = 100f;

    [SerializeField]
    private Transform _mask;

    [SerializeField]
    private Transform _maskTarget;

    private float _volume = MAX_VOLUME;

    private float _drinkSpeed = 0;

    private bool _isDrinking = false;

    public int bobaRemaining { get; private set; } = 0;

    public void StartingDrinking(float drinkSpeed = 100)
    {
        _drinkSpeed = drinkSpeed;

        _isDrinking = true;
    }

    public void StopDrinking()
    {
        _isDrinking = false;
    }

    public IEnumerator Drink()
    {
        var startMaskPosition = _mask.position;

        while (_volume > 0)
        {
            if (_isDrinking)
            {
                _volume -= _drinkSpeed * Time.deltaTime;
            }

            _mask.position = Vector3.Lerp(startMaskPosition, _maskTarget.position,
                Mathf.InverseLerp(MAX_VOLUME, 0, _volume));

            yield return null;
        }
    }

}
