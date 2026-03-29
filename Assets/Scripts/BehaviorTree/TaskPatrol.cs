using UnityEngine;
using UnityEngine.AI;
using BehaviorTree;

namespace BehaviorTree
{
    public class TaskPatrol : Node2
    {
        private Transform _transform;
        private NavMeshAgent _agent;
        private Transform[] _waypoints;
        private float _waitTime;
        private int _currentWaypointIndex = 0;
        private float _waitCounter = 0f;
        private bool _waiting = false;
        private bool _destinationSet = false;
        private bool _wasChasing = false;
        private const float _arrivalThreshold = 0.5f;

        public TaskPatrol(Transform transform, NavMeshAgent agent, Transform[] waypoints, float waitTime)
        {
            _transform = transform;
            _agent = agent;
            _waypoints = waypoints;
            _waitTime = waitTime;
        }

        public override NodeState Evaluate()
        {
            if (_waypoints == null || _waypoints.Length == 0)
            {
                state = NodeState.FAILURE;
                return state;
            }

            // Detect transition from chase back to patrol -> pick a new random waypoint
            bool isChasing = GetData("target") != null;
            if (_wasChasing && !isChasing)
            {
                _currentWaypointIndex = GetRandomWaypointIndex();
                _destinationSet = false;
                _waiting = false;
            }
            _wasChasing = isChasing;

            // Waiting at a waypoint before moving to the next
            if (_waiting)
            {
                _waitCounter += Time.deltaTime;
                if (_waitCounter >= _waitTime)
                {
                    _waiting = false;
                    _destinationSet = false;
                    _currentWaypointIndex = GetRandomWaypointIndex();
                }

                state = NodeState.RUNNING;
                return state;
            }

            // Issue SetDestination only once per waypoint
            if (!_destinationSet)
            {
                _agent.isStopped = false;
                _agent.SetDestination(_waypoints[_currentWaypointIndex].position);
                _destinationSet = true;
            }

            // Using Mathf.Max ensures we respect any stoppingDistance already set on the agent,
            // but never go below _arrivalThreshold so we don't get stuck when stoppingDistance == 0.
            float threshold = Mathf.Max(_agent.stoppingDistance, _arrivalThreshold);
            
            if (!_agent.pathPending && _agent.hasPath && _agent.remainingDistance <= threshold)
            {
                _waiting = true;
                _waitCounter = 0f;
            }

            state = NodeState.RUNNING;
            return state;
        }

        // Returns a random waypoint index always different from the current one.
        // Falls back gracefully when there is only one waypoint.
        private int GetRandomWaypointIndex()
        {
            if (_waypoints.Length <= 1)
                return 0;

            int next = _currentWaypointIndex;
            while (next == _currentWaypointIndex)
            {
                next = Random.Range(0, _waypoints.Length);
            }
            return next;
        }
    }
}
