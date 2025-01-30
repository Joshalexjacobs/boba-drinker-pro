using UnityEngine;

namespace DefaultNamespace
{

    public class StrawBottomController : MonoBehaviour
    {

        private DrinkController _drinkController;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Tea"))
            {
                return;
            }

            if (_drinkController == null)
            {
                _drinkController = FindFirstObjectByType<DrinkController>();
            }

            _drinkController.StartingDrinking();
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Tea"))
            {
                return;
            }

            if (_drinkController == null)
            {
                _drinkController = FindFirstObjectByType<DrinkController>();
            }

            _drinkController.StopDrinking();
        }

    }

}
