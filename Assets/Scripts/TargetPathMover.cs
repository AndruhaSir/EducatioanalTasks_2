using UnityEngine;

public class TargetPathMover : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField, Min(0.1f)] private float _speed = 3f;
    [SerializeField, Min(0.01f)] private float _reachDistance = 0.1f;

    private int _currentWaypointIndex;

    private void Update()
    {
        MoveToNextPoint();
    }

    private void MoveToNextPoint()
    {
        if (_waypoints == null || _waypoints.Length == 0)
        {
            return;
        }

        Transform targetWaypoint = _waypoints[_currentWaypointIndex];

        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, _speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetWaypoint.position) <= _reachDistance)
        {
            _currentWaypointIndex = ++_currentWaypointIndex % _waypoints.Length;
        }
    }
}
