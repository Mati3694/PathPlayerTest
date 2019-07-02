using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathAlignedObject : MonoBehaviour
{
    [ReadOnly]
    public Waypoint prev, next;

    public Vector3 Right { get { if (prev == null || next == null) return Vector3.zero; Vector3 dir = next.Position - prev.Position; dir.y = 0; return dir.normalized; } }
    public Vector3 Left { get { if (prev == null || next == null) return Vector3.zero; Vector3 dir = prev.Position - next.Position; dir.y = 0; return dir.normalized; } }
    

    protected virtual void Start()
    {
        CalculateCurrentNodes();
    }

    protected void ForceIntoPath(Action<Vector3> movementAction)
    {
        if (prev == null || next == null) return;

        Vector3 positionInPath = Vector3.zero;

        var pointData = GetPointClosestToPath(true);
        Vector3 pointA, pointB;

        if (pointData.normalizedDot > 1 && next.next != null) // Si me paso del nodo y hay un next
        {
            prev = next;
            next = next.next;

            pointA = prev.Position;
            pointA.y = 0;

            pointB = next.Position;
            pointB.y = 0;

            positionInPath = pointA + (pointB - pointA).normalized * (pointData.normalizedDot - 1f) * Vector3.Distance(pointA, pointB);
        }
        else if (pointData.normalizedDot < 0 && prev.prev != null) // Si me quedo atras del nodo y hay un prev
        {
            next = prev;
            prev = prev.prev;

            pointA = prev.Position;
            pointA.y = 0;

            pointB = next.Position;
            pointB.y = 0;

            positionInPath = pointB + (pointA - pointB).normalized * Mathf.Abs(pointData.normalizedDot) * Vector3.Distance(pointA, pointB);
        }
        else
        {
            positionInPath = pointData.point;
        }
        movementAction(positionInPath);
    }

    public void CalculateCurrentNodes()
    {
        for (int i = 0; i < WaypointManager.Waypoints.Count - 1; i++)
        {
            var curr = WaypointManager.GetWaypoint(i);
            var currNext = WaypointManager.GetWaypoint(i + 1);
            var dot = GetClosestPointDot(curr.transform.position, currNext.transform.position, true);

            if (dot > 0 && dot < 1)
            {
                prev = curr;
                next = currNext;
                return;
            }
        }
        Debug.LogWarning("PathAlignedObject " + gameObject.name + " not between any Waypoints", gameObject);
    }

    public float GetClosestPointDot(Vector3 pointA, Vector3 pointB, bool normalized = false)
    {
        Vector3 lineDir = pointB - pointA;
        Vector3 dirToObj = transform.position - pointA;

        double dot = Vector3.Dot(lineDir, dirToObj);
        

        dot /= lineDir.magnitude;
        
        return (float)(normalized ? dot / lineDir.magnitude : dot);
    }

    public (Vector3 point, float normalizedDot) GetPointClosestToPath(bool clamp)
    {
        Vector3 pointA = new Vector3(prev.Position.x, 0, prev.Position.z);
        Vector3 pointB = new Vector3(next.Position.x, 0, next.Position.z);

        var lineDir = pointB - pointA;
        var dot = GetClosestPointDot(pointA, pointB);

        Vector3 pointNearestToLine;

        if (!clamp)
            pointNearestToLine = pointA + lineDir.normalized * dot;
        else
            pointNearestToLine = pointA + lineDir.normalized * Mathf.Clamp(dot, 0, lineDir.magnitude); //ESTO ES POR SI QUERES QUE CLAMPEE EN LOS NODOS
        return (pointNearestToLine, dot / lineDir.magnitude);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        if (prev != null && next != null)
        {
            var closestPos = GetPointClosestToPath(false);
            Gizmos.DrawWireSphere(closestPos.point, 0.8f);
            Gizmos.DrawLine(transform.position, closestPos.point);
        }
    }
}
