using Cysharp.Threading.Tasks;
using UnityEngine;

public class DrinkManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _drinkPrefab;

    [SerializeField]
    private bool _spawning = true;

    private async UniTask Start()
    {
        while (_spawning)
        {
            var spawnedDrink = Instantiate(_drinkPrefab, new Vector2(-10, -2.3f), Quaternion.identity);

            var drinkController = spawnedDrink.GetComponent<DrinkController>();

            await drinkController.AnimateIn(0.0025f);

            await UniTask.Delay(1000);

            await drinkController.AnimateOut(0.0025f);

            Destroy(spawnedDrink);
        }

    }

}
