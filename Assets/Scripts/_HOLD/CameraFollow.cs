using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed;
    [SerializeField] private Vector3 offset;

    // FixedUpdate is called once per frame
    void FixedUpdate()
    {
        Vector2 finalPos = target.position + offset;
        Vector2 smoothPos = Vector3.Lerp(transform.position, finalPos, smoothSpeed);
        transform.position = smoothPos;


        transform.LookAt(smoothPos);
    }
}
