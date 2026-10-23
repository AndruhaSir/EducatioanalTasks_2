using UnityEngine;

public class TargetPathMover : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField, Min(0.1f)] private float _speed = 3f;
    [SerializeField, Min(0.01f)] private float _reachDistance = 0.1f;

    private int _currentWaypointIndex;

    public void MoveToNextPoint()
    {
        if (_waypoints == null || _waypoints.Length == 0)
        {
            return;
        }

        Transform targetWaypoint = _waypoints[_currentWaypointIndex];

        if (targetWaypoint == null)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position_speed * Time.deltaTime);

        Vector3 offset = targetWaypoint.position - transform.position;

        if (offset.sqrMagnitude <= _reachDistance * _reachDistance)
        {
            _currentWaypointIndex = ++_currentWaypointIndex % _waypoints.Length;
        }
    }
}