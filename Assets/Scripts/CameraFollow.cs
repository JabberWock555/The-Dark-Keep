using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float cameraSpeed = 2f;
    [SerializeField] private float yOffset = 1f;
    [SerializeField] private Transform target;

    private void Update()
    {
        Vector3 newPos = new Vector3(target.position.x, target.position.y + yOffset, -5f);
        transform.position = Vector3.Slerp(transform.position, newPos, cameraSpeed * Time.deltaTime);
    }
}
