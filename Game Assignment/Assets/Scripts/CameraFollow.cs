using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public float yOffset = 1f;
    public float xOffset = 0f;
    public Transform target;
    // Update is called once per frame
    void Update()
    {
        if (target != null) // Check if target is assigned
        {
            Vector3 newPos = new Vector3(target.position.x + xOffset, target.position.y + yOffset, -10f);
            transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
        }
        else
        {
            Debug.LogWarning("Target is not assigned to the CameraFollow script.");
        }
    }
}
