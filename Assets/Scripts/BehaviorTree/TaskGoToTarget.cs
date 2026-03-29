using UnityEngine;
using UnityEngine.AI;
using BehaviorTree;
namespace BehaviorTree
{
public class TaskGoToTarget : Node2
{
private NavMeshAgent _agent;

private string _targetKey;
private float _rotationSpeed = 5f;
// Threshold to avoid calling SetDestination every single frame
private const float _destinationUpdateThreshold = 0.1f;
public TaskGoToTarget(NavMeshAgent agent, string targetKey)
{
_agent = agent;
_targetKey = targetKey;
}
public override NodeState Evaluate()
{
Transform target = GetData(_targetKey) as Transform;
if (target == null)
{
_agent.isStopped = true;
state = NodeState.FAILURE;
return state;
}
_agent.isStopped = false;
// Use sqrMagnitude for cheaper distance comparison
if ((_agent.destination - target.position).sqrMagnitude >
_destinationUpdateThreshold * _destinationUpdateThreshold)
_agent.SetDestination(target.position);
// Rotate toward target
Vector3 dir = (target.position -
_agent.transform.position).normalized;
if (dir.sqrMagnitude > 0.01f)
{
Quaternion lookRotation = Quaternion.LookRotation(dir);
_agent.transform.rotation = Quaternion.Slerp(
_agent.transform.rotation,
lookRotation,

Time.deltaTime * _rotationSpeed

);
}
state = NodeState.RUNNING;
return state;
}
}
}
