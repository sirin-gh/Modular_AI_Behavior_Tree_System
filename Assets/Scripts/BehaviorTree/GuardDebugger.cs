using UnityEngine;
public class GuardDebugger : MonoBehaviour
{

public float detectionRange = 10f;
public float fieldOfViewAngle = 90f;
public Color fovColor = new Color(1, 0, 0, 0.3f);
public Color rangeColor = Color.yellow;
void OnDrawGizmos()
{
// Detection Range
Gizmos.color = rangeColor;
Gizmos.DrawWireSphere(transform.position, detectionRange);
// Field of View
Vector3 forward = transform.forward * detectionRange;
Vector3 leftBoundary = Quaternion.Euler(0, -fieldOfViewAngle / 2f, 0)
* forward;
Vector3 rightBoundary = Quaternion.Euler(0, fieldOfViewAngle / 2f, 0)
* forward;
Gizmos.color = fovColor;
Gizmos.DrawLine(transform.position, transform.position +
leftBoundary);
Gizmos.DrawLine(transform.position, transform.position +
rightBoundary);
// Remplir FOV (arc)
int segments = 20;
Vector3 prevPoint = transform.position + leftBoundary;
for (int i = 1; i <= segments; i++)
{
float angle = -fieldOfViewAngle / 2f + (fieldOfViewAngle /
segments) * i;
Vector3 point = transform.position + Quaternion.Euler(0, angle,
0) * forward;
Gizmos.DrawLine(prevPoint, point);
prevPoint = point;
}
}
void OnDrawGizmosSelected()
{
// Path NavMesh (si NavMeshAgent attaché)
UnityEngine.AI.NavMeshAgent agent =
GetComponent<UnityEngine.AI.NavMeshAgent>();
if (agent != null && agent.hasPath)
{
Gizmos.color = Color.green;
Vector3[] corners = agent.path.corners;
for (int i = 0; i < corners.Length - 1; i++)
{
Gizmos.DrawLine(corners[i], corners[i + 1]);
Gizmos.DrawSphere(corners[i], 0.2f);
}
}
}
}
