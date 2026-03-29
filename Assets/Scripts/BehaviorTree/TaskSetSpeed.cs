using UnityEngine.AI;
using BehaviorTree;
namespace BehaviorTree
{
public class TaskSetSpeed : Node2
{
private NavMeshAgent _agent;
private float _speed;
public TaskSetSpeed(NavMeshAgent agent, float speed)
{
_agent = agent;
_speed = speed;
}
public override NodeState Evaluate()
{
if (_agent.speed != _speed)
_agent.speed = _speed;
state = NodeState.SUCCESS;
return state;
}
}
}
