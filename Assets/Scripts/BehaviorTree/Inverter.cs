using System.Collections.Generic;
namespace BehaviorTree
{
public class Inverter : Node2
{
public Inverter() : base() { }
public Inverter(List<Node2> children) : base(children) { }
public override NodeState Evaluate()
{
if (children.Count == 0)
{
state = NodeState.FAILURE;
return state;
}
switch (children[0].Evaluate())
{
case NodeState.FAILURE:
state = NodeState.SUCCESS;
return state;
case NodeState.SUCCESS:
state = NodeState.FAILURE;
return state;
case NodeState.RUNNING:
state = NodeState.RUNNING;
return state;
}
state = NodeState.FAILURE;
return state;
}

}
}
