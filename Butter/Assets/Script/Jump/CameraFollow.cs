using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float fixedX = 0;
    public float yOffset = 2f;

    void LateUpdate()
    {
        Vector3 pos = transform.position;
        pos.x = fixedX;
        pos.y = target.position.y + yOffset;
        transform.position = pos;
    }
}
