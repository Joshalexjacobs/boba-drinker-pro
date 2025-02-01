using UnityEngine;

public class StrawBottomController : MonoBehaviour
{

    [SerializeField]
    private LayerMask _bobaLayer;

    private DrinkController _drinkController;

    private const float _testRadius = 0.25f;

    private const float _testDistance = 0.25f;

    private readonly Collider[] _hitColliders = new Collider[10];

    private Vector3 _testPosition => gameObject.transform.position - gameObject.transform.up * _testDistance;

    private DrinkController GetDrinkController()
    {
        if (_drinkController == null)
        {
            _drinkController = FindFirstObjectByType<DrinkController>();
        }

        return _drinkController;
    }

    private void Update()
    {
        if (Physics.OverlapSphereNonAlloc(_testPosition, _testRadius, _hitColliders, _bobaLayer) ==
            0)
        {
            return;
        }

        foreach (var hit in _hitColliders)
        {
            if (hit == null)
            {
                continue;
            }

            AudioManager.instance.Play(AudioManager.AudioClips.Slurp);
            HapticsController.TriggerHapticFeedback();

            GetDrinkController().EatBoba(hit.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Tea"))
        {
            return;
        }

        GetDrinkController().StartingDrinking();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Tea"))
        {
            return;
        }

        GetDrinkController().StopDrinking();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_testPosition, _testRadius);
    }

}
