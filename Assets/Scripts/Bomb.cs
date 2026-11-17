using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Renderer), typeof(Collider), typeof(Rigidbody))]
public class Bomb : MonoBehaviour
{
    [SerializeField, Min(0.1f)] private float _minFuseTime = 2f;
    [SerializeField, Min(0.1f)] private float _maxFuseTime = 5f;
    [SerializeField, Min(0f)] private float _explosionRadius = 5f;
    [SerializeField, Min(0f)] private float _explosionForce = 700f;
    [SerializeField] private float _upwardModifier = 1f;

    private Renderer _renderer;
    private Rigidbody _rigidbody;
    private Coroutine _fuseCoroutine;
    private BombPool _bombPool;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Initialize(BombPool bombPool)
    {
        _bombPool = bombPool;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        SetAlpha(1f);
        _fuseCoroutine = StartCoroutine(FadeAndExplode(Random.Range(_minFuseTime, _maxFuseTime)));
    }

    private IEnumerator FadeAndExplode(float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            SetAlpha(1f - Mathf.Clamp01(elapsed / duration));
            yield return null;
        }

        _fuseCoroutine = null;
        Explode();
        _bombPool.ReturnBomb(this);
    }

    private void Explode()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius);
        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null && hit.attachedRigidbody != _rigidbody)
                hit.attachedRigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius,
                    _upwardModifier);
        }
    }

    private void SetAlpha(float alpha)
    {
        Color color = _renderer.material.color;
        color.a = alpha;
        _renderer.material.color = color;
    }
}