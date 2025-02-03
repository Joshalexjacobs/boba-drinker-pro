using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkController : MonoBehaviour
{

    private const float MAX_VOLUME = 100f;

    [SerializeField]
    private GameObject _bobaPrefab;

    [SerializeField]
    private Transform _bobaSpawnOrigin;

    [SerializeField]
    private Transform _mask;

    [SerializeField]
    private Transform _maskTarget;

    private float _volume = MAX_VOLUME;

    private float _drinkSpeed = 0;

    private bool _isDrinking = false;

    public int bobaRemaining => _spawnedBoba.Count;

    private List<GameObject> _spawnedBoba = new();

    private Vector3 _startMaskPosition;

    private void Awake()
    {
        _startMaskPosition = _mask.localPosition;
    }

    public void StartingDrinking()
    {
        _isDrinking = true;
    }

    public void StopDrinking()
    {
        _isDrinking = false;
    }

    public IEnumerator SpawnBoba(int bobCount = 5)
    {
        var delayBetweenSpawningBoba = new WaitForSeconds(0.25f);

        while (bobCount > 0)
        {
            _spawnedBoba.Add(Instantiate(_bobaPrefab,
                _bobaSpawnOrigin.position + (Vector3)UnityEngine.Random.insideUnitCircle * 1.5f, Quaternion.identity));

            bobCount -= 1;

            yield return delayBetweenSpawningBoba;
        }
    }

    public IEnumerator Drink(float drinkSpeed)
    {
        _drinkSpeed = drinkSpeed;

        while (_volume > 0 && bobaRemaining > 0)
        {
            if (_isDrinking)
            {
                _volume -= _drinkSpeed * Time.deltaTime;
            }

            _mask.localPosition = Vector3.Lerp(_startMaskPosition, _maskTarget.localPosition,
                Mathf.InverseLerp(MAX_VOLUME, 0, _volume));

            yield return null;
        }
    }

    public bool EatBoba(GameObject boba)
    {
        if (!_spawnedBoba.Contains(boba))
        {
            return false;
        }

        _spawnedBoba.Remove(boba);

        Destroy(boba);

        return true;
    }

    private void OnDisable()
    {
        foreach (var spawnedBoba in _spawnedBoba)
        {
            Destroy(spawnedBoba);
        }

        _spawnedBoba.Clear();
    }

}
