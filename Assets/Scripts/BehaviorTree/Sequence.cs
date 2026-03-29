using System.Collections.Generic;
namespace BehaviorTree
{
public class Sequence : Node2
{
public Sequence() : base() { }
public Sequence(List<Node2> children) : base(children) { }
public override NodeState Evaluate()
{
foreach (Node2 node in children)
{
switch (node.Evaluate())
{
case NodeState.FAILURE:
state = NodeState.FAILURE;
return state;
case NodeState.SUCCESS:
continue;

case NodeState.RUNNING:
state = NodeState.RUNNING;
return state;
default:
state = NodeState.FAILURE;
return state;
}
}
state = NodeState.SUCCESS;
return state;
}
}
}
