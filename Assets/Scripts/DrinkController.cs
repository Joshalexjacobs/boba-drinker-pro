using System.Collections;
using UnityEngine;

public class DrinkController : MonoBehaviour
{

    [SerializeField]
    private Transform _mask;

    [SerializeField]
    private Transform _maskTarget;

    public IEnumerator Drink()
    {
        yield return CandyCoded.Animate.MoveTo(_mask.gameObject, _maskTarget.position, 1);
    }

}
