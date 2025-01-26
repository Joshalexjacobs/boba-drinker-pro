using System;
using System.Collections;
using System.Collections.Generic;
using ScottDoxey;
using Unity.VisualScripting;
using UnityEngine;

public class StrawController : MonoBehaviour
{

    private static readonly Lazy<StrawController> _instance = new(FindStrawController);

    public static StrawController LocateStrawController()
    {
        if (_instance.IsValueCreated)
            return _instance.Value;

        return FindStrawController();
    }

    private static StrawController FindStrawController()
    {
        return FindObjectOfType<StrawController>();
    }

    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private List<ParticleSystem> _particles;

    [SerializeField]
    private Transform _idlePoint;

    [SerializeField]
    private Transform _playPoint;

    public RopeController ropeController;

    public bool canTouch = false;

    private void Start()
    {
        _camera = Camera.main;
    }

    public IEnumerator SlideInStraw()
    {
        yield return CandyCoded.Animate.MoveTo(gameObject, _playPoint.position, 3f, Space.World);

        foreach (var sphereCollider in ropeController.GetColliders())
        {
            sphereCollider.enabled = true;
        }

        canTouch = true;
    }

    public void SlideOutStraw()
    {
        canTouch = false;

        foreach (var sphereCollider in ropeController.GetColliders())
        {
            sphereCollider.enabled = false;
        }

        transform.position = _idlePoint.position;
    }

    private void Update()
    {
        if (canTouch && Input.GetMouseButton(0))
        {
            var mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);

            transform.position = new Vector3(mousePosition.x, mousePosition.y, 0f);
        }
    }

    public void StartParticles()
    {
        foreach (var particleSystem in _particles)
        {
            particleSystem.Play();
        }
    }

    public void StopParticles()
    {
        foreach (var particleSystem in _particles)
        {
            particleSystem.Stop();
        }
    }

}
