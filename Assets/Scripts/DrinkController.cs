using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class DrinkController : MonoBehaviour
{

    [SerializeField]
    private AnimationCurve _animateIn;

    [SerializeField]
    private AnimationCurve _animateOut;

    public List<Transform> bobaSpawnPoints;

    public List<GameObject> _spinners;

    public void SpawnSpinners(int drinksDrank)
    {
        var spinnersToSpawn = Mathf.Clamp(drinksDrank, 0, _spinners.Count);

        foreach (var o in _spinners.RandomRange(spinnersToSpawn)) {
            o.SetActive(true);
        }
    }

    public async UniTask AnimateIn(float speed)
    {
        await AnimateToPosition(_animateIn, speed);
    }

    public async UniTask AnimateOut(float speed)
    {
        await AnimateToPosition(_animateOut, speed);
    }

    private async UniTask AnimateToPosition(AnimationCurve curve, float speed)
    {
        var timeElapsed = 0f;

        while (Mathf.Abs(gameObject.transform.position.x - curve.Evaluate(1)) > 0.1f)
        {
            var position = gameObject.transform.position;

            timeElapsed += speed;

            gameObject.transform.position = new Vector3(curve.Evaluate(timeElapsed), position.y);

            await UniTask.Yield();
        }

        gameObject.transform.position = new Vector3(curve.Evaluate(1), gameObject.transform.position.y);
    }

}
