using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField, Min(0.1f)] private float _lifetime = 5f;
    [SerializeField, Min(0.01f)] private float _reachDistance = 1f;

    public event Action<Enemy> ReturnRequested;

    private Rigidbody _rigidbody;
    private Coroutine _lifetimeCoroutine;
    private Transform _targetTransform;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MoveToTarget();
        CheckTargetCompleted();
    }

    public void Initialize(Transform targetTransform)
    {
        _targetTransform = targetTransform;

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

    private void MoveToTarget()
    {
        if (_targetTransform == null)
        {
            return;
        }

        Vector3 direction = (_targetTransform.position - gameObject.transform.position).normalized;

        Vector3 velocity = _rigidbody.velocity;
        velocity.x = direction.x * _speed;
        velocity.z = direction.z * _speed;

        _rigidbody.velocity = velocity;
    }

    private void RequestReturn()
    {
        if (_lifetimeCoroutine != null)
        {
            StopCoroutine(_lifetimeCoroutine);
            _lifetimeCoroutine = null;
        }

        ReturnRequested?.Invoke(this);
    }

    private IEnumerator LifetimeRoutine()
    {
        yield return new WaitForSeconds(_lifetime);
        RequestReturn();
    }

    private void CheckTargetCompleted()
    {
        if (_targetTransform == null)
        {
            Debug.LogWarning("Нет цели для движения", this);
            return;
        }

        Vector3 direction = _targetTransform.position - transform.position;
        direction.y = 0f;

        if (direction.magnitude <= _reachDistance)
        {
            RequestReturn();
        }
    }
}