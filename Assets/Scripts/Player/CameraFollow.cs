using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float FollowSpeed;
    public Vector3 CameraOffset;

    void FixedUpdate()
    {
        Vector3 desiredPosition = player.position + CameraOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, FollowSpeed);
        transform.position = smoothedPosition;
    }
}
