using UnityEngine;

public class StrawController : MonoBehaviour {

  [SerializeField] private Camera _camera;

  private void Start() {
    _camera = Camera.main;
  }

  private void Update() {
    if (Input.GetMouseButton(0)) {
      var mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);

      transform.position = new Vector3(mousePosition.x, mousePosition.y, 0f);
    }
  }
  
}
