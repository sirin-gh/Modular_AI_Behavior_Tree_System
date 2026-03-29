using UnityEngine;

namespace BehaviorTree
{
public abstract class BehaviorTree : MonoBehaviour
{
private Node2 _root = null;
protected virtual void Start()
{
_root = SetupTree();
}
private void Update()
{
if (_root != null)
_root.Evaluate();
}
protected abstract Node2 SetupTree();
}
}
