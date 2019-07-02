using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Player player;
    public float smoothTime;
    private float currentVel;

    void LateUpdate()
    {
        transform.position = new Vector3(Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref currentVel, smoothTime),transform.position.y, transform.position.z);
    }
}
