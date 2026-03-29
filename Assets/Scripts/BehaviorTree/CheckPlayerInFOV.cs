using UnityEngine;
using BehaviorTree;
public class CheckPlayerInFOV : BehaviorTree.Node2
{
private Transform _transform;
private float _range;
private float _fovAngle;
private LayerMask _playerLayer;
private LayerMask _obstacleLayer;
private int _playerLayerIndex;
public CheckPlayerInFOV(Transform transform, float range, float fovAngle,
LayerMask playerLayer, LayerMask obstacleLayer)
{
_transform = transform;
_range = range;
_fovAngle = fovAngle;
_playerLayer = playerLayer;
_obstacleLayer = obstacleLayer;
_playerLayerIndex = LayerMask.NameToLayer("Player");
}
public override NodeState Evaluate()
{
Collider[] hits = Physics.OverlapSphere(_transform.position, _range,
_playerLayer);
if (hits.Length > 0)
{
Transform player = hits[0].transform;
Vector3 directionToPlayer = (player.position -
_transform.position).normalized;
float angleToPlayer = Vector3.Angle(_transform.forward,
directionToPlayer);
if (angleToPlayer <= _fovAngle / 2f)
{
Vector3 rayOrigin = _transform.position + Vector3.up *
(_transform.localScale.y * 0.5f);
RaycastHit hit;
if (Physics.Raycast(rayOrigin, directionToPlayer, out hit,
_range, _playerLayer | _obstacleLayer))
{
if (hit.collider.gameObject.layer == _playerLayerIndex)
{
SetDataAtRoot("target", player);
state = NodeState.SUCCESS;

return state;
}
}
}
}
if (GetData("target") != null)
ClearData("target");
state = NodeState.FAILURE;
return state;
}
}
