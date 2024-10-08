using UnityEngine;

public class PathController : MonoBehaviour
{
    public PathManager pathManager;
    public float moveSpeed = 5.0f;
    public float rotateSpeed = 5.0f;

    private int currentWaypointIndex = 0;

    private void Start()
    {
        if (pathManager == null)
        {
            Debug.LogError("PathManager not assigned.");
            return;
        }
    }

    private void Update()
    {
        if (pathManager == null || pathManager.GetPath().Count == 0)
        {
            return;
        }

        MoveAlongPath();
    }

    private void MoveAlongPath()
    {
        Waypoint target = pathManager.GetPath()[currentWaypointIndex];
        Vector3 direction = target.pos - transform.position;
        direction.Normalize();

        transform.position += direction * moveSpeed * Time.deltaTime;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.pos) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % pathManager.GetPath().Count;
        }
    }
}
