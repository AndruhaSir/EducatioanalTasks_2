using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField, Min(2f)] private float _minLifetime = 2f;
    [SerializeField, Min(2f)] private float _maxLifetime = 5f;
    [SerializeField] private Color _initialColor = Color.white;

    private Renderer _renderer;
    private CubePool _cubePool;
    private Coroutine _lifetimeCoroutine;
    private bool _hasTouchedSurface;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void Initialize(CubePool cubePool)
    {
        _cubePool = cubePool;
        _hasTouchedSurface = false;

        if (_lifetimeCoroutine != null)
        {
            StopCoroutine(_lifetimeCoroutine);
            _lifetimeCoroutine = null;
        }

        _renderer.material.color = _initialColor;

        Rigidbody cubeRigidbody = GetComponent<Rigidbody>();

        if (cubeRigidbody != null)
        {
            cubeRigidbody.velocity = Vector3.zero;
            cubeRigidbody.angularVelocity = Vector3.zero;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasTouchedSurface)
        {
            return;
        }

        if (!collision.gameObject.TryGetComponent<Surface>(out _))
        {
            return;
        }

        _hasTouchedSurface = true;
        _renderer.material.color = Random.ColorHSV();

        float lifetime = Random.Range(_minLifetime, _maxLifetime);
        _lifetimeCoroutine = StartCoroutine(WaitForLifetime(lifetime));
    }

    private IEnumerator WaitForLifetime(float lifetime)
    {
        yield return new WaitForSeconds(lifetime);

        _lifetimeCoroutine = null;
        _cubePool.ReturnCube(this);
    }
}