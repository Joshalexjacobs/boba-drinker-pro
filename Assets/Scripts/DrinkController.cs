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
