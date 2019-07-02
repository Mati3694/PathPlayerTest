using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Waypoint next, prev;

    public float X { get { return transform.position.x; } }
    public float Y { get { return transform.position.y; } }
    public float Z { get { return transform.position.z; } }

    public Vector3 Position { get { return transform.position; } }


    const float GIZMO_LINE_OFFSET = 0.05f;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, 0.5f);


        if (prev != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(prev.Position +Vector3.forward * GIZMO_LINE_OFFSET, Position + Vector3.forward * GIZMO_LINE_OFFSET);
        }

        if (next != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(Position -Vector3.forward * GIZMO_LINE_OFFSET, next.Position - Vector3.forward * GIZMO_LINE_OFFSET);
        }
    }
}
