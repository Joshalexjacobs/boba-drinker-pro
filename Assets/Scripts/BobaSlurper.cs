using UnityEngine;

public class BobaSlurper : MonoBehaviour
{

    public const string Tag = "Boba Slurper";

    [SerializeField]
    private float _distance = 5f;

    [SerializeField]
    private LayerMask _bobaMask;

    [SerializeField]
    private AudioManager _audioManager;

    [SerializeField]
    private LineRenderer _lineRenderer;

    private void Update()
    {
        Vector3 direction = transform.TransformDirection(Vector3.down) * _distance;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction, out hit, _distance, _bobaMask))
        {
            Destroy(hit.transform.gameObject);

            // var boba = hit.transform.gameObject.GetComponent<Boba>();

            // if (boba)
            // {
                // Destroy(boba.gameObject);

                // StartCoroutine(boba.SlurpBoba(_lineRenderer));

                _audioManager.Play(AudioManager.AudioClips.Slurp);

                HapticsController.TriggerHapticFeedback();
            // }
        }
    }

}
