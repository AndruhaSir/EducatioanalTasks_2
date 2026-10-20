using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField, Min(0.1f)] private float _lifetime = 5f;

    private Rigidbody _rigidbody;
    private EnemyPool _pool;
    private Vector3 _moveDirection = Vector3.forward;
    private Coroutine _lifetimeCoroutine;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Initialize(EnemyPool pool, Vector3 direction)
    {
        _pool = pool;
        _moveDirection = direction.normalized;

        if (_lifetimeCoroutine != null)
        {
            StopCoroutine(_lifetimeCoroutine);
        }

        _lifetimeCoroutine = StartCoroutine(LifetimeRoutine());
    }

    private void FixedUpdate()
    {
        Vector3 velocity = _rigidbody.velocity;

        velocity.x = _moveDirection.x * _speed;
        velocity.z = _moveDirection.z * _speed;

        _rigidbody.velocity = velocity;
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
        _pool.ReturnEnemy(this);
        _lifetimeCoroutine = null;
    }
}