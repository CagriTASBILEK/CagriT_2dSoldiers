using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerControl : MonoBehaviour
{
  public Vector3 targetTransform; 
    private AstarPathfinding _pathfinding;
    private GridManager _gridManager;
    private List<GridCellControl> path;
    private int currentPathIndex = 0; 
    private bool isMoving = false;

    public float speed = 5f;

    private void Start()
    {
        _gridManager = GridManager.Instance;
        _pathfinding = _gridManager.GetPathfinding();
    }

    private void Update()
    {
        
        if (targetTransform != null && isMoving)
        {
            if (path == null || path.Count == 0 || currentPathIndex >= path.Count)
            {
                Vector2Int start = new Vector2Int((int)transform.position.x, (int)transform.position.y);
                Vector2Int end = new Vector2Int((int)targetTransform.x, (int)targetTransform.y);

                path = _pathfinding.FindPath(start, end);
                currentPathIndex = 0;
            }
            else
            {
                MoveAlongPath();
            }
        }
    }

    private void MoveAlongPath()
    {
        if (currentPathIndex < path.Count)
        {
            Vector3 targetPosition = path[currentPathIndex].CellObject.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                currentPathIndex++;
                
                if (currentPathIndex >= path.Count)
                {
                    isMoving = false;
                }
            }
        }
    }

    public void SetTarget(Vector3 newTarget)
    {
        targetTransform = newTarget;
        isMoving = true;
    }

    private void OnDrawGizmos()
    {
        if (targetTransform != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(targetTransform, 0.5f);
        }

        if (path != null && path.Count > 0)
        {
            Gizmos.color = Color.black;
            for (int i = 0; i < path.Count - 1; i++)
            {
                Vector3 start = path[i].CellObject.transform.position;
                Vector3 end = path[i + 1].CellObject.transform.position;
                Gizmos.DrawLine(start, end);
            }
        }
    }
}
