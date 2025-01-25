using UnityEngine;

public class Fake2D : MonoBehaviour {
  
  private void Update() {
    transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
  }
  
}
