using UnityEngine;

public class CopyLineRenderer : MonoBehaviour
{

    [SerializeField]
    private LineRenderer _lineRenderer;

    [SerializeField]
    private LineRenderer _toCopy;

    private void Update()
    {
        _lineRenderer.positionCount = _toCopy.positionCount;

        for (int i = 0; i < _toCopy.positionCount; i++)
        {
            _lineRenderer.SetPosition(i, _toCopy.GetPosition(i));
        }
    }
}
