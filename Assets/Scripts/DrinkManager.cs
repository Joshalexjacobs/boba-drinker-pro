using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class DrinkManager : MonoBehaviour
{

    [SerializeField]
    private DrinkController _drinkPrefab;

    public DrinkController currentDrink;

    public async UniTask SpawnDrink()
    {
        currentDrink = Instantiate(_drinkPrefab, new Vector2(-10, -2.3f), Quaternion.identity);

        currentDrink.transform.localScale = gameObject.transform.localScale;

        await currentDrink.AnimateIn(0.0025f);
    }

    public async UniTask DespawnDrink()
    {
        await currentDrink.AnimateOut(0.0025f);

        Destroy(currentDrink);
    }

}
