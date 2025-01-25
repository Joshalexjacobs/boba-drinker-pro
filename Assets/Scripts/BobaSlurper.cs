using UnityEngine;

public class BobaSlurper : MonoBehaviour
{

    public const string Tag = "Boba Slurper";

    [SerializeField]
    private float _distance = 5f;

    [SerializeField]
    private LayerMask _bobaMask;

    private void Update()
    {
        Vector3 direction = transform.TransformDirection(Vector3.down) * _distance;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction, out hit, _distance, _bobaMask))
        {
            Destroy(hit.transform.gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(Vector3.down) * _distance;
        Gizmos.DrawRay(transform.position, direction);
    }

}
