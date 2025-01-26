using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrawController : MonoBehaviour {

    private static readonly Lazy<StrawController> _instance = new (FindStrawController);

    public static StrawController LocateStrawController() {
        if (_instance.IsValueCreated)
            return _instance.Value;

        return FindStrawController();
    }

    private static StrawController FindStrawController() {
        return FindObjectOfType<StrawController>();
    }

  [SerializeField] private Camera _camera;

  [SerializeField]
  private List<ParticleSystem> _particles;

  [SerializeField]
  private Transform _idlePoint;

  [SerializeField]
  private Transform _playPoint;

  public bool canTouch = false;

  private void Start() {
    _camera = Camera.main;
  }

  public IEnumerator SlideInStraw()
  {
      yield return CandyCoded.Animate.MoveTo(gameObject, _playPoint.position, 3f, Space.World);

      canTouch = true;
  }

  public IEnumerator SlideOutStraw()
  {
      canTouch = false;

      yield return CandyCoded.Animate.MoveTo(gameObject, _idlePoint.position, 1f,  Space.World);
  }

  private void Update() {
    if (canTouch && Input.GetMouseButton(0)) {
      var mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);

      transform.position = new Vector3(mousePosition.x, mousePosition.y, 0f);
    }
  }

  public void StartParticles()
  {
      foreach (var particleSystem in _particles) {
          particleSystem.Play();
      }
  }

  public void StopParticles()
  {
      foreach (var particleSystem in _particles) {
          particleSystem.Stop();
      }
  }

}
