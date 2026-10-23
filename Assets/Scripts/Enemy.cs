using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private TargetPathMover _pathMover;

    private void Update()
    {
        _pathMover.MoveToNextPoint();
    }
}