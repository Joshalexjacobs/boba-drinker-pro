using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Boba : MonoBehaviour
{

    [SerializeField]
    private BoxCollider _boxCollider;

    [SerializeField]
    private Rigidbody _rigidbody;

    public IEnumerator SlurpBoba(LineRenderer lineRenderer)
    {
        _boxCollider.enabled = false;

        _rigidbody.isKinematic = true;

        for (int i = lineRenderer.positionCount - 1; i >= 0; i--)
        {
            var destination = lineRenderer.GetPosition(i);

            yield return CandyCoded.Animate.MoveTo(gameObject, destination, 0.01f, Space.World);
        }

        Destroy(gameObject);
    }

}
