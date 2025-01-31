using UnityEngine;

public class StrawBottomController : MonoBehaviour
{

    private DrinkController _drinkController;

    private const float _testRadius = 0.25f;

    private const float _testDistance = 0.5f;

    private readonly Collider[] _hitColliders = new Collider[10];

    private Vector3 _testPosition => gameObject.transform.position - gameObject.transform.up * _testDistance;

    private void Update()
    {
        if (Physics.OverlapSphereNonAlloc(_testPosition, _testRadius, _hitColliders, LayerMask.NameToLayer("Boba")) ==
            0)
        {
            return;
        }

        foreach (var hit in _hitColliders)
        {
            if (hit == null || !hit.gameObject.CompareTag("Boba"))
            {
                continue;
            }

            AudioManager.instance.Play(AudioManager.AudioClips.Slurp);
            HapticsController.TriggerHapticFeedback();

            Destroy(hit.gameObject);
        }
    }

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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_testPosition, _testRadius);
    }

}
