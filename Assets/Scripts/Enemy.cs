using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField, Min(0.1f)] private float _lifetime = 5f;

    public event Action<Enemy> LifetimeOver;

    private Rigidbody _rigidbody;
    private Vector3 _moveDirection = Vector3.forward;
    private Coroutine _lifetimeCoroutine;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 velocity = _rigidbody.velocity;

        velocity.x = _moveDirection.x * _speed;
        velocity.z = _moveDirection.z * _speed;

        _rigidbody.velocity = velocity;
    }

    public void Initialize(Vector3 direction)
    {
        _moveDirection = direction.normalized;

        if (_lifetimeCoroutine != null)
        {
            StopCoroutine(_lifetimeCoroutine);
        }

        _lifetimeCoroutine = StartCoroutine(LifetimeRoutine());
    }

    public void ResetState()
    {
        if (_lifetimeCoroutine != null)
        {
            StopCoroutine(_lifetimeCoroutine);
            _lifetimeCoroutine = null;
        }

        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }

    private IEnumerator LifetimeRoutine()
    {
        yield return new WaitForSeconds(_lifetime);
        LifetimeOver?.Invoke(this);
        _lifetimeCoroutine = null;
    }
}